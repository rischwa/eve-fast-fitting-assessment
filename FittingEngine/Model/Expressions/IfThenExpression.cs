using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    public class IfThenExpression : BinaryExpression
    {
        public IfThenExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            //return true, if predicate is true, false otherwise
            if ((bool) stack.Peek())
            {
                Arg2.Execute(context, stack);
            }
        }
    }
}
