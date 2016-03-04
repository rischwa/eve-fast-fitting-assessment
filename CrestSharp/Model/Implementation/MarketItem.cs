using System;

namespace CrestSharp.Model.Implementation
{
    public class MarketItem : CrestObject, IMarketItem
    {
        private bool? _buy;
        private int? _duration;
        private long? _id;
        private DateTime? _issued;
        //TODO der mapping kram kann evtl. getoetet werden, wenn man ueberall konkrete typen bneutzt
        private ICrestLocation _location;
        private long? _minVolume;
        private double? _price;
        private string _range;
        private ICrestType _type;
        private long? _volume;
        private long? _volumeEntered;

        public long Id => EnsureLoadedValue(ref _id);

        public DateTime Issued => EnsureLoadedValue(ref _issued);

        public double Price => EnsureLoadedValue(ref _price);

        public long VolumeEntered => EnsureLoadedValue(ref _volumeEntered);

        public long MinVolume => EnsureLoadedValue(ref _minVolume);

        public long Volume => EnsureLoadedValue(ref _volume);

        public string Range => EnsureLoadedValue(ref _range);

        public ICrestLocation Location => EnsureLoadedValue(ref _location);

        public int Duration => EnsureLoadedValue(ref _duration);

        public ICrestType Type => EnsureLoadedValue(ref _type);

        public bool IsSell => !IsBuy;

        public bool IsBuy => EnsureLoadedValue(ref _buy);
    }
}