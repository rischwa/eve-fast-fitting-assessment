using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class CrestCategory : CrestImplicitIdNameObject, ICrestCategory
    {
        private ICollection<ICrestGroup> _groups;
        private bool? _published;

        public bool Published => EnsureLoadedValue(ref _published);

        public ICollection<ICrestGroup> Groups => EnsureLoadedValue(ref _groups);
    }
}
