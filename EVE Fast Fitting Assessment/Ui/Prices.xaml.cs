using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CrestSharp;
using EVE_Fast_Fitting_Assessment.Annotations;
using EVE_Fast_Fitting_Assessment.Utilities;
using Serilog;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    /// <summary>
    ///     Interaction logic for Prices.xaml
    /// </summary>
    public sealed partial class Prices : UserControl, INotifyPropertyChanged
    {
        private IAuthenticatedCrest _authenticatedCrest;
        private Visibility _navigationVisibility;

        public Prices()
        {
            InitializeComponent();
            NavigationVisibility = Visibility.Hidden;

            App.LoggedIntoCrest += AppOnLoggedIntoCrest;
            App.LoggedOutFromCrest += AppOnLoggedOutFromCrest;
        }

        public Visibility NavigationVisibility
        {
            get { return _navigationVisibility; }
            private set
            {
                if (value == _navigationVisibility)
                {
                    return;
                }
                _navigationVisibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AppOnLoggedIntoCrest(IAuthenticatedCrest authenticatedCrest)
        {
            Application.Current.Dispatcher.Invoke(
                                                  () =>
                                                  {
                                                      _authenticatedCrest = authenticatedCrest;
                                                      NavigationVisibility = Visibility.Visible;
                                                  });
        }

        private void AppOnLoggedOutFromCrest()
        {
            Application.Current.Dispatcher.Invoke(
                                                  () =>
                                                  {
                                                      _authenticatedCrest = null;
                                                      NavigationVisibility = Visibility.Hidden;
                                                  });
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Jita_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetNavigationTarget(EveConstants.JITA_ID);
        }

        private void SetNavigationTarget(int systemId)
        {
            Task.Factory.StartNew(
                                  async () =>
                                  {
                                      var system = await App.CREST.Map.FetchSolarSystemAsync(systemId);
                                      await _authenticatedCrest.SetWaypointAsync(system);

                                      return system;
                                  })
                .Unwrap()
                .ContinueWith(
                              t =>
                              {
                                  if (t.IsFaulted)
                                  {
                                      Log.Logger.Warning(t.Exception, "Error setting navigation target");
                                      MessageBox.Show(
                                                      $"Could not set navigation target: {t.Exception?.InnerExceptions?.FirstOrDefault() ?.Message ?? "unknown reason"}",
                                                      "Error");
                                  }
                                  else
                                  {
                                      MessageBox.Show( $"Successfully set navigation target to {t.Result.Name}");
                                  }
                              });
        }

        private void Rens_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetNavigationTarget(EveConstants.RENS_ID);
        }

        private void Hek_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetNavigationTarget(EveConstants.HEK_ID);
        }

        private void Dodixie_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetNavigationTarget(EveConstants.DODIXIE_ID);
        }

        private void Amarr_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetNavigationTarget(EveConstants.AMARR_ID);
        }
    }
}
