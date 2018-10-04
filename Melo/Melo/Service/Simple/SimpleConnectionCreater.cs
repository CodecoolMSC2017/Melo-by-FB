using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.Service.Interface;

namespace Melo.Service.Simple
{
    class SimpleConnectionCreater : IConnectionCreater
    {
        public SqlConnection connect()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            @"Data Source=JÓZSI\SQLEXPRESS;" +
            "Initial Catalog=Melo;" +
            "Integrated Security=SSPI;";
            return conn;
        }
    }
}
