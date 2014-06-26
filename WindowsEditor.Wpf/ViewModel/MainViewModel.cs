using System.Drawing;
using System.Windows.Input;
using CrossPlatformLogic;
using Microsoft.Win32;

namespace WindowsEditor.Wpf.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand buttonCommand;
        private ImageLoader imageLoader;
        private string tbText;
        private string title;
        private Image image;
        private RelayCommand flipCommand;

        public MainViewModel()
        {
            Title = "Our nice windows editor";
            imageLoader = new ImageLoader();
            ButtonCommand = new RelayCommand(_ => SelectImage());
            FlipCommand=  new RelayCommand(_ => Flip(), _ => Image != null);
        }

        private void Flip()
        {
            if (FlipCommand.CanExecute(null))
            {
                Image = imageLoader.FlipHorizontal(Image);    
            }
            
        }


        public string Title
        {
            get { return title; }
            set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonCommand
        {
            get { return buttonCommand; }
            set
            {
                if (Equals(value, buttonCommand)) return;
                buttonCommand = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get { return tbText; }
            set
            {
                if (value == tbText) return;
                tbText = value;
                OnPropertyChanged();
            }
        }

        private void SelectImage()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };
            var result = dlg.ShowDialog();

            if (result != true) return;


            string filename = dlg.FileName;
            FilePath = filename;
            var img = imageLoader.LoadImage(filename);
            if (img != null)
            {
                Image = img;
            }
        }

        public RelayCommand FlipCommand
        {
            get { return flipCommand; }
            set
            {
                if (Equals(value, flipCommand)) return;
                flipCommand = value;
                OnPropertyChanged();
            }
        }

        public Image Image
        {
            get { return image; }
            set
            {
//                if (Equals(value, image)) return;
                image = value;
                FlipCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }
    }
}