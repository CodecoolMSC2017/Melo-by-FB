using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
