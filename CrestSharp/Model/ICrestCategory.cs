using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestCategory : ICrestNameIdObject
    {
        bool Published { get; }

        ICollection<ICrestGroup> Groups { get; }
    }
}
