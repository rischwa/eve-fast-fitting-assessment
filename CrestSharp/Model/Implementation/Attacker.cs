namespace CrestSharp.Model.Implementation
{
    public class Attacker : IAttacker
    {
        public ICrestType ShipType { get; set; }

        public ICrestCorporation Corporation { get; set; }

        public ICrestCharacter Character { get; set; }

        public ICrestAlliance Alliance { get; set; }

        public ICrestType WeaponType { get; set; }

        public double SecurityStatus { get; set; }

        public bool FinalBlow { get; set; }

        public int DamageDone { get; set; }
    }
}
