using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EVE_Fast_Fitting_Assessment.Annotations;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    public abstract class DispatcherNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Application.Current.Dispatcher.InvokeAsync(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        protected void UpdateProperty<T>(T value, ref T variable, [CallerMemberName] string propertyName = null)
        {
            if (Equals(value, variable))
            {
                return;
            }
            variable = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);
        }
    }
}
