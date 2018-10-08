using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Service.Interface
{
    public interface IMusicService
    {
        void Add(Music music, int directoryId);
        Music GetById(int id);
        List<Music> GetAll();
        List<Music> GetAllByAlbum(String album);
        List<Music> GetAllByArtist(String artist);
        void DeleteById(int id);
        void UpdateMusic(Music music);
        void Combine(string[] mp3Files, string mp3OuputFile, int dirId);
    }
}
