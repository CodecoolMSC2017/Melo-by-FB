using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Melo.Service.Interface;
using Melo.Service.Simple;
using Melo.ClientEntities;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.Data;

namespace Melo
{
    /// <summary>
    /// Interaction logic for Videos.xaml
    /// </summary>
    public partial class Videos : UserControl
    {
        IVideoService videoService;
        ObservableCollection<Video> myVideos;
        Video selectedVideo;

        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public Videos()
        {
            videoService = new SimpleVideoService(new SimpleConnectionCreater());
            InitializeComponent();
            myVideos = new ObservableCollection<Video>(videoService.GetAll());
            if (myVideos.ToArray().Length > 0)
            {
                selectedVideo = myVideos.ToArray()[0];
            }
            videoList.ItemsSource = myVideos;
            this.DataContext = selectedVideo;


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

        private void Start_Play(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Video video = b.CommandParameter as Video;
            selectedVideo = video;
            this.DataContext = selectedVideo;
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }
    }
}
