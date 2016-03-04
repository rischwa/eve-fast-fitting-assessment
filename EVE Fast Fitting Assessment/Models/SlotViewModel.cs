using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CrestSharp.Model;
using EVE_Fast_Fitting_Assessment.Ui;

namespace EVE_Fast_Fitting_Assessment.Models
{
    public class TypeViewModel : DispatcherNotifyPropertyChanged
    {
        public static readonly TypeViewModel EMPTY_MODEL = new TypeViewModel(null);
        public readonly ICrestType Type;
        private string _price;
        private string _tooltip;

        public TypeViewModel(ICrestType type)
        {
            Type = type;
            Visibility = type?.Icon64X64Url != null ? Visibility.Visible : Visibility.Hidden;
            Tooltip = type?.Name;
            Icon = Type?.Icon64X64Url;
        }

        public string Tooltip
        {
            get { return _tooltip; }
            set
            {
                if (Equals(value, _tooltip))
                {
                    return;
                }
                _tooltip = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Visibility of the module. This is a workaround because after a cachedImage was initialized once,
        ///     it cannot be reset to empty. So changing between fits where a slots becomes unused would otherwise
        ///     lead to old modules being shown.
        /// </summary>
        public Visibility Visibility { get; }

        public string Icon { get; set; }

        public string Name => Type?.Name;

        public string Price
        {
            get { return _price; }
            set
            {
                if (Equals(value, _price))
                {
                    return;
                }
                _price = value;

                OnPropertyChanged();


                Tooltip = $"{Name}\n{_price}";
            }
        }
    }

    public class SlotViewModel : DispatcherNotifyPropertyChanged
    {
        private readonly TypeViewModel[] _slots;

        public SlotViewModel(int maxSlots)
        {
            _slots = new TypeViewModel[maxSlots];
            for (var i = 0; i < maxSlots; ++i)
            {
                _slots[i] = TypeViewModel.EMPTY_MODEL;
            }
        }

        public IReadOnlyCollection<TypeViewModel> Slots
        {
            get
            {
                lock (this)
                {
                    return _slots.ToList()
                        .AsReadOnly();
                }
            }
        }

        [IndexerName("Item")]
        public TypeViewModel this[int index]
        {
            get
            {
                lock (this)
                {
                    return _slots[index];
                }
            }
            set
            {
                lock (this)
                {
                    _slots[index] = value;
                    OnPropertyChanged("Item[]");
                }
            }
        }
    }
}
