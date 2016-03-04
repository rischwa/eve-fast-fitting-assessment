namespace CrestSharp.Model.Implementation
{
    public class CrestPlanet : CrestImplicitIdNameObject, ICrestPlanet
    {
        private IVector3D _position;
        private ICrestSolarSystem _solarSystem;

        public IVector3D Position
        {
            get { return EnsureLoadedValue(ref _position); }
            set { _position = value; }
        }

        public ICrestSolarSystem System
        {
            get { return EnsureLoadedValue(ref _solarSystem); }
            set { _solarSystem = value; }
        }
    }
}
