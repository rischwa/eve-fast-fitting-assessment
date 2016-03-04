namespace CrestSharp.Model.Implementation
{
    public class CrestMoon : CrestNameIdObject, ICrestMoon
    {
        private IVector3D _position;
        private ICrestSolarSystem _solarSystem;
        private ICrestType _type;

        public IVector3D Position
        {
            get { return EnsureLoadedValue(ref _position); }
            set { _position = value; }
        }

        public ICrestType Type
        {
            get { return EnsureLoadedValue(ref _type); }
            set { _type = value; }
        }

        public ICrestSolarSystem System
        {
            get { return EnsureLoadedValue(ref _solarSystem); }
            set { _solarSystem = value; }
        }
    }
}
