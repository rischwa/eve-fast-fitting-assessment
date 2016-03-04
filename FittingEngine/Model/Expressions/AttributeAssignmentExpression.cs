using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    public class AttributeAssignmentExpression : BinaryExpression
    {
        public AttributeAssignmentExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var attributes = (IEnumerable<IAttribute>)stack.Pop();

            Arg2.Execute(context, stack);
            var value = Convert.ToDouble(stack.Pop());

            foreach (var curAttr in attributes)
            {
                curAttr.SetValueForced(value);
            }
        }
    }
}
