using System;
using System.Collections.Generic;

namespace FittingEngine.Model.Expressions
{
    class RemoveModifierExpression : BinaryExpression
    {
        public RemoveModifierExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var targets = (IEnumerable<IAssociations>)stack.Pop();

            Arg2.Execute(context, stack);
            var sourceAttributeId = Convert.ToInt32(stack.Pop());


            IAttribute sourceAttribute;
            if (!context.Self.TryGetAttributeById(sourceAttributeId, out sourceAttribute))
            {
                return;
                //TODO hier sollte besser ein fehler entstehen, weil sonst fehlende dinge uebersehen werden
                //oder irgednwie loggen zumindest

                //TODO exception type -- code duplication
                throw new Exception(string.Format("Missing attribute {0} for item {1}/{2}", sourceAttributeId, context.Self.TypeName, context.Self.TypeId));
            }

            foreach (var curTarget in targets)
            {
                curTarget.Remove(sourceAttribute);
            }
        }
    }
}
