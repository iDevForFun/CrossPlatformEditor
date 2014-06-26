using System.Globalization;
using System.Windows.Input;

namespace WindowsEditor.Wpf.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _title;
        private string _tbText;
        private ICommand _buttonCommand;
        private int counter;

        public MainViewModel()
        {
            Title = "Our nice windows editor";
            ButtonCommand = new RelayCommand(_ => OnClick()); 
        }

        private void OnClick()
        {
            counter++;
            TbText = "hop " + counter.ToString(CultureInfo.InvariantCulture);
        }


        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonCommand
        {
            get { return _buttonCommand; }
            set
            {
                if (Equals(value, _buttonCommand)) return;
                _buttonCommand = value;
                OnPropertyChanged();
            }
        }

        public string TbText
        {
            get { return _tbText; }
            set
            {
                if (value == _tbText) return;
                _tbText = value;
                OnPropertyChanged();
            }
        }
    }
}
