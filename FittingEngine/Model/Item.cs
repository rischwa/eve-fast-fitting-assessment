using System;
using System.Collections.Generic;
using System.Linq;
using CrestSharp.Model;

namespace FittingEngine.Model
{
    public static class IDictionaryExtensions
    {
        public static void AddRange<TK, TV>(this IDictionary<TK, TV> dic, IEnumerable<TV> entries, Func<TV, TK> keySelector)
        {
            foreach (var curEntry in entries)
            {
                dic[keySelector(curEntry)] = curEntry;
            }
        }
    }

    public enum ItemState
    {
        None = 0,
        Installed = 1,
        Active = 2,
        Overloaded = 3
    }

    //TODO splitten in ship
    [Serializable]
    public class Item
    {
        private readonly Dictionary<int, IAttribute> _attributesById = new Dictionary<int, IAttribute>();
        private readonly List<IEffect> _effects = new List<IEffect>();
        private ItemState _state;

        private Item(Item item)
        {
            TypeName = item.TypeName;
            TypeId = item.TypeId;
            GroupId = item.GroupId;
            CategoryId = item.CategoryId;
            InstalledItems = item.InstalledItems.Select(x=>x.Copy()).ToList();

            _attributesById = item._attributesById.ToDictionary(x => x.Key, x => x.Value.Copy(this));
            _effects = item._effects;
            _state = ItemState.None;
        }
        
        public Item(ICrestType type, int groupId, int categoryId)
            : this(type.Name, type.Id, groupId, categoryId, type.Volume, type.Mass, type.Capacity, type.Radius)
        {
            //add effects
            AddAttributeRange(
                              type.Dogma.Attributes.AsParallel()
                                  .Select(
                                          x =>
                                          new Attribute(
                                              this,
                                              x.Attribute.Id,
                                              x.Attribute.Name,
                                              x.Attribute.Stackable,
                                              x.Attribute.HighIsGood,
                                              x.Value ?? x.Attribute.DefaultValue)));

            var effects = type.Dogma?.Effects;
            if (effects != null)
            {
                AddEffectRange(effects.Select(x => FittingService.CreateEffect(this, x.Effect)));
            }
        }

        public Item(string typeName,
                    int typeId,
                    int groupId,
                    int categoryId,
                    double volume,
                    double mass = 0,
                    double capacity = 0,
                    double radius = 0) : this()
        {
            TypeName = typeName;
            TypeId = typeId;
            GroupId = groupId;
            CategoryId = categoryId;
            AddAttributeRange(
                              new[]
                              {
                                  new Attribute(this, 4, "mass", false, true, mass),
                                  new Attribute(this, 104, "warpScrambleStatus", true, true, 0),
                                  new Attribute(this, 38, "capacity", true, true, capacity),
                                  new Attribute(this, 162, "radius", true, true, radius),
                                  new Attribute(this, 161, "volume", true, true, volume),
                                  new Attribute(this, 1470, "maxVelocityMultiplier", true, true, 0)
                              });
        }

        public Item()
        {
            //add attributes for mass capacity udn whatever else -> see eufe
            AddAttributeRange(new[] {new Attribute(this, 2, "isOnline", false, true, 0)});
        }

        //TODO attributes indizieren by id

        public Dictionary<int, IAttribute>.ValueCollection Attributes => _attributesById.Values;

        //public int Capacity { get; private set; }
        public int CategoryId { get; }

        public IReadOnlyCollection<IEffect> Effects
        {
            get { return _effects.AsReadOnly(); }
        }

        public int GroupId { get; }

        //TODO own class for installed items with methods for uninstalling etc.
        public List<Item> InstalledItems { get; } = new List<Item>();

        //public int Mass { get; private set; }
        public int TypeId { get; }

        public string TypeName { get; }

        public Item Copy()
        {
            return new Item(this);
        }

        public override string ToString()
        {
            return TypeName + " (" + TypeId + ")";
        }

        public void UninstallItems(Context context)
        {
            foreach (var curItem in InstalledItems)
            {
                curItem.Offline(context);
            }

            InstalledItems.Clear();
        }

        public void AddAttributeRange(IEnumerable<IAttribute> attributes)
        {
            _attributesById.AddRange(
                                     attributes.Where(
                                                      x =>
                                                      {
                                                          IAttribute attr;
                                                          return !_attributesById.TryGetValue(x.AttributeID, out attr)
                                                                 || !(attr is ConstantAttribute);
                                                      }),
                                     x => x.AttributeID);
        }

