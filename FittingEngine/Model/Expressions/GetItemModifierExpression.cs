using System;
using System.Collections.Generic;
using System.Linq;

namespace FittingEngine.Model.Expressions
{
    public class GetItemModifierExpression : BinaryExpression
    {
        public GetItemModifierExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var modifierType = (string) stack.Pop();

            Arg2.Execute(context, stack);
            var targets = stack.Pop();

            var target = targets as Attribute;
            if (target != null)
            {
                stack.Push(GetTargetModifierList(target, modifierType));
                return;
            }

            stack.Push(((IEnumerable<IAttribute>) targets).Select(x => GetTargetModifierList(x, modifierType))
                                                         .ToArray());
        }

        private static IAssociations GetTargetModifierList(IAttribute target, string modifierType)
        {
            switch (modifierType)
            {
                case "ModAdd":
                    return target.AdditionAssociations;
                case "ModSub":
                    return target.SubstractionAssociations;
                case "PostAssignment":
                    return target.PostAssignmentAssociations;
                case "PostDiv":
                    return target.PostDivisionAssociations;
                case "PostMul":
                    return target.PostMultiplicationAssociations;
                case "PostPercent":
                    return target.PostPercentAssociations;
                case "PreAssignment":
                    return target.PreAssignmentAssociations;
                case "PreDiv":
                    return target.PreDivisionAssociations;
                case "PreMul":
                    return target.PreMultiplicationAssociations;
                case "9":
                    //skilllevel modifier
                    return new DummyAssociations();
                default://TODO richtiger exceptiontyp
                    throw new Exception("unknown modifier type: " + modifierType);
            }
        }
    }
}
