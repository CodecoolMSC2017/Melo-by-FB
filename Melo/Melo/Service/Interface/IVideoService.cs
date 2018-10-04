using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Service.Interface
{
    public interface IVideoService
    {
        void Add(Video video,int directoryId);
        Video GetById(int id);
        List<Video> GetAll();
        void DeleteById(int id);
    }
}
