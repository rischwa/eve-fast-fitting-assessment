using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    public class GreaterEqualExpression : BinaryExpression
    {
        public GreaterEqualExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var firstValue = Convert.ToDouble(stack.Pop());

            Arg2.Execute(context, stack);
            var secondValue = Convert.ToDouble(stack.Pop());

            stack.Push(firstValue >= secondValue);
        }
    }
}
