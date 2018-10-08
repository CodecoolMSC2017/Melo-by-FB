using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Melo.ClientEntities;
using Melo.Service.Interface;
using Melo.Dao.Interface;
using Melo.Dao.Simple;

namespace Melo.Service.Simple
{
    class SimpleMusicService: IMusicService
    {
        IMusicDao musicDao;
        public SimpleMusicService(IConnectionCreater connectionCreater)
        {
            musicDao = new SimpleMusicDao(connectionCreater);
        }

        public void Add(Music music, int directoryId)
        {
            musicDao.InsertMusic(music, directoryId);
        }

        public void DeleteById(int id)
        {
            musicDao.DeleteMusicById(id);
        }

        public List<Music> GetAll()
        {
            return musicDao.GetAll();
        }

        public List<Music> GetAllByAlbum(string album)
        {
            return musicDao.GetAllByAlbum(album);
        }

        public List<Music> GetAllByArtist(string artist)
        {
            return musicDao.GetAllByArtist(artist);
        }

        public Music GetById(int id)
        {
            return musicDao.GetMusicById(id);
        }

        public void UpdateMusic(Music music)
        {
            TagLib.File tagFile = TagLib.File.Create(music.FilePath);
            tagFile.Tag.Album = music.Album;
            tagFile.Tag.Title = music.Title;
            tagFile.Tag.Artists = new string[] { music.Artist };
            tagFile.Save();
            musicDao.UpdateMusic(music);
        }

        public void Combine(string[] mp3Files, string mp3OuputFile, int dirId)
        {
            if (File.Exists(mp3OuputFile)) {
                string[] splitted = mp3OuputFile.Split('.');
                
                for (int i = 0; i < 100; i++)
                {
                    string newName = splitted[0] + i + "." + splitted[1];
                    if (!File.Exists(newName))
                    {
                        mp3OuputFile = newName;
                        break;
                    }
                }
            }


            using (var w = new BinaryWriter(File.Create(mp3OuputFile)))
            {
                new List<string>(mp3Files).ForEach(f => w.Write(File.ReadAllBytes(f)));
            }
            FileInfo file = new FileInfo(mp3OuputFile);
            Music music = new Music(file.FullName, file.Name);
            Add(music, dirId);
        }
    }
}
