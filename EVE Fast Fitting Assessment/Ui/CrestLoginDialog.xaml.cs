using System.Windows;
using System.Windows.Input;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    /// <summary>
    ///     Interaction logic for CrestLoginDialog.xaml
    /// </summary>
    public partial class CrestLoginDialog : Window
    {
        private bool _isLoggingIn = false;

        public CrestLoginDialog()
        {
            InitializeComponent();
        }

        private async void BtnLogin_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isLoggingIn)
            {
                return;
            }
            _isLoggingIn = true;
            BtnLogin.Visibility = Visibility.Hidden;

            try
            {
                var isLoggedIn = await App.LoginToCrest();
                if (isLoggedIn)
                {
                    MessageBox.Show("Successfully logged in, thank you!", "Login Success");
                    Close();
                }

                MessageBox.Show("Login was unsuccessful, please try again");
            }
            finally
            {
                _isLoggingIn = false;
                BtnLogin.Visibility = Visibility.Visible;
            }
        }
    }
}
