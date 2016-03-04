using System;
using System.Collections.Generic;
using System.Linq;
using FittingEngine;
using FittingEngine.Model;
using FittingEngine.Model.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attribute = FittingEngine.Model.Attribute;

namespace FittingEngineTest
{
    [TestClass]
    public class ExpressionTest
    {
        private Item _area;
        private Item _char;
        private Context _context;
        private Item _other;
        private Item _self;
        private Item _ship;
        private Stack<object> _stack;
        private Item _target;

        [TestInitialize]
        public void Init()
        {
            _stack = new Stack<object>();
            _ship = new FittingService().CreateTestShip("Rifter");
            _area = new Item();
            _char = new Item();
            _target = new Item();
            _self = new Item();
            _other = new Item();
            _context = new Context {
                                       Ship = _ship,
                                       Area = _area,
                                       Char = _char,
                                       Target = _target,
                                       Other = _other,
                                       Self = _self
                                   };
        }

        [TestMethod]
        public void TestRemoveModifierExpression()
        {
            var attribute = new Attribute(null, 1, "test", true, true, 12);
            var attribute2 = new Attribute(null, 11, "test2", true, true, 12);
            attribute.AdditionAssociations.Add(attribute2);
            _self.AddAttributeRange(new[] { attribute2 });
           
            Assert.AreEqual(24,attribute.Value);

            var targetAttributes = new[] { attribute.AdditionAssociations };
            var e = new RemoveModifierExpression(new ConstantExpression(targetAttributes), new ConstantExpression(11));

            e.Execute(_context, _stack);

            Assert.IsFalse(_stack.Any());

            Assert.AreEqual(12, attribute.Value);
        }

        [TestMethod]
        public void TestLocationAndGroupSelectorExpression()
        {
            const int GROUP_ID = 200;
            var type1 = new Item("test", 15, GROUP_ID, 1, 14, 13);
            var type2 = new Item("test", 15, GROUP_ID, 1, 14, 13);
            var type3 = new Item("test", 15, GROUP_ID + 1, 1, 14, 13);
            _ship.InstalledItems.Add(type1);
            _ship.InstalledItems.Add(type2);
            _ship.InstalledItems.Add(type3);
            var e = new LocationAndGroupSelectorExpression(new ConstantExpression(_ship), new ConstantExpression(GROUP_ID));

            e.Execute(_context, _stack);
            var items = ((IEnumerable<Item>)_stack.Pop()).ToArray();
            Assert.IsFalse(_stack.Any());

            Assert.AreEqual(2, items.Count());

            Assert.AreSame(type1, items.First());
            Assert.AreSame(type2, items.Skip(1).First());
           
        }


