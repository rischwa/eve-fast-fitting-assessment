namespace CrestSharp.Model
{
    public interface ICrestPlanet : ICrestNameIdObject
    {
        IVector3D Position { get; }

        ICrestSolarSystem System { get; }
    }
}