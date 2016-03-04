using EVE_Fast_Fitting_Assessment.Ui;

namespace EVE_Fast_Fitting_Assessment.Models
{
    public class CharacterViewModel : DispatcherNotifyPropertyChanged
    {
        private string _imageUrl;
        private string _location;
        private string _name;

        public long Id { get; set; }

        public string Name
        {
            get { return _name; }
            set { UpdateProperty(value, ref _name); }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { UpdateProperty(value, ref _imageUrl); }
        }

        public string Location
        {
            get { return _location; }
            set { UpdateProperty(value, ref _location); }
        }
    }
}