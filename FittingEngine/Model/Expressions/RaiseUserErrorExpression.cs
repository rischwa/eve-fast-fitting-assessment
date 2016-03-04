using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    [Serializable()]
    public class RaiseUserErrorExpression : UnaryExpression
    {
        public RaiseUserErrorExpression(IExpression arg1) : base(arg1)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var error = (string) stack.Pop();

            context.RaiseUserError(error);
        }
    }
}