        [TestMethod]
        public void TestGetTypeIdExpression()
        {
            var e = new GetTypeIdExpression(new ConstantExpression(new Item(null, 111,1,1,1,1,1)));

            e.Execute(_context, _stack);
            Assert.AreEqual(111, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestLocationAndSkillRequirementSelectorExpression()
        {
            foreach (var curAttributeId in new[]{182,183,184,1285,1289,1290})
            {
                TestLocationAndSkillRequirementInternal(curAttributeId);
            }
        }

        private void TestLocationAndSkillRequirementInternal(int attributeId)
        {
            int skillType = 3000 + attributeId;
            var item = new Item();
            item.AddAttributeRange(new[] { new Attribute(item, attributeId, "requiredSkill", true , true, skillType), });
            var item2 = new Item();
            item2.AddAttributeRange(new[] { new Attribute(item2, 0, "invalid", true, true, skillType), });
            var item3 = new Item();
            item3.AddAttributeRange(new[] { new Attribute(item3, attributeId, "requiredSkill", true, true, skillType), });
            _ship.InstalledItems.Add(item);
            _ship.InstalledItems.Add(item2);
            _ship.InstalledItems.Add(item3);
            var e = new LocationAndSkillRequirementSelectorExpression(new ConstantExpression(_ship), new ConstantExpression(skillType));

            e.Execute(_context, _stack);
            var items = ((IEnumerable<Item>) _stack.Pop()).ToArray();
            Assert.IsFalse(_stack.Any());

            Assert.AreEqual(2, items.Count());

            Assert.AreSame(item, items.First());
            Assert.AreSame(item3, items.Skip(1).First());
        }

        [TestMethod]
        public void TestAddExpression()
        {
            var e = new AddExpression(new ConstantExpression(5), new ConstantExpression(7));

            e.Execute(_context, _stack);
            Assert.AreEqual(12.0, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestAddModifierExpression()
        {
            var attribute = new Attribute(null,1, "test", true, true, 12);
            _self.AddAttributeRange(new[] {attribute});

            var targetAttribute1 = new Attribute(null, 11, "test", true, true, 20);
            var targetAttribute2 = new Attribute(null, 12, "test", true, true, 22);

            var targetAttributes = new [] {targetAttribute1.AdditionAssociations, targetAttribute2.AdditionAssociations};
            var e = new AddModifierExpression(new ConstantExpression(targetAttributes), new ConstantExpression(1));

            e.Execute(_context, _stack);

            Assert.IsFalse(_stack.Any());

            //TODO test kann nur implizit erfolgen
            //Assert.AreSame(attribute, targetAttribute1.AdditionAssociations.Single());
            //Assert.AreSame(attribute, targetAttribute2.AdditionAssociations.Single());
        }

        [TestMethod]
        public void TestAndExpression()
        {
            var e = new AndExpression(new ConstantExpression(true), new ConstantExpression(true));

            e.Execute(_context, _stack);
            Assert.AreEqual(true, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            var e2 = new AndExpression(new ConstantExpression(true), new ConstantExpression(false));
            e2.Execute(_context, _stack);
            Assert.AreEqual(false, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestAttributeAssignmentExpression()
        {
            var attribute = new Attribute(null, 1, "test", true, true, 7);
            var e = new AttributeAssignmentExpression(new ConstantExpression(new[] { attribute }), new ConstantExpression(27.0));

            e.Execute(_context, _stack);
            Assert.IsFalse(_stack.Any());

            Assert.AreEqual(27.0, attribute.Value);
        }

        [TestMethod]
        public void TestConstantExpression()
        {
            var e = new ConstantExpression(5);
            e.Execute(_context, _stack);
            Assert.AreEqual(5, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestGetItemAttributeExpression()
        {
            var item = new Item("test", 14, 1, 2, 3, 4);
            const int ATTRIBUTE_ID = 1;
            const double ATTRIBUTE_VALUE = 15.0;
            var attribute = new Attribute(item, ATTRIBUTE_ID, "testAttribute", true , true, ATTRIBUTE_VALUE);
            item.AddAttributeRange(new[] {attribute});

            var e = new GetItemAttributeExpression(new ConstantExpression(item), new ConstantExpression(ATTRIBUTE_ID));
            e.Execute(_context, _stack);
            Assert.IsTrue(attribute == ((IEnumerable<IAttribute>)_stack.Pop()).Single());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestGetItemAttributeValueExpression()
        {
            var item = new Item("test", 14, 1, 2,3, 4);
            const int ATTRIBUTE_ID = 1;
            const double ATTRIBUTE_VALUE = 15.0;
            item.AddAttributeRange(new[] {new Attribute(item, ATTRIBUTE_ID, "testAttribute", true, true, ATTRIBUTE_VALUE)});

            var e = new GetItemAttributeValueExpression(new ConstantExpression(item), new ConstantExpression(ATTRIBUTE_ID));
            e.Execute(_context, _stack);

            Assert.AreEqual(ATTRIBUTE_VALUE, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestGetItemModifierExpression()
        {
            var attribute = new Attribute(null,1, "test", true, true,12);
            _self.AddAttributeRange(new[] {attribute});

            var e = new GetItemModifierExpression(new ConstantExpression("ModAdd"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.AdditionAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("ModSub"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.SubstractionAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PreMul"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PreMultiplicationAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PostMul"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PostMultiplicationAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PreDiv"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PreDivisionAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PostDiv"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PostDivisionAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PreAssignment"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PreAssignmentAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PostAssignment"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PostAssignmentAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new GetItemModifierExpression(new ConstantExpression("PostPercent"), new ConstantExpression(attribute));

            e.Execute(_context, _stack);
            Assert.IsTrue(attribute.PostPercentAssociations == _stack.Pop());
            Assert.IsFalse(_stack.Any());
            var attribute2 = new Attribute(null, 12, "test", true, true, 122);
            e = new GetItemModifierExpression(new ConstantExpression("ModAdd"), new ConstantExpression(new[] {attribute, attribute2}));

            e.Execute(_context, _stack);
            var attributes = (IEnumerable<IAssociations>) _stack.Pop();
            Assert.AreEqual(2, attributes.Count());
            Assert.IsTrue(attributes.Any(x => x == attribute.AdditionAssociations));
            Assert.IsTrue(attributes.Any(x => x == attribute2.AdditionAssociations));
        }

        [TestMethod]
        public void TestRaiseUserErrorExpression()
        {
            var e = new RaiseUserErrorExpression(new ConstantExpression("moep"));

            e.Execute(_context, _stack);
            Assert.IsFalse(_stack.Any());
            Assert.AreEqual("moep", _context.UserErrors.Single());
        }
        
        [TestMethod]
        public void TestGreaterExpression()
        {
            var e = new GreaterExpression(new ConstantExpression(15), new ConstantExpression(7));

            e.Execute(_context, _stack);
            Assert.AreEqual(true, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            var e2 = new GreaterExpression(new ConstantExpression(9), new ConstantExpression(9));
            e2.Execute(_context, _stack);
            Assert.AreEqual(false, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestGreateEqualExpression()
        {
            var e = new GreaterEqualExpression(new ConstantExpression(15), new ConstantExpression(7));

            e.Execute(_context, _stack);
            Assert.AreEqual(true, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            var e2 = new GreaterEqualExpression(new ConstantExpression(9), new ConstantExpression(23));
            e2.Execute(_context, _stack);
            Assert.AreEqual(false, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            var e3 = new GreaterEqualExpression(new ConstantExpression(9), new ConstantExpression(9));
            e3.Execute(_context, _stack);
            Assert.AreEqual(true, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestIfThenExpression()
        {
            var e = new IfThenExpression(new ConstantExpression(true), new ConstantExpression(7));
            e.Execute(_context, _stack);
            Assert.AreEqual(7, _stack.Pop());
            //TODO macht das sinn, dass das testergebnis auf den stack kommt?
            Assert.AreEqual(true, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            var e2 = new IfThenExpression(new ConstantExpression(false), new ConstantExpression(7));
            e2.Execute(_context, _stack);
            Assert.AreEqual(false, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestLocationExpression()
        {
            var e = new LocationExpression("Ship");
            e.Execute(_context, _stack);

            Assert.AreSame(_ship, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new LocationExpression("Char");
            e.Execute(_context, _stack);

            Assert.AreSame(_char, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new LocationExpression("Target");
            e.Execute(_context, _stack);

            Assert.AreSame(_target, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new LocationExpression("Area");
            e.Execute(_context, _stack);

            Assert.AreSame(_area, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new LocationExpression("Self");
            e.Execute(_context, _stack);

            Assert.AreSame(_self, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            e = new LocationExpression("Other");
            e.Execute(_context, _stack);

            Assert.AreSame(_other, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestOrExpression()
        {
            var e = new OrExpression(new ConstantExpression(false), new ConstantExpression(true));
            e.Execute(_context, _stack);
            Assert.AreEqual(true, _stack.Pop());
            Assert.IsFalse(_stack.Any());

            var e2 = new AndExpression(new ConstantExpression(false), new ConstantExpression(false));
            e2.Execute(_context, _stack);
            Assert.AreEqual(false, _stack.Pop());
            Assert.IsFalse(_stack.Any());
        }

        [TestMethod]
        public void TestSpliceExpression()
        {
            var e = new SpliceExpression(new ConstantExpression(5), new ConstantExpression(7));

            e.Execute(_context, _stack);
            Assert.AreEqual(7, _stack.Pop());
            Assert.AreEqual(5, _stack.Pop());

            Assert.IsFalse(_stack.Any());
        }
    }
}
