using System;
using System.Collections.Generic;
using System.Linq;

namespace FittingEngine.Model.Expressions
{
    public class LocationAndGroupSelectorExpression : BinaryExpression
    {
        public LocationAndGroupSelectorExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        //TODO locations als extra typ behandeln und nicht item?
        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var item = (Item) stack.Pop();

            Arg2.Execute(context, stack);
            var groupId = Convert.ToInt32(stack.Pop());

            var items = item.InstalledItems.Where(x => x.GroupId == groupId);
            stack.Push(items.ToArray());
        }
    }
}
