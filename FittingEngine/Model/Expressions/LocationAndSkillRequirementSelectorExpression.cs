using System;
using System.Collections.Generic;
using System.Linq;

namespace FittingEngine.Model.Expressions
{
    public class LocationAndSkillRequirementSelectorExpression : BinaryExpression
    {
        private static readonly int[] SKILL_REQUIREMENT_ATTRIBUTE_IDS = {182, 183, 184, 1285, 1289, 1290};

        public LocationAndSkillRequirementSelectorExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var item = (Item) stack.Pop();

            Arg2.Execute(context, stack);
            var attributeIdO = stack.Pop();
            if (attributeIdO == null)
            {
                throw new Exception("Missing attributeId");
            }
            var attributeId = Convert.ToInt32(attributeIdO);
            var selectedItems = GetItemsRecursively(item, attributeId);

            stack.Push(selectedItems);
        }

        private static List<Item> GetItemsRecursively(Item item, int attributeId)
        {
            var itemStack = new Stack<Item>();
            itemStack.Push(item);

            var selectedItems = new List<Item>();

            while (itemStack.Any())
            {
                var curItem = itemStack.Pop();
                if (curItem is Skill)
                {
                    continue; //TODO das ist haesslich evtl. anders loesen
                }
                foreach (var subItem in curItem.InstalledItems)
                {
                    if (subItem is Skill)
                    {
                        continue;
                    }
                    //TODO index attributes by id -> faster lookup
                    foreach (var curSkillRequirementId  in SKILL_REQUIREMENT_ATTRIBUTE_IDS)
                    {
                        IAttribute attribute;
                        if (subItem.TryGetAttributeById(curSkillRequirementId, out attribute) && (int) attribute.Value == attributeId)
                        {
                            selectedItems.Add(subItem);
                            break;
                        }
                    }
                    itemStack.Push(subItem);
                }
            }
            return selectedItems;
        }
    }
}
