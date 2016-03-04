using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class CrestSolarSystem : CrestNameIdPositionObject, ICrestSolarSystem
    {
        private string _securityClass;
        private double? _securityStatus;
        private ICollection<ICrestPlanet> _planets;
        private ICrestAlliance _sovereignty;
        private ICrestConstellation _constellation; 
        

        public double SecurityStatus
        {
            get { return EnsureLoadedValue(ref _securityStatus); }
            protected set { _securityStatus = value; }
        }

        public string SecurityClass
        {
            get { return EnsureLoadedValue(ref _securityClass); }
            set { _securityClass = value; }
        }

        public ICollection<ICrestPlanet> Planets
        {
            get { return EnsureLoadedValue(ref _planets); }
            set { _planets = value; }
        }

        public ICrestAlliance Sovereignty
        {
            get { return EnsureLoadedValue(ref _sovereignty); }
            set { _sovereignty = value; }
        }

        public ICrestConstellation Constellation
        {
            get { return EnsureLoadedValue(ref _constellation); }
            set { _constellation = value; }
        }
    }
}