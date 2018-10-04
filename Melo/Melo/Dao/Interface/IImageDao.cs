using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Dao.Interface
{
    public interface IImageDao
    {
        void InsertImage(Image image,int directoryId);
        Image GetImageById(int id);
        List<Image> GetAll();
        void DeleteImageById(int id);
    }
}
