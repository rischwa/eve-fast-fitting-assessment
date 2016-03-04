using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    [Serializable()]
    public abstract class BinaryExpression : IExpression
    {
        protected readonly IExpression Arg1;
        protected readonly IExpression Arg2;

        protected BinaryExpression(IExpression arg1, IExpression arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }

        public abstract void Execute(IContext context, Stack<object> stack);
    }
}