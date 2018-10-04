using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Dao.Interface
{
    interface IMusicDao
    {
        void InsertMusic(Music music, int directoryId);
        Music GetMusicById(int id);
        List<Music> GetAll();
        void DeleteMusicById(int id);
        List<Music> GetAllByAlbum(String album);
        List<Music> GetAllByArtist(String artist);
    }
}
