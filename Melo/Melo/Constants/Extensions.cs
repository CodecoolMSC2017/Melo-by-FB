using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melo.Constants
{
    public class Extensions
    {
        public List<String> MusicExtensions { get; set; }

        public List<String> VideoExtensions { get; set; }

        public List<String> PictureExtensions { get; set; }

        public Extensions()
        {
            CreateMusicExtensions();
            CreateVideoExtensions();
            CreatePictureExtensions();
        }

        private void CreateMusicExtensions()
        {
            this.MusicExtensions = new List<String>();
            MusicExtensions.Add(".mp3");
            MusicExtensions.Add(".flac");
            MusicExtensions.Add(".ogg");
        }

        private void CreateVideoExtensions()
        {
            this.VideoExtensions = new List<String>();
            VideoExtensions.Add(".mp4");
            VideoExtensions.Add(".avi");
            VideoExtensions.Add(".wmv");
            VideoExtensions.Add(".flv");
            VideoExtensions.Add(".mkv");
        }

        private void CreatePictureExtensions()
        {
            this.PictureExtensions = new List<String>();
            PictureExtensions.Add(".png");
            PictureExtensions.Add(".jpg");
            PictureExtensions.Add(".jpeg");
        }
    }
}
