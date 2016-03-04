using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    class AddAttributeValueExpression : BinaryExpression
    {
        public AddAttributeValueExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var targets = (IEnumerable<IAttribute>) stack.Pop();

            Arg2.Execute(context, stack);
            var changeValueAttributeId = Convert.ToInt32(stack.Pop());

            var changeValueAttribute = context.Self.GetAttributeById(changeValueAttributeId);
            foreach (var curTarget in targets)
            {
                curTarget.AdditionAssociations.Add(changeValueAttribute);
            }
        }
    }
}
