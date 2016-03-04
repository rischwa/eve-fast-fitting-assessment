using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class Victim : IVictim
    {
        public Victim()
        {
            Items = new List<IVictimItem>();
        }

        public int DamageTaken { get; set; }

        public ICrestType ShipType { get; set; }

        public IVector3D Position { get; set; }

        public ICrestCorporation Corporation { get; set; }

        public ICrestCharacter Character { get; set; }

        public ICrestAlliance Alliance { get; set; }

        public ICollection<IVictimItem> Items { get; set; }
    }
}
