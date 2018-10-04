using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Melo.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private ICommand _gotoDirectoriesCommand;
        private ICommand _gotoPicturesCommand;
        private ICommand _gotoMusicsCommand;
        private ICommand _gotoVideosCommand;
        private object _currentView;
        private object directories;
        private object musics;
        private object pictures;
        private object videos;


        public MainWindowViewModel()
        {
            directories = new Directories();
            pictures = new Pictures();
            musics = new Musics();
            videos = new Videos();

            CurrentView = directories;
        }

        public object GotoDirectoriesCommand
        {
            get
            {
                return _gotoDirectoriesCommand ?? (_gotoDirectoriesCommand = new RelayCommand(
                   x =>
                   {
                       GotoDirectories();
                   }));
            }
        }

        public ICommand GotoPicturesCommand
        {
            get
            {
                return _gotoPicturesCommand ?? (_gotoPicturesCommand = new RelayCommand(
                   x =>
                   {
                       GotoPictures();
                   }));
            }
        }

        public ICommand GotoMusicsCommand
        {
            get
            {
                return _gotoMusicsCommand ?? (_gotoMusicsCommand = new RelayCommand(
                   x =>
                   {
                       GotoMusics();
                   }));
            }
        }

        public ICommand GotoVideosCommand
        {
            get
            {
                return _gotoVideosCommand ?? (_gotoVideosCommand = new RelayCommand(
                   x =>
                   {
                       GotoVideos();
                   }));
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private void GotoDirectories()
        {
            CurrentView = new Directories();
        }

        private void GotoPictures()
        {
            CurrentView = new Pictures();
        }

        private void GotoMusics()
        {
            CurrentView = new Musics();
        }

        private void GotoVideos()
        {
            CurrentView = new Videos();
        }
    }
}
