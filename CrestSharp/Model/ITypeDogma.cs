using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ITypeDogma
    {
        ICollection<ITypeDogmaAttribute> Attributes { get; }

        ICollection<ITypeDogmaEffect> Effects { get; }
    }
}