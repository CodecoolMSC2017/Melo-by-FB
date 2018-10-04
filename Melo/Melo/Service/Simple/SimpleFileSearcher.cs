using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Melo.Service.Interface;
using Melo.ClientEntities;
using Melo.Constants;


namespace Melo.Service.Simple
{
    public class SimpleFileSearcher: IFileSearcher
    {
        private List<String> MusicExtensions;
        private List<String> VideoExtensions;
        private List<String> PictureExtensions;
        private IMusicService musicService;
        private IImageService imageService;
        private IVideoService videoService;
        private int dirId;


        public SimpleFileSearcher(ClientEntities.Directory dir, Extensions extensions,IMusicService musicService, IVideoService videoService, IImageService imageService)
        {
            this.MusicExtensions = extensions.MusicExtensions;
            this.VideoExtensions = extensions.VideoExtensions;
            this.PictureExtensions = extensions.PictureExtensions;
            this.musicService = musicService;
            this.imageService = imageService;
            this.videoService = videoService;
            dirId = dir.Id;
            Search(dir);
        }

        public void Search(ClientEntities.Directory dir)
        {
                FileInfo[] files = null;
                DirectoryInfo[] subDirs = null;
                DirectoryInfo root = new DirectoryInfo(dir.Path);
                files = root.GetFiles();

                if (files != null)
                {
                    foreach (FileInfo fi in files)
                    {

                        /// Add to specific list(Casted)
                        String extension = Path.GetExtension(fi.FullName);
                        if (MusicExtensions.Contains(extension))
                        {
                            musicService.Add(new Music(fi.FullName, fi.Name),dirId);
                        }
                        else if (VideoExtensions.Contains(extension))
                        {
                            videoService.Add(new Video(fi.FullName, fi.Name), dirId);
                        }
                        else if (PictureExtensions.Contains(extension))
                        {
                            imageService.Add(new Image(fi.FullName, fi.Name), dirId);
                        }
                    }


                    subDirs = root.GetDirectories();

                    foreach (DirectoryInfo dirInfo in subDirs)
                    {

                        Search(new ClientEntities.Directory(dirInfo.Name,dirInfo.FullName));

                    }
                }
            }
        }
}
