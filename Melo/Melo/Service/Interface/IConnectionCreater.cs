using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Melo.ClientEntities;

namespace Melo.Service.Interface
{
    public interface IConnectionCreater
    {
        SqlConnection connect();
    }
}
