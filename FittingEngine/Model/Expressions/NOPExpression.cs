using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    [Serializable()]
    public class NOPExpression : IExpression
    {
        public void Execute(IContext context, Stack<object> stack)
        {
            return;
        }
    }
}
