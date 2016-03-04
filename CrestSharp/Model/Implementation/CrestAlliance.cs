using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class CrestAlliance : CrestNameIdIconObject, ICrestAlliance
    {
        private ICollection<ICrestCorporation> _corporations;
        private bool? _deleted;
        private string _shortName;

        public bool Deleted => EnsureLoadedValue(ref _deleted);

        public string ShortName => EnsureLoadedValue(ref _shortName);

        public ICollection<ICrestCorporation> Corporations => EnsureLoadedValue(ref _corporations);
    }
}
