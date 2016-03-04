namespace CrestSharp.Model
{
    public interface ICrestDogmaAttribute : ICrestNameIdObject
    {
        string Description { get; }

        int UnitId { get; }

        int IconId { get; }

        bool HighIsGood { get; }

        bool Stackable { get; }

        bool Published { get; }

        double DefaultValue { get; }
    }
}
