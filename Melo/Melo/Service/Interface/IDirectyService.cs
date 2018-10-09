using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Service.Interface
{
    public interface IDirectyService
    {
        Directory Add(Directory directory);
        Directory GetById(int id);
        List<Directory> GetAll();
        void DeleteById(int id);
    }
}