        public void AddEffectRange(IEnumerable<IEffect> effects)
        {
            _effects.AddRange(effects);
        }

        public bool TryGetAttributeById(int id, out IAttribute attribute)
        {
            return _attributesById.TryGetValue(id, out attribute);
        }

        public IAttribute GetAttributeById(int id)
        {
            return _attributesById[id];
        }

        public IAttribute GetAttributeByIdOrNull(int id)
        {
            IAttribute attribute;
            return _attributesById.TryGetValue(id, out attribute) ? attribute : null;
        }

        public bool ContainsAttributeId(int id)
        {
            return _attributesById.ContainsKey(id);
        }

        public void Online(Context context)
        {
            //TODO we should switch state up and down
            ExecutePreExpressions(context, x => x.EffectCategory == EffectCategory.Passive || x.EffectCategory == EffectCategory.Online);
            _state = ItemState.Installed;
        }

        public void Activate(Context context)
        {
            switch (_state)
            {
                case ItemState.None:
                    Online(context);
                    break;
                case ItemState.Installed:

                    break;
                default:
                    break;
                //return;
            }
            ExecutePreExpressions(context, x => x.EffectCategory == EffectCategory.Active);
            _state = ItemState.Active;
        }

        //TODO der kram muss rekursiv auf den installed items aufgerufen werden und nicht mittels executepreexpressions rekursiv, damit state erhalten bleibt
        public void Overload(Context context)
        {
            switch (_state)
            {
                case ItemState.None:
                    Activate(context);

                    break;
                case ItemState.Installed:
                    Activate(context);
                    break;
                case ItemState.Active:
                    break;
                default:
                    break;
                // return;
            }
            ExecutePreExpressions(context, x => x.EffectCategory == EffectCategory.Overload);
            _state = ItemState.Overloaded;
        }

        public void Offline(Context context)
        {
            switch (_state)
            {
                case ItemState.Installed:
                    ExecutePostExpressions(
                                           context,
                                           x => x.EffectCategory == EffectCategory.Passive || x.EffectCategory == EffectCategory.Online);
                    _state = ItemState.None;
                    return;
                case ItemState.Active:
                    ExecutePostExpressions(
                                           context,
                                           x =>
                                           x.EffectCategory == EffectCategory.Passive || x.EffectCategory == EffectCategory.Online
                                           || x.EffectCategory == EffectCategory.Active);
                    _state = ItemState.None;
                    return;
                case ItemState.Overloaded:
                    ExecutePostExpressions(
                                           context,
                                           x =>
                                           x.EffectCategory == EffectCategory.Passive || x.EffectCategory == EffectCategory.Online
                                           || x.EffectCategory == EffectCategory.Active || x.EffectCategory == EffectCategory.Overload);
                    _state = ItemState.None;
                    return;
                default:
                    return;
            }
        }

        private void ExecutePreExpressions(Context context, Func<IEffect, bool> effectFilter = null)
        {
            if (effectFilter == null)
            {
                effectFilter = x => true;
            }
            context.Self = this;
            foreach (var curExpression in Effects.Where(effectFilter)
                .Select(x => x.PreExpression))
            {
                curExpression.Execute(context, new Stack<object>());
            }
            context.Other = this;
            foreach (var curItem in InstalledItems)
            {
                curItem.ExecutePreExpressions(context, effectFilter);
            }
        }

        private void ExecutePostExpressions(Context context, Func<IEffect, bool> effectFilter = null)
        {
            if (effectFilter == null)
            {
                effectFilter = x => true;
            }
            context.Self = this;
            foreach (var curExpression in Effects.Where(effectFilter)
                .Select(x => x.PostExpression))
            {
                curExpression.Execute(context, new Stack<object>());
            }
            context.Other = this;
            foreach (var curItem in InstalledItems)
            {
                curItem.ExecutePostExpressions(context, effectFilter);
            }
        }
    }

    //public class ConstantAttribute : Attribute
    //{
    //    public ConstantAttribute(Item parent, int id, string name, int value) :base(parent, id, name, false, false, value)
    //    {
    //        SetValueForced(value);
    //    }
    //}
}
