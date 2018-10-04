using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Dao.Interface
{
    interface IVideoDao
    {
        void InsertVideo(Video video, int directoryId);
        Video GetVideoById(int id);
        List<Video> GetAll();
        void DeleteVideoById(int id);
    }
}
