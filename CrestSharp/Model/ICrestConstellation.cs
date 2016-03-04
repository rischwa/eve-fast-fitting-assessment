using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestConstellation : ICrestNameIdObject
    {
        IVector3D Position { get; }

        ICrestRegion Region { get; }

        ICollection<ICrestSolarSystem> Systems { get; }
    }
}