using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    public class OrExpression: BinaryExpression
    {

        public OrExpression(IExpression arg1, IExpression arg2) :base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            if ((bool) stack.Pop())
            {
                stack.Push(true);
                return;
            }
            Arg2.Execute(context, stack);
        }
    }
}
