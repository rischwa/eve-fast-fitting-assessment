using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    internal class AndExpression : BinaryExpression
    {
        public AndExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
            //TODO check paramters are boolean expressions?
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            if ((bool) stack.Pop())
            {
                Arg2.Execute(context, stack);
            }
            else
            {
                stack.Push(false);
            }
        }
    }
}
