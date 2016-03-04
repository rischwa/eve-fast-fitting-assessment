namespace CrestSharp.Model
{
    public interface ICrestNamedObject : ICrestObject
    {
        string Name { get; }
    }
}