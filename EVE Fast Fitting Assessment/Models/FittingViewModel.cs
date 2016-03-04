using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EVE_Fast_Fitting_Assessment.Ui;

namespace EVE_Fast_Fitting_Assessment.Models
{
    public sealed class FittingViewModel : DispatcherNotifyPropertyChanged
    {
        private TypeViewModel _ship;



        public TypeViewModel Ship
        {
            get { return _ship; }
            set
            {
                if (Equals(value, _ship))
                {
                    return;
                }
                _ship = value;

                OnPropertyChanged();
                Task.Factory.StartNew(() => { Ship.Type?.EnsureInitialization(); },TaskCreationOptions.LongRunning)
                    .ContinueInDispatcherAsyncWith(
                                                   t =>
                                                   {
                                                       OnPropertyChanged(nameof(HighSlotBackgroundImage));
                                                       OnPropertyChanged(nameof(LowSlotBackgroundImage));
                                                       OnPropertyChanged(nameof(MediumSlotBackgroundImage));
                                                       OnPropertyChanged(nameof(SubsystemBackgroundImage));
                                                       OnPropertyChanged(nameof(RigSlotBackgroundImage));
                                                   });
            }
        }

        public string HighSlotBackgroundImage
            => _ship == null ? null : Path.GetFullPath($"Resources/{((double?)Ship.Type?.Dogma?.Attributes?.FirstOrDefault(x => x.Attribute.Name == "hiSlots") ?.Value).GetValueOrDefault()}h.png");

        public string LowSlotBackgroundImage
            => _ship == null ? null : Path.GetFullPath($"Resources/{((double?)Ship.Type?.Dogma?.Attributes?.FirstOrDefault(x => x.Attribute.Name == "lowSlots")?.Value).GetValueOrDefault()}l.png");

        public string MediumSlotBackgroundImage
            => _ship == null ? null : Path.GetFullPath($"Resources/{((double?)Ship?.Type?.Dogma?.Attributes?.FirstOrDefault(x => x.Attribute.Name == "medSlots")?.Value).GetValueOrDefault()}m.png");
        public string RigSlotBackgroundImage
          => _ship == null ? null : Path.GetFullPath($"Resources/{((double?)Ship?.Type?.Dogma?.Attributes?.FirstOrDefault(x => x.Attribute.Name == "rigSlots")?.Value).GetValueOrDefault()}r.png");

        public string SubsystemBackgroundImage =>  $"Resources/{((double?)Ship?.Type?.Dogma?.Attributes?.FirstOrDefault(x => x.Attribute.Name == "maxSubSystems")?.Value).GetValueOrDefault()}s.png";

        public SlotViewModel HighSlots { get; } = new SlotViewModel(8);

        public SlotViewModel MediumSlots { get; } = new SlotViewModel(8);

        public SlotViewModel LowSlots { get; } = new SlotViewModel(8);

        public SlotViewModel RigSlots { get; } = new SlotViewModel(3);

        public SlotViewModel SubSystemSlots { get; } = new SlotViewModel(5);
    }
}