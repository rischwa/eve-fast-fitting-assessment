using System;
using System.Collections.Generic;
using Serilog;

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

            if (!item.ContainsAttributeId(attributeId))
            {
                Log.Logger.Warning($"missing attributeid: {attributeId}, item: {item.TypeName} / {item.TypeId}");
                stack.Push(0.0);
                return;
            }
            var attribute = item.GetAttributeById(attributeId);
            stack.Push(attribute.Value);
        }
    }
}
