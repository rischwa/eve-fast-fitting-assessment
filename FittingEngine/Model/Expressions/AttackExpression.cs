using System;
using System.Collections.Generic;
using System.Linq;

namespace FittingEngine.Model.Expressions
{
    public class AttackExpression : UnaryExpression
    {
        public AttackExpression(IExpression arg1) : base(arg1)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);

            var attributeId = Convert.ToInt32(stack.Pop());
            var attribute = context.Self.Attributes.First(x => x.AttributeID == attributeId);
            //damage modifier attribute is das evtl nur, muss also noch anders verrechnet werden???
            //eventuell kann das hier auch einfach  ignoriert werden
            context.Attacks.Add(new Attack(context.Self, attribute.Value));

        }
    }
}
