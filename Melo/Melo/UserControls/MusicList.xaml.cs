using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Melo.Service.Interface;
using Melo.Service.Simple;
using Melo.ClientEntities;
using Melo.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Melo
{
    /// <summary>
    /// Interaction logic for MusicList.xaml
    /// </summary>
    public partial class MusicList : UserControl
    {
        IMusicService musicSerice;
        ObservableCollection<Music> listable;
        public Music selectedMusic;
        

        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public MusicList()
        {
            if (Musics.selectedMusic != null)
            {
                selectedMusic = Musics.selectedMusic;
                selectedDockPanel.DataContext = selectedMusic;
            }

        }

        public MusicList(String arg)
        {
            musicSerice = new SimpleMusicService(new SimpleConnectionCreater());
            if (arg.Equals("All"))
            {
                listable = new ObservableCollection<Music>(musicSerice.GetAll());
            }
            InitializeComponent();
            musicList.ItemsSource = listable;
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

        public MusicList(MusicCategory category)
        {
            musicSerice = new SimpleMusicService(new SimpleConnectionCreater());
            listable = new ObservableCollection<Music>(category.musics);
            InitializeComponent();
            musicList.ItemsSource = listable;
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

        public void Start_Play(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Music music = b.CommandParameter as Music;
            selectedMusic = music;
            Musics.selectedMusic = music;
            selectedDockPanel.DataContext = selectedMusic;
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        public void Edit_Music(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Music music = b.CommandParameter as Music;
            Musics.editableMusic = music;
            var viewModel = (MusicViewModel)DataContext;
            if (viewModel.GoToEditMusicCommand.CanExecute(null))
                viewModel.GoToEditMusicCommand.Execute(null);
        }
    }
}
