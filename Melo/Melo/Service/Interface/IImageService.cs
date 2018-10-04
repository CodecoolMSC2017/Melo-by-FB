using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Service.Interface
{
    public interface IImageService
    {
        void Add(Image image, int directoryId);
        Image GetById(int id);
        List<Image> GetAll();
        void DeleteById(int id);
    }
}
