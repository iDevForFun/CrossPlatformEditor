using System.Drawing;
using System.Windows.Input;
using CrossPlatformLogic;
using CrossPlatformLogic.Network;
using Microsoft.Win32;
using System.Reactive.Linq;
using System;

namespace WindowsEditor.Wpf.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand buttonCommand;
        private readonly ImageLoader imageLoader;
        private readonly INetworkClient networkClient;
        private string tbText;
        private string title;
        private Image image;
        private RelayCommand flipCommand;
        private RelayCommand listenCommand;

        public MainViewModel()
        {
            Title = "Our nice windows editor";
            imageLoader = new ImageLoader();
            networkClient = new NetworkClient();
            ButtonCommand = new RelayCommand(_ => SelectImage());
            FlipCommand=  new RelayCommand(_ => Flip(true), _ => Image != null);
            ListenCommand = new RelayCommand(_ => Listen());

        }


        private void Listen()
        {
            networkClient.OnNetworkEvent()
                .Subscribe(networEvent =>
            {
                if (networEvent.Type == EventType.Flip)
                {
                    Flip(false);
                }
            });
        }

        private void Flip(bool report)
        {
            if (FlipCommand.CanExecute(null))
            {
                Image = imageLoader.FlipHorizontal(Image);  
               if(report) networkClient.ReportFlip();
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

        public RelayCommand ListenCommand
        {
            get { return listenCommand; }
            set
            {
                if (Equals(value, listenCommand)) return;
                listenCommand = value;
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


            var filename = dlg.FileName;
            FilePath = filename;
            var img = imageLoader.LoadImage(filename);
            if (img != null)
            {
                Image = img;
                networkClient.ReportLoaded(filename);
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