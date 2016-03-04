namespace CrestSharp.Model.Implementation
{
    public class VictimItem : IVictimItem
    {
        public const int FLAG_SUBSYSTEM_SLOT_0 = 125;
        public const int FLAG_SUBSYSTEM_SLOT_7 = 132;
        public const int FLAG_RIG_SLOT_0 = 92;
        public const int FLAG_RIG_SLOT_7 = 99;
        public const int FLAG_LOW_SLOT_0 = 11;
        public const int FLAG_LOW_SLOT_7 = 18;
        public const int FLAG_MED_SLOT_0 = 19;
        public const int FLAG_MED_SLOT_7 = 26;
        public const int FLAG_HIGH_SLOT_0 = 27;
        public const int FLAG_HIGH_SLOT_7 = 34;

        public bool Singleton { get; set; }

        public ICrestType ItemType { get; set; }

        public int QuantityDestroyed { get; set; }

        public int QuantityDropped { get; set; }

        public int Flag { get; set; }

        public bool IsFittedToHighSlot => Flag >= FLAG_HIGH_SLOT_0 && Flag <= FLAG_HIGH_SLOT_7;

        public bool IsFittedToLowSlot => Flag >= FLAG_LOW_SLOT_0 && Flag <= FLAG_LOW_SLOT_7;

        public bool IsFittedToMediumSlot => Flag >= FLAG_MED_SLOT_0 && Flag <= FLAG_MED_SLOT_7;

        public bool IsFittedToRigSlot => Flag >= FLAG_RIG_SLOT_0 && Flag <= FLAG_RIG_SLOT_7;

        public bool IsFittedAsSubSystem => Flag >= FLAG_SUBSYSTEM_SLOT_0 && Flag <= FLAG_SUBSYSTEM_SLOT_7;
    }
}
