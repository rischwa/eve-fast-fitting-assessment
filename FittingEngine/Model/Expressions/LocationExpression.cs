using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    [Serializable()]
    public abstract class UnaryExpression : IExpression
    {
        protected readonly IExpression Arg1;

        protected UnaryExpression(IExpression arg1)
        {
            Arg1 = arg1;
        }

        public abstract void Execute(IContext context, Stack<object> stack);
    }
    [Serializable()]
    public class LocationExpression : IExpression
    {
        private readonly string _location;

        public LocationExpression(string location)
        {
            _location = location;
        }

        public void Execute(IContext context, Stack<object> stack)
        {
            switch (_location)
            {
                case "Area":
                    stack.Push(context.Area);
                    return;
                case "Char":
                    stack.Push(context.Char);
                    return;
                case "Other":
                    stack.Push(context.Other);
                    return;
                case "Self":
                    stack.Push(context.Self);
                    return;
                case "Ship":
                    stack.Push(context.Ship);
                    return;
                case "Target":
                    stack.Push(context.Target);
                    return;
            }
        }
    }
}
