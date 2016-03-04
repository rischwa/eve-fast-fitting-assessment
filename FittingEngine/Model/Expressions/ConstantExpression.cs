using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    [Serializable()]
    internal class ConstantExpression : IExpression
    {
        private readonly object _value;

        public ConstantExpression(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _value = value;
        }

        public void Execute(IContext context, Stack<object> stack)
        {
            stack.Push(_value);
        }
    }
}
