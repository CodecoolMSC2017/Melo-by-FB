using System.Collections.Generic;
using Melo.ClientEntities;
using Melo.Service.Interface;
using Melo.Dao.Interface;
using Melo.Dao.Simple;

namespace Melo.Service.Simple
{
    class SimpleVideoService: IVideoService
    {
        IVideoDao videoDao;
        public SimpleVideoService(IConnectionCreater connectionCreater)
        {
            videoDao = new SimpleVideoDao(connectionCreater);
        }

        public void Add(Video video, int directoryId)
        {
            videoDao.InsertVideo(video, directoryId);
        }

        public void DeleteById(int id)
        {
            videoDao.DeleteVideoById(id);
        }

        public List<Video> GetAll()
        {
            return videoDao.GetAll();
        }

        public Video GetById(int id)
        {
            return videoDao.GetVideoById(id);
        }
    }
}
