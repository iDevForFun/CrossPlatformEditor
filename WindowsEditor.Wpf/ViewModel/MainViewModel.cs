using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using CrossPlatformLogic;
using CrossPlatformLogic.Network;
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
        private ObservableCollection<string> imagesList;
        private string selectedImagePath;
        private bool isEditorModeOn;
        private RelayCommand editModeCommand;
        private RelayCommand rotateCommand;

        public MainViewModel()
        {
            Title = "Our nice windows editor";
            imageLoader = new ImageLoader();
            networkClient = new NetworkClient();
            ImagesList = new ObservableCollection<string>(imageLoader.Images);
            SelectedImagePath = ImagesList.First();
            InitCommands();
            Listen();
        }

        private void InitCommands()
        {
            ButtonCommand = new RelayCommand(_ => SelectImage(SelectedImagePath, true));
            FlipCommand = new RelayCommand(_ => Flip(true), _ => (Image != null && IsEditorModeOn));
            RotateCommand = new RelayCommand(_ => Rotate(true), _ => (Image != null && IsEditorModeOn));
            EditModeCommand = new RelayCommand(state => SwtichEditorState(true));
        }


        public RelayCommand RotateCommand
        {
            get { return rotateCommand; }
            set
            {
                if (Equals(value, rotateCommand)) return;
                rotateCommand = value;
                OnPropertyChanged();
            }
        }


        private void Listen()
        {
            networkClient.OnNetworkEvent()
                .Subscribe(networkEvent =>
            {
                Debug.WriteLine("network event recieved, event type:{0}", Enum.GetName(typeof(EventType),networkEvent.Type));
                switch (networkEvent.Type)
                {
                    case EventType.Loaded:
                        var filename = networkEvent.Data;
                        SelectedImagePath = filename;
                        SelectImage(filename, false);
                        break;
                    case EventType.Flip:
                        Flip(false);
                        break;
                    case EventType.Lock:
                        SwtichEditorState(false);
                        break;
                    case EventType.Rotate:
                        break;
                }
            });
        }

        private void SwtichEditorState(bool report)
        {
            IsEditorModeOn = !IsEditorModeOn;
            if(report) networkClient.ReportLock();
        }


        private void Flip(bool report)
        {
            if (FlipCommand.CanExecute(null))
            {
                Image = imageLoader.FlipHorizontal(Image);  
                if(report) networkClient.ReportFlip();
            }
        }

        private void Rotate(bool report)
        {
            if (RotateCommand.CanExecute(null))
            {
                Image = imageLoader.Rotate(Image);
                if (report) networkClient.ReportRotate();
            }
        }

        public RelayCommand EditModeCommand
        {
            get { return editModeCommand; }
            set
            {
                if (Equals(value, editModeCommand)) return;
                editModeCommand = value;
                OnPropertyChanged();
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

        public ObservableCollection<string> ImagesList
        {
            get { return imagesList; }
            set
            {
                if (Equals(value, imagesList)) return;
                imagesList = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditorModeOn
        {
            get { return isEditorModeOn; }
            set
            {
                if (value.Equals(isEditorModeOn)) return;
                FlipCommand.RaiseCanExecuteChanged();
                isEditorModeOn = value;
                OnPropertyChanged();
            }
        }

        public string SelectedImagePath
        {
            get { return selectedImagePath; }
            set
            {
                if (value == selectedImagePath) return;
                selectedImagePath = value;
                OnPropertyChanged();
            }
        }

        private void SelectImage(string filename, bool report)
        {
            if(string.IsNullOrWhiteSpace(filename)) filename = "RSV4-1.jpg";
            FilePath = filename;
            var img = imageLoader.LoadImage(filename);
            if (img != null)
            {
                Image = img;
                if(report) networkClient.ReportLoaded(filename);
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
                image = value;
                FlipCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }
    }
}