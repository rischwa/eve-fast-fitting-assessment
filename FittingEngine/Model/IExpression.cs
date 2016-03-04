using System.Collections.Generic;

namespace FittingEngine.Model
{
    public interface IExpression
    {
        void Execute(IContext context, Stack<object> stack);
    }
}