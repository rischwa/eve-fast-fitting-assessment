using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    public class SpliceExpression : BinaryExpression
    {
        public SpliceExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            Arg2.Execute(context, stack);
        }
    }
}
