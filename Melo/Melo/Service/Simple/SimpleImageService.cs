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
    public class SimpleImageService: IImageService
    {
        IImageDao imageDao;
        public SimpleImageService(IConnectionCreater connectionCreater)
        {
            imageDao = new SimpleImageDao(connectionCreater);
        }

        public void Add(Image image, int directoryId)
        {
            imageDao.InsertImage(image, directoryId);
        }

        public void DeleteById(int id)
        {
            imageDao.DeleteImageById(id);
        }

        public List<Image> GetAll()
        {
            return imageDao.GetAll();
        }

        public Image GetById(int id)
        {
            return imageDao.GetImageById(id);
        }
    }
}
