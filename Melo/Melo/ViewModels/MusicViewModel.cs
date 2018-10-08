using System.Windows.Input;
using Melo.ClientEntities;

namespace Melo.ViewModel
{
    public class MusicViewModel: ViewModelBase
    {
        private ICommand _gotoAllMusicsCommand;
        private ICommand _gotoAlbumsCommand;
        private ICommand _gotoArtistsCommand;
        private ICommand _goToListingCommand;
        private ICommand _goToEditMusicCommand;

        private object _currentView;

        public MusicViewModel()
        {
            CurrentView = new MusicList("All");
        }

        public ICommand GoToAllMusicsCommand
        {
            get
            {
                return _gotoAllMusicsCommand ?? (_gotoAllMusicsCommand = new RelayCommand(
                   x =>
                   {
                       GoToAllMusics();
                   }));
            }
        }

        public ICommand GoToAlbumsCommand
        {
            get
            {
                return _gotoAlbumsCommand ?? (_gotoAlbumsCommand = new RelayCommand(
                   x =>
                   {
                       GoToAlbums();
                   }));
            }
        }

        public ICommand GoToArtistsCommand
        {
            get
            {
                return _gotoArtistsCommand ?? (_gotoArtistsCommand = new RelayCommand(
                   x =>
                   {
                       GoToArtists();
                   }));
            }
        }

        public ICommand GoToListingCommand
        {
            get
            {
                return _goToListingCommand ?? (_goToListingCommand = new RelayCommand(
                   x =>
                   {
                       GoToListing(Musics.selectedCategory);
                   }));
            }
        }

        public ICommand GoToEditMusicCommand
        {
            get
            {
                return _goToEditMusicCommand ?? (_goToEditMusicCommand = new RelayCommand(
                   x =>
                   {
                       GoToEditMusic(Musics.editableMusic);
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

        private void GoToAllMusics()
        {
            CurrentView = new MusicList("All");
        }

        private void GoToAlbums()
        {
            CurrentView = new MusicCategories("Album");
        }

        private void GoToArtists()
        {
            CurrentView = new MusicCategories("Artist");
        }

        private void GoToListing(MusicCategory category)
        {
            CurrentView = new MusicList(category);
        }

        private void GoToEditMusic(Music music)
        {
            CurrentView = new MusicEdit(music);
        }
    }
}
