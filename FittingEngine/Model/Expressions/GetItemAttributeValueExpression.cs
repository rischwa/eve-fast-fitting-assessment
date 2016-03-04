using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    internal class GetItemAttributeValueExpression : BinaryExpression
    {
        public GetItemAttributeValueExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var item = (Item) stack.Pop();

            Arg2.Execute(context, stack);
            var attributeId = Convert.ToInt32(stack.Pop());

            var attribute = item.GetAttributeById(attributeId);
            stack.Push(attribute.Value);
        }
    }
}
