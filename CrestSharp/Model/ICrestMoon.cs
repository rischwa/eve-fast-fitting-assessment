namespace CrestSharp.Model
{
    public interface ICrestMoon : ICrestNameIdObject
    {
        IVector3D Position { get; }
        ICrestType Type { get; }

        ICrestSolarSystem System { get; }
    }
}