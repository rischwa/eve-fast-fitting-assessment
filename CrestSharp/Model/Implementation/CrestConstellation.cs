using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class CrestConstellation : CrestImplicitIdNameObject, ICrestConstellation
    {
        private IVector3D _position;
        private ICrestRegion _region;
        private ICollection<ICrestSolarSystem> _solarSystems; 

        public IVector3D Position => EnsureLoadedValue(ref _position);

        public ICrestRegion Region => EnsureLoadedValue(ref _region);

        public ICollection<ICrestSolarSystem> Systems => EnsureLoadedValue(ref _solarSystems);
    }
}
