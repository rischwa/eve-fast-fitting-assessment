using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class CrestRegion : CrestImplicitIdNameObject, ICrestRegion
    {
        private ICollection<ICrestConstellation> _constellations;
        private string _description;

        public string Description
        {
            get { return EnsureLoadedValue(ref _description); }
            set { _description = value; }
        }

        public ICollection<ICrestConstellation> Constellations
        {
            get { return EnsureLoadedValue(ref _constellations); }
            set { _constellations = value; }
        }
    }
}
