using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestSolarSystem : ICrestNameIdObject
    {
        double SecurityStatus { get; }

        string SecurityClass { get; }

        IVector3D Position { get; }

        ICollection<ICrestPlanet> Planets { get; }

        ICrestAlliance Sovereignty { get; }

        ICrestConstellation Constellation { get; }
    }
}
