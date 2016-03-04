namespace CrestSharp.Model
{
    public interface IVictimItem
    {
        bool Singleton { get; }

        ICrestType ItemType { get; }

        int QuantityDestroyed { get; }

        int QuantityDropped { get; }

        int Flag { get; }

        bool IsFittedToHighSlot { get; }

        bool IsFittedToLowSlot { get; }
        bool IsFittedToMediumSlot { get; }

        bool IsFittedToRigSlot { get; }

        bool IsFittedAsSubSystem { get; }
    }
}