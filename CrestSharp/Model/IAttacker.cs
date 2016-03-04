namespace CrestSharp.Model
{
    public interface IAttacker
    {
        ICrestType ShipType { get; }

        bool FinalBlow { get; }

        double SecurityStatus { get; }

        int DamageDone { get; }

        ICrestCorporation Corporation { get; }

        ICrestCharacter Character { get; }

        ICrestAlliance Alliance { get; }

        ICrestType WeaponType { get; }
    }

    public interface ICrestCorporation : ICrestNameIdIconObject
    {
    }
}
