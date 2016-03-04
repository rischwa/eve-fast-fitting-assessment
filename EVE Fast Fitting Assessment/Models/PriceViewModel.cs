using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVE_Fast_Fitting_Assessment.Ui;

namespace EVE_Fast_Fitting_Assessment.Models
{
    public class PriceViewModel : DispatcherNotifyPropertyChanged
    {
        private string _amarrPrice = "";
        private string _jitaPrice = "";
        private string _dodixiePrice = "";
        private string _hekPrice = "";
        private string _rensPrice = "";
        

        public string AmarrPrice
        {
            get { return _amarrPrice; }
            set
            {
                if (value.Equals(_amarrPrice))
                {
                    return;
                }
                _amarrPrice = value;
                OnPropertyChanged();
            }
        }

        public string RensPrice

        {
            get { return _rensPrice; }
            set
            {
                if (value.Equals(_rensPrice))
                {
                    return;
                }
                _rensPrice = value;
                OnPropertyChanged();
            }
        }

        public string HekPrice
        {
            get { return _hekPrice; }
            set
            {
                if (value.Equals(_hekPrice))
                {
                    return;
                }
                _hekPrice = value;
                OnPropertyChanged();
            }
        }

        public string DodixiePrice
        {
            get { return _dodixiePrice; }
            set
            {
                if (value.Equals(_dodixiePrice))
                {
                    return;
                }
                _dodixiePrice = value;
                OnPropertyChanged();
            }
        }

        public string JitaPrice
        {
            get { return _jitaPrice; }
            set
            {
                if (value.Equals(_jitaPrice))
                {
                    return;
                }
                _jitaPrice = value;
                OnPropertyChanged();
            }
        }
    }
}
