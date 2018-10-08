using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Melo.Service.Interface;
using Melo.Service.Simple;
using Melo.ClientEntities;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Melo.ViewModel;


namespace Melo
{
    /// <summary>
    /// Interaction logic for MusicCategories.xaml
    /// </summary>
    public partial class MusicCategories : UserControl

    {
        IMusicService musicService;
        IMusicCategorizer musicCategorizer;
        ObservableCollection<Album> albumList;
        ObservableCollection<Artist> artistList;
        String type;
        public Music selectedMusic;


        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;


        public MusicCategories()
        {
            if (Musics.selectedMusic != null)
            {
                selectedMusic = Musics.selectedMusic;
                selectedDockPanel.DataContext = selectedMusic;
            }
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public MusicCategories(String arg)
        {
            musicService = new SimpleMusicService(new SimpleConnectionCreater());
            type = arg;
            musicCategorizer = new SimpleMusicCategorizer(musicService);
            if (type.Equals("Album"))
            {
                albumList = new ObservableCollection<Album>(musicCategorizer.CreateAlbumList());
                InitializeComponent();
                musicList.ItemsSource = albumList;
            } else if (type.Equals("Artist"))
            {
                artistList = new ObservableCollection<Artist>(musicCategorizer.CreateArtistList());
                InitializeComponent();
                musicList.ItemsSource = artistList;
            }

            if (Musics.selectedMusic != null)
            {
                selectedMusic = Musics.selectedMusic;
                selectedDockPanel.DataContext = selectedMusic;
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void DockPanel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        public void SelectAlbum(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            MusicCategory category = b.CommandParameter as MusicCategory;
            Musics.selectedCategory = category;
            var viewModel = (MusicViewModel)DataContext;
            if (viewModel.GoToListingCommand.CanExecute(null))
                viewModel.GoToListingCommand.Execute(null);
        }
    }
}
