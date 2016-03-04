namespace CrestSharp.Model
{
    public interface ICrestType : ICrestNameIdObject
    {
        double Volume { get; }

        double Radius { get; }

        bool Published { get; }

        double Mass { get; }

        int IconId { get; }

        double Capacity { get; }

        int PortionSize { get; }

        ITypeDogma Dogma { get; }

        string Icon64X64Url { get; }
    }
}
