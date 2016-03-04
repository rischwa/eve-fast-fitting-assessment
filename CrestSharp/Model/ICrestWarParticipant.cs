namespace CrestSharp.Model
{
    public interface ICrestWarParticipant : ICrestNameIdIconObject
    {
        int ShipsKilled { get; }
        long IskKilled { get; }

        bool IsCorporation { get; }

        bool IsAlliance { get; }

        ICrestAlliance AsAlliance();

        ICrestCorporation AsCorporation();
    }
}