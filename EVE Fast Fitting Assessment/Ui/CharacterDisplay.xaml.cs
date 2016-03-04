using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CrestSharp;
using CrestSharp.Model;
using Serilog;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    /// <summary>
    ///     Interaction logic for CharacterDisplay.xaml
    /// </summary>
    public partial class CharacterDisplay : UserControl
    {
        private bool _isLoggingIn;

        private const double WIDTH = 180.0;
        private static readonly TimeSpan ANIMATION_SPEED = TimeSpan.FromMilliseconds(1000);
        private IAuthenticatedCrest _authenticatedCrest;
        private Fitting _fitting;

        public CharacterDisplay()
        {
            InitializeComponent();
            App.LoggedIntoCrest += AppOnLoggedIntoCrest;
            App.LoggedOutFromCrest += AppOnLoggedOutFromCrest;
        }

        private void AppOnLoggedOutFromCrest()
        {
            Application.Current.Dispatcher.Invoke(() => _authenticatedCrest = null);
        }

        private void AppOnLoggedIntoCrest(IAuthenticatedCrest authenticatedcrest)
        {
            Application.Current.Dispatcher.Invoke(() => _authenticatedCrest = authenticatedcrest);
        }

        public async void Login()
        {
            if (_isLoggingIn)
            {
                return;
            }
            _isLoggingIn = true;

            try
            { 
                var isLoggedIn = await App.LoginToCrest();
                if (isLoggedIn)
                {
                    IsLoggedIn();
                    return;
                }
               
                MessageBox.Show("Login was unsuccessful, please try again", "Warning");
            }
            finally
            {
                _isLoggingIn = false;
            }
        }

        public void SetFitting(Fitting fitting)
        {
            Application.Current.Dispatcher.Invoke(
                                          () =>
                                          {
                                              _fitting = fitting;
                                              BtnSaveFitting.Visibility = _fitting != null ? Visibility.Visible : Visibility.Hidden ;
                                          });
        }

        public void IsLoggedIn()
        {
            AnimateWidth(0, WIDTH);
        }

        private void AnimateWidth(double from, double to)
        {
            var storyboard = new Storyboard();
            var widthAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                AutoReverse = false,
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseOut
                },
                Duration = new Duration(ANIMATION_SPEED)
            };

            Storyboard.SetTarget(widthAnimation, this);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("MaxWidth"));
            storyboard.Children.Add(widthAnimation);

            storyboard.Begin(this);
        }

        private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
        {
            App.LogoutFromCrest();
            AnimateWidth(ActualWidth, 0);
        }

        private async void BtnSaveFitting_OnClick(object sender, RoutedEventArgs ev)
        {
            string name, description;
            if (!SaveFittingDialog.TryGetNameAndDescription(out name, out description))
            {
                return;
            }
            try
            {
                _fitting.Name = name;
                _fitting.Description = description;
                await _authenticatedCrest.SaveFittingAsync(_fitting);

                MessageBox.Show($"Fitting {_fitting.Name} ({_fitting.Ship?.Name}) saved successfully.");
            }
            catch (Exception e)
            {
                Log.Logger.Warning(e, "Error trying to save fitting");
                MessageBox.Show($"Could not save fitting: {e.Message}", "Warning");
            }
        }
    }
}
