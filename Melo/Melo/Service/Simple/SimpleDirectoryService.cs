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
    public class SimpleDirectoryService : IDirectyService
    {
        IDirectoryDao directoryDao;
        public SimpleDirectoryService(IConnectionCreater connectionCreater)
        {
            directoryDao = new SimpleDirectoryDao(connectionCreater);
        }
        public Directory Add(Directory directory)
        {
            return directoryDao.InsertDirectory(directory);
        }

        public void DeleteById(int id)
        {
            directoryDao.DeleteDirectoryById(id);
        }

        public List<Directory> GetAll()
        {
            return directoryDao.GetAll();
        }

        public Directory GetById(int id)
        {
            return directoryDao.GetDirectoryById(id);
        }
    }
}
