using System.Collections.Generic;
using System.Linq;

namespace CrestSharp.Model
{
    public class Fitting
    {
        public Fitting()
        {
            Items = new List<FittingItem>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public FittingType Ship { get; set; }

        public ICollection<FittingItem> Items { get; set; }

        public static Fitting FromCrestKillmail(string name, string description, ICrestKillmail killmail)
        {
            return new Fitting
                   {
                       Name = name,
                       Description = description,
                       Ship = FittingType.FromCrestType(killmail.Victim.ShipType),
                       Items =
                           killmail.Victim.Items.Where(
                                                       item =>
                                                       item.IsFittedAsSubSystem || item.IsFittedToHighSlot || item.IsFittedToLowSlot
                                                       || item.IsFittedToMediumSlot || item.IsFittedToRigSlot)
                           .Select(FittingItem.FromVictimItem)
                           .ToList()
                   };
        }
    }
}