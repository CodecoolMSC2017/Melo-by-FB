using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.ClientEntities;

namespace Melo.Dao.Interface
{
    public interface IDirectoryDao
    {
        Directory InsertDirectory(Directory directory);
        Directory GetDirectoryById(int id);
        List<Directory> GetAll();
        void DeleteDirectoryById(int id);

    }
}
