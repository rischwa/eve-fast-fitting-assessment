using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestGroup : ICrestNameIdObject
    {
        ICrestCategory Category { get; }

        string Description { get; }

        ICollection<ICrestType> Types { get; }

        bool Published { get; }
    }
}