using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using CrestSharp;
using CrestSharp.Caches;
using CrestSharp.Model;
using FittingEngine.DBModel;
using FittingEngine.Model;
using FittingEngine.Model.Expressions;
using FittingEngine.Models;
using Serilog;

namespace FittingEngine
{
    public class FittingService : IDisposable
    {
        //TODO make configurable
        private const int CREST_REQUEST_THREAD_COUNT = 8;
        private static readonly Crest CREST = new Crest();
        private static readonly Database DATABASE = new Database(new SQLiteConnection(@"Data Source=expressions.sqlite;"));
        private static Dictionary<int, ICrestGroup> _typeToGroupMapping;
        private static Dictionary<string, ICrestType> _typenameToTypeMapping;
        private static Item _allVCharacter;

        static FittingService()
        {
            DATABASE.Connection.Open();

            //TODO don't set here
            Crest.Settings.Cache = new CrestSqliteCache();
        }

        public void Dispose()
        {
        }

        public async Task Initialize()
        {
            await Task.Factory.StartNew(
                                        () =>
                                        {
                                            lock (CREST)
                                            {
                                                if (_allVCharacter != null)
                                                {
                                                    return;
                                                }

                                                var start = DateTime.UtcNow;
                                                var categories = CREST.Inventory.GetCategories()
                                                    .ToArray();

                                                var groups = categories.AsParallel()
                                                    .WithDegreeOfParallelism(CREST_REQUEST_THREAD_COUNT)
                                                    .Where(category => category.Published)
                                                    .SelectMany(category => category.Groups)
                                                    .ToArray();

                                                Log.Logger.Debug(
                                                                 $"Got {groups.Count()} groups after {(DateTime.UtcNow - start).TotalSeconds}s");

                                                start = DateTime.UtcNow;

                                                var typeGroups = groups.AsParallel()
                                                    .WithDegreeOfParallelism(CREST_REQUEST_THREAD_COUNT)
                                                    .Where(g => g.Published)
                                                    .SelectMany(
                                                                g => g.Types.Select(
                                                                                    t => new TypeAndGroup
                                                                                         {
                                                                                             Type = t,
                                                                                             Group = g
                                                                                         }))
                                                    .ToArray();

                                                Log.Logger.Debug(
                                                                 $"Got {typeGroups.Count()} typegroups after {(DateTime.UtcNow - start).TotalSeconds}s");

                                                var typesAndGroups = typeGroups.Distinct(new NameComparer())
                                                    .Distinct(new IdComparer())
                                                    .ToArray();

                                                _typenameToTypeMapping = typesAndGroups.ToDictionary(x => x.Type.Name, x => x.Type);
                                                _typeToGroupMapping = typesAndGroups.ToDictionary(x => x.Type.Id, x => x.Group);

                                                _allVCharacter = CreateAllVCharacter(categories);
                                            }
                                        },
                                        TaskCreationOptions.LongRunning);
        }

        private static SkillWithGroup[] GetSkills(ICrestGroup group)
        {
            //access of publish lazy loads object
            return group.Types.AsParallel()
                .WithDegreeOfParallelism(8)
                .Where(t => t.Published)
                .Select(InitDogmaOfType)
                .Select(
                        t => new SkillWithGroup
                             {
                                 SkillGroupId = group.Id,
                                 SkillType = t
                             })
                .ToArray();
        }

        private static ICrestType InitDogmaOfType(ICrestType arg)
        {
            Parallel.ForEach(arg?.Dogma?.Attributes ?? new ITypeDogmaAttribute[0], attribute => attribute.Attribute.EnsureInitialization());
            Parallel.ForEach(arg?.Dogma?.Effects ?? new ITypeDogmaEffect[0], effect => effect.Effect.EnsureInitialization());

            return arg;
        }

        private static Item CreateAllVCharacter(ICrestCategory[] categories)
        {
            //TODO this takes too long. create a special stripped down database for the skills/effects etc for faster loading
            const int SKILL_CATEGORY_ID = 16;
            var skillCategory = categories.First(x => x.Id == SKILL_CATEGORY_ID);
            var skillTypes = skillCategory.Groups.Where(g => g.Published)
                .SelectMany(GetSkills)
                .ToArray();

            var items = skillTypes.Select(t => new Skill(t.SkillType, t.SkillGroupId, SKILL_CATEGORY_ID, SkillLevel.V))
                .ToArray();

            var character = new Item("Character", -1, -1, -1, -1, -1);
            character.InstalledItems.AddRange(items);

            return character;
        }

        public Item GetAllVCharacter()
        {
            EnsureInitalization();
            return _allVCharacter.Copy();
        }

        private void EnsureInitalization()
        {
            Initialize()
                .Wait();
        }

        public IContext CreateContext(Item ship, Item character = null)
        {
            EnsureInitalization();
            if (character == null)
            {
                character = _allVCharacter.Copy();
            }

            character.InstalledItems.Add(ship);

            return new Context
                   {
                       Char = character,
                       Ship = ship
                   };
        }

        public IList<Item> CreateItems(params string[] typeNames)
        {
            EnsureInitalization();
            return typeNames.Select(CreateItem)
                .ToArray();
        }

        public Item CreateItem(string typeName)
        {
            EnsureInitalization();
            try
            {
                var crestType = _typenameToTypeMapping[typeName];
                var group = _typeToGroupMapping[crestType.Id];
                return new Item(crestType, group.Id, group.Category.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Could not create item " + typeName, e);
            }
        }

        public Item CreateTestShip(string shipName)
        {
            EnsureInitalization();
            return CreateItem(shipName);
        }

        public static IEffect CreateEffect(Item parent, ICrestDogmaEffect dogmaEffect)
        {
            var effect = new Effect(dogmaEffect.Id, (EffectCategory) dogmaEffect.EffectCategory);

            const string QUERY =
                @"WITH ExpressionCTE (expressionID, operandID, arg1, arg2, expressionTypeID, expressionGroupID, expressionAttributeID, expressionValue)
AS (
    SELECT expressionID, operandID, arg1, arg2, expressionTypeID, expressionGroupID, expressionAttributeID, expressionValue
    FROM dgmExpressions
    WHERE expressionID = @0

    UNION ALL

    SELECT d1.expressionID, d1.operandID, d1.arg1, d1.arg2, d1.expressionTypeID, d1.expressionGroupID, d1.expressionAttributeID, d1.expressionValue
    FROM dgmExpressions as d1 JOIN ExpressionCTE ON d1.expressionID = ExpressionCTE.arg1 OR d1.expressionID = ExpressionCTE.arg2
) SELECT DISTINCT(expressionID), operandID, arg1, arg2, expressionTypeID, expressionGroupID, expressionAttributeID, expressionValue
FROM ExpressionCTE;";

            DATABASE.EnableAutoSelect = false;
            DATABASE.EnableNamedParams = false;

            if (dogmaEffect.PreExpressionId != 0)
            {
                var preExpressions = DATABASE.Query<ExpressionRecord>(QUERY, dogmaEffect.PreExpressionId)
                    .ToDictionary(x =>(int) x.expressionID, x => x);
                if (!preExpressions.ContainsKey(dogmaEffect.PreExpressionId))
                {
                    effect.PreExpression = new NOPExpression();
                    Console.WriteLine($"No such expression: {dogmaEffect.PreExpressionId}");
                }
                else
                {
                    effect.PreExpression = CreateExpression(parent, preExpressions[dogmaEffect.PreExpressionId], preExpressions);
                }
            }
            else
            {
                effect.PreExpression = new NOPExpression();
            }

            if (dogmaEffect.PostExpressionId != 0)
            {
                var postExpressions = DATABASE.Query<ExpressionRecord>(QUERY, dogmaEffect.PostExpressionId)
                    .ToDictionary(x =>(int) x.expressionID, x => x);
                if (!postExpressions.ContainsKey(dogmaEffect.PostExpressionId))
                {
                    effect.PostExpression = new NOPExpression();
                    Console.WriteLine($"No such expression: {dogmaEffect.PostExpressionId}");
                }
                else
                {
                    effect.PostExpression = CreateExpression(parent, postExpressions[dogmaEffect.PostExpressionId], postExpressions);
                }
            }
            else
            {
                effect.PostExpression = new NOPExpression();
            }

            return effect;
        }

        private static IExpression CreateExpression(Item parent,
                                                    ExpressionRecord expression,
                                                    Dictionary<int, ExpressionRecord> preExpressions)
        {
            try
            {
                //TODO check for null values in nullable constants?
                IExpression arg1;
                IExpression arg2;

                long x = expression.operandID.GetValueOrDefault();

                switch (x)
                {
                    case 1:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddExpression(arg1, arg2);
                    case 2: // gang related, ignore for now
                    case 3:
                    case 4:
                    case 5:
                        //TODO implement -> application to group modules -> does not really work with the current implementation
                        //as in the child expressions there seems to be no reference for the group
                        //idea: set Other or some other property in the context to the targets for the lower expressions
                        return new NOPExpression();
                    case 6:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddModifierExpression(arg1, arg2);
                    case 7:
                        //add item modifier to location by group
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddModifierExpression(arg1, arg2);
                    case 8:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddModifierExpression(arg1, arg2);
                    case 9:
                        //TODO check ob wirklic hd asselbe wie 6, aber sieht erst einmal so aus
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddModifierExpression(arg1, arg2);
                    case 10:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AndExpression(arg1, arg2);
                    case 11:
                        //TODO die selektoren muessen evtl. other context parameter abhaengig von operand gesetzt bekommen?
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddModifierExpression(arg1, arg2);
                    case 12:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new GetItemAttributeExpression(arg1, arg2);
                    case 13:
                        if (expression.arg1 == null)
                        {
                            throw new Exception("missing argument for unary operand " + expression.operandID);
                        }
                        arg1 = CreateExpression(parent, preExpressions[(int) expression.arg1.Value], preExpressions);
                        return new AttackExpression(arg1);
                    case 17:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new SpliceExpression(arg1, arg2);
                    case 18:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new SubstractAttributeValueExpression(arg1, arg2);
                    case 21:
                        //get modifier operator (e.g. ModAdd)
                        return new ConstantExpression(expression.expressionValue);
                    case 22:
                        //get attribute id
                        return new ConstantExpression(expression.expressionAttributeID);
                    case 23:
                        //boolean constant
                        return new ConstantExpression(expression.expressionValue == "1");
                    case 24:
                        return new LocationExpression(expression.expressionValue);
                    case 26:
                        //get group id
                        if (expression.expressionGroupID == null)
                        {
                            //some values are fucked up, we need to map string to groupID manually
                            switch (expression.expressionValue)
                            {
                                case "    None":
                                    expression.expressionGroupID = -1;
                                    break;
                                case "HybridWeapon":
                                    expression.expressionGroupID = 74;
                                    break;
                                case "ProjectileWeapon":
                                    expression.expressionGroupID = 55;
                                    break;
                                case "EnergyWeapon":
                                    expression.expressionGroupID = 53;
                                    break;

                                case "MiningLaser":
                                    expression.expressionGroupID = 54;
                                    break;
                                //TODO there are other values like structure and power core with operand26 in the db ...
                                default:
                                    //TODO move to crest
                                    expression.expressionGroupID = DATABASE.First<int>(
                                                                                       "SELECT groupID FROM invGroups WHERE groupName = @0",
                                                                                       expression.expressionValue);
                                    break;
                                //throw new Exception("operand 26 unknown group value " + expression.expressionValue);
                            }

                            //TODO koennte man in der db fixen
                        }
                        return new ConstantExpression(expression.expressionGroupID);
                    case 27:
                        //integer constant
                        return new ConstantExpression(int.Parse(expression.expressionValue));
                    case 28:
                        //error text
                        return new ConstantExpression(expression.expressionValue);
                    case 29:
                        //type definition
                        //TODO gleicher scheiss wie 26
                        if (expression.expressionTypeID == null)
                        {
                            switch (expression.expressionValue)
                            {
                                case "Shield Emission Systems":
                                    expression.expressionTypeID = 3422;
                                    break;
                                case "Acceleration Control":
                                case "Acceration Control":
                                    expression.expressionTypeID = 3452;
                                    break;

                                default:
                                    throw new Exception("operand 29 unknown type value " + expression.expressionValue);
                            }
                        }
                        return new ConstantExpression(expression.expressionTypeID);

                    case 31:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new GetItemModifierExpression(arg1, arg2);
                    case 32:
                        //EMP Wave effect
                        return new NOPExpression();
                    case 35:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new GetItemAttributeValueExpression(arg1, arg2);
                    case 36:
                        if (expression.arg1 == null)
                        {
                            throw new Exception("missing argument for unary operand " + expression.operandID);
                        }
                        arg1 = CreateExpression(parent, preExpressions[(int) expression.arg1.Value], preExpressions);
                        return new GetTypeIdExpression(arg1);
                    case 38:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new GreaterExpression(arg1, arg2);
                    case 39:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new GreaterEqualExpression(arg1, arg2);

                    case 41:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new IfThenExpression(arg1, arg2);
                    case 42:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AddAttributeValueExpression(arg1, arg2);
                    case 44:
                        //TODO e.g. for Caldari Navy Mjolnir Light Missile, what does it do?
                        return new NOPExpression();
                    case 48:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new LocationAndGroupSelectorExpression(arg1, arg2);
                    case 49:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new LocationAndSkillRequirementSelectorExpression(arg1, arg2);
                    case 52:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new OrExpression(arg1, arg2);
                    case 55: // remove gang modifier
                    case 56:
                    case 57:
                        return new NOPExpression();
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new RemoveModifierExpression(arg1, arg2);
                    case 65:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new AttributeAssignmentExpression(arg1, arg2);
                    case 67:
                        //TODO skillcheck expression ...
                        return new ConstantExpression(true);
                    case 68:
                        GetBinaryExpressionParameters(parent, expression, preExpressions, out arg1, out arg2);
                        return new SubExpression(arg1, arg2);
                    case 73:
                        if (expression.arg1 == null)
                        {
                            throw new Exception("missing argument for unary operand " + expression.operandID);
                        }
                        return
                            new RaiseUserErrorExpression(
                                CreateExpression(parent, preExpressions[(int) expression.arg1.Value], preExpressions));

                    default:

                        //TODO return NOP for now, later crawl every type and find out missing expressions
                        return new NOPExpression();
                    //throw new NotImplementedException("operand " + expression.operandID + " not implemented for type " + parent.TypeId);
                }
            }
            catch (Exception e)
            {
                //TODO remove TODO logging
                Console.WriteLine("error in creating expression {0} with operand {1} ", expression.expressionID, expression.operandID);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        private static void GetBinaryExpressionParameters(Item parent,
                                                          ExpressionRecord expression,
                                                          Dictionary<int, ExpressionRecord> preExpressions,
                                                          out IExpression arg1,
                                                          out IExpression arg2)
        {
            if (expression.arg1 == null || expression.arg2 == null)
            {
                throw new Exception("missing argument for binary operand " + expression.operandID);
            }
            arg1 = CreateExpression(parent, preExpressions[(int) expression.arg1.Value], preExpressions);
            arg2 = CreateExpression(parent, preExpressions[(int) expression.arg2.Value], preExpressions);
        }

        public Ship CreateShip(string typeName)
        {
            EnsureInitalization();
            try
            {
                var crestType = _typenameToTypeMapping[typeName];
                var group = _typeToGroupMapping[crestType.Id];
                return new Ship(crestType, group.Id, group.Category.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Could not create ship " + typeName, e);
            }
        }

        public async Task<Context> CreatContextFromFittingAsync(Fitting fittingModel)
        {
            const int RIG_SLOT = 2663;
            const int HIGH_SLOT = 12;
            const int MID_SLOT = 13;
            const int LOW_SLOT = 11;
            const int SUBSYSTEM_SLOT = 3772;
            return await Task.Factory.StartNew(
                                               () =>
                                               {
                                                   var ship = CreateShip(fittingModel.Ship.Name);
                                                   //TODO make sure to only select modules here
                                                   var items = CreateItems(
                                                                           fittingModel.Items.Select(x => x.Type.Name)
                                                                               .ToArray())
                                                       .Where(
                                                              i =>
                                                              i.Effects.Any(
                                                                            x =>
                                                                            x.EffectId == RIG_SLOT || x.EffectId == HIGH_SLOT
                                                                            || x.EffectId == MID_SLOT || x.EffectId == LOW_SLOT
                                                                            || x.EffectId == SUBSYSTEM_SLOT))
                                                       .ToArray();

                                                   var context = new Context
                                                                 {
                                                                     Ship = ship,
                                                                     Target = CreateItem("Rifter"),
                                                                     Char = GetAllVCharacter(),
                                                                     Area = new Item()
                                                                 };
                                                   context.Char.InstalledItems.Add(ship);
                                                   ship.InstalledItems.AddRange(items);

                                                   return context;
                                               });
        }

        private struct TypeAndGroup
        {
            public ICrestGroup Group;
            public ICrestType Type;
        }

        private class NameComparer : IEqualityComparer<TypeAndGroup>
        {
            public bool Equals(TypeAndGroup x, TypeAndGroup y)
            {
                return x.Type.Name == y.Type.Name;
            }

            public int GetHashCode(TypeAndGroup obj)
            {
                return obj.Type.Name.GetHashCode();
            }
        }

        private class IdComparer : IEqualityComparer<TypeAndGroup>
        {
            public bool Equals(TypeAndGroup x, TypeAndGroup y)
            {
                return x.Type.Id == y.Type.Id;
            }

            public int GetHashCode(TypeAndGroup obj)
            {
                return obj.Type.Id.GetHashCode();
            }
        }

        public struct SkillWithGroup
        {
            public int SkillGroupId;
            public ICrestType SkillType;
        }
    }
}
