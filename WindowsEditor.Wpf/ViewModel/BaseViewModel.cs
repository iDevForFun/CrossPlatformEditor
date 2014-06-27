using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WindowsEditor.Wpf.Annotations;

namespace WindowsEditor.Wpf.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}