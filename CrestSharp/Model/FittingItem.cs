namespace CrestSharp.Model
{
    public class FittingItem
    {
        public FittingType Type { get; set; }

        public int Flag { get; set; }

        public int Quantity { get; set; }

        public static FittingItem FromVictimItem(IVictimItem victimItem)
        {
            return new FittingItem()
                   {
                       Flag = victimItem.Flag,
                       Quantity = victimItem.QuantityDestroyed + victimItem.QuantityDropped,
                       Type = FittingType.FromCrestType(victimItem.ItemType)
                   };
        }
    }
}