namespace CrestSharp.Model.Implementation
{
    public abstract class CrestNameIdPositionObject : CrestNameIdObject
    {
        private IVector3D _position;

        public IVector3D Position => EnsureLoadedValue(ref _position);
    }
}
