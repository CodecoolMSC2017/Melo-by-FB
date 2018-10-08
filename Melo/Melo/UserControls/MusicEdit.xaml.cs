using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Melo.Service.Interface;
using Melo.Service.Simple;
using Melo.ClientEntities;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Reflection;

namespace Melo
{
    /// <summary>
    /// Interaction logic for MusicEdit.xaml
    /// </summary>
    public partial class MusicEdit : UserControl
    {

        public Music selectedMusic;
        IMusicService musicService;
        List<Music> musicList;

        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public MusicEdit()
        {
            InitializeComponent();
        }

        public MusicEdit(Music music)
        {
            musicService = new SimpleMusicService(new SimpleConnectionCreater());
            musicList = musicService.GetAll();
            selectedMusic = music;
            InitializeComponent();
            this.DataContext = selectedMusic;
            InitComboBoxes();
            StartTimerAndPlayer();
        }

        private void InitComboBoxes()
        {
            firstMp3.ItemsSource = musicList;
            secondMp3.ItemsSource = musicList;
            if(musicList.Count > 0)
            {
                firstMp3.SelectedIndex = 0;
                secondMp3.SelectedIndex = 0;
            }
        }

        private void StartTimerAndPlayer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                slider.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            String newTitle = TitleTextBox.Text;
            String newAlbum = AlbumTextBox.Text;
            String newArtist = ArtistTextBox.Text;
            if (newTitle == null)
            {
                newTitle = "undefined";
            }
            if (newAlbum == null)
            {
                newAlbum = "undefined";
            }
            if (newArtist == null)
            {
                newArtist = "undefined";
            }
            selectedMusic.Album = newAlbum;
            selectedMusic.Artist = newArtist;
            selectedMusic.Title = newTitle;
            if (Musics.selectedMusic != null)
            {
                if (Musics.selectedMusic.Id == selectedMusic.Id)
                {
                    Musics.selectedMusic = null;
                    mePlayer.Source = null;
                    mePlayer.Stop();
                    mediaPlayerIsPlaying = false;
                }
            }

            musicService.UpdateMusic(selectedMusic);
            this.DataContext = selectedMusic;

        }


        private void Concat_Click(object sender, RoutedEventArgs e)
        {
            Music first = firstMp3.SelectedItem as Music;
            Music second = secondMp3.SelectedItem as Music;
            if(first != null && second != null)
            {
                string[] mp3Paths = new string[] { first.FilePath, second.FilePath };

                string pathWithoutExtension = first.FilePath.Split('.')[0];
                string outputName = pathWithoutExtension + "+" + second.Name;
                musicService.Combine(mp3Paths, outputName, first.DirectoryId);
            }

        }
    }
}
