namespace CrestSharp.Model.Implementation
{
    public abstract class CrestNameIdIconObject : CrestNameIdObject, ICrestNameIdIconObject
    {
        private IUriResource _icon;

        public IUriResource Icon
        {
            get { return EnsureLoadedValue(ref _icon); }
            set { _icon = value; }
        }
    }
}
