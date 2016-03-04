using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    public class GetTypeIdExpression : UnaryExpression
    {
        public GetTypeIdExpression(IExpression arg1) : base(arg1)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var item = (Item) stack.Pop();

            stack.Push(item.TypeId);
        }
    }
}
