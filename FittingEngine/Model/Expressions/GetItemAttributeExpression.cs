using System;
using System.Collections.Generic;
using System.Linq;

namespace FittingEngine.Model.Expressions
{
    internal class GetItemAttributeExpression : BinaryExpression

    {
        public GetItemAttributeExpression(IExpression arg1, IExpression arg2) : base(arg1, arg2)
        {
        }

        public override void Execute(IContext context, Stack<object> stack)
        {
            Arg1.Execute(context, stack);
            var items = stack.Pop();

            Arg2.Execute(context, stack);
            var attributeId = Convert.ToInt32(stack.Pop());
            var item = items as Item;
            if (item != null)
            {
                IAttribute attribute;
               
               if (!item.TryGetAttributeById(attributeId, out attribute))
                {
                    stack.Push(new Attribute[0]);
                    return;
                    //TODO ist das richtig, darf das auftreten, dass  es nic passendes gibt? -> enyo test
                    //TODO exception type
                    throw new Exception(string.Format("Missing attribute {0} for item {1}", attributeId, item.TypeName));
                }

                stack.Push(new[] {attribute});
                return;
            }

            var result = ((IEnumerable<Item>) items).Select(
                                                            a =>
                                                            {
                                                                IAttribute attribute;
                                                                a.TryGetAttributeById(attributeId, out attribute);

                                                                return attribute;
                                                            })
                .Where(x => x != null)
                .ToArray();
            stack.Push(result);
        }
    }
}
