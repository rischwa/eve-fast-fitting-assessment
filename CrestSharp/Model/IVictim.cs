using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface IVictim
    {
        int DamageTaken { get; }

        ICrestType ShipType { get; }

        IVector3D Position { get; }

        ICrestCorporation Corporation { get; }

        ICrestCharacter Character { get; }

        ICrestAlliance Alliance { get; }

        ICollection<IVictimItem> Items { get; }
    }
}