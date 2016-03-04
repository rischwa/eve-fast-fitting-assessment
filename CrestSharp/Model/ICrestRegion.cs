using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestRegion : ICrestNameIdObject
    {
        string Description { get; }

        ICollection<ICrestConstellation> Constellations { get; }
    }
}