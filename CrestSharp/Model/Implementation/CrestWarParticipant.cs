using System;

namespace CrestSharp.Model.Implementation
{
    public class CrestWarParticipant : CrestNameIdIconObject, ICrestWarParticipant
    {
        private long? _iskKilled;
        private int? _shipsKilled;

        public int ShipsKilled => EnsureLoadedValue(ref _shipsKilled);

        public long IskKilled => EnsureLoadedValue(ref _iskKilled);

        public bool IsCorporation => Href.Contains("corporations");

        public bool IsAlliance => !IsCorporation;

        public ICrestAlliance AsAlliance()
        {
            if (!IsAlliance)
            {
                throw new Exception("Cannot convert corporation to alliance");
            }
            return new CrestAlliance
                   {
                       Href = Href,
                       Icon = Icon,
                       Id = Id,
                       Name = Name
                   };
        }

        public ICrestCorporation AsCorporation()
        {
            if (!IsCorporation)
            {
                throw new Exception("Cannot convert alliance to corporation");
            }

            return new CrestCorporation
                   {
                       Href = Href,
                       Icon = Icon,
                       Id = Id,
                       Name = Name
                   };
        }
    }
}
