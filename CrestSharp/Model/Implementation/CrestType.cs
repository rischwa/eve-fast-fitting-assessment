namespace CrestSharp.Model.Implementation
{
    public class CrestType : CrestImplicitIdNameObject, ICrestType
    {
        private double? _capacity;
        private int? _iconId;
        private double? _mass;
        private int? _portionSize;
        private bool? _published;
        private double? _radius;
        private double? _volume;
        private ITypeDogma _dogma;

        public double Volume
        {
            get { return EnsureLoadedValue(ref _volume); }
            set { _volume = value; }
        }

        public double Radius
        {
            get { return EnsureLoadedValue(ref _radius); }
            set { _radius = value; }
        }

        public bool Published
        {
            get { return EnsureLoadedValue(ref _published); }
            set { _published = value; }
        }

        public double Mass
        {
            get { return EnsureLoadedValue(ref _mass); }
            set { _mass = value; }
        }

        public int IconId
        {
            get { return EnsureLoadedValue(ref _iconId); }
            set { _iconId = value; }
        }

        public double Capacity
        {
            get { return EnsureLoadedValue(ref _capacity); }
            set { _capacity = value; }
        }

        public int PortionSize
        {
            get { return EnsureLoadedValue(ref _portionSize); }
            set { _portionSize = value; }
        }

        public ITypeDogma Dogma {
            get { return EnsureLoadedValue(ref _dogma); }
            set { _dogma = value; }
        }

        public string Icon64X64Url => $"https://imageserver.eveonline.com/Type/{Id}_64.png";
    }
}
