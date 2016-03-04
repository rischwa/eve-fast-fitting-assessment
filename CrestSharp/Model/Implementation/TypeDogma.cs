using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class TypeDogma : ITypeDogma
    {
        public ICollection<ITypeDogmaAttribute> Attributes { get; set; }

        public ICollection<ITypeDogmaEffect> Effects { get; set; }
    }
}
