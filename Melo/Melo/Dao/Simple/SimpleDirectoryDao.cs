using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Melo.Service.Interface;
using Melo.ClientEntities;
using Melo.Dao.Interface;
using log4net;

namespace Melo.Dao.Simple
{
    public class SimpleDirectoryDao: IDirectoryDao
    {
        private IConnectionCreater ConnectionCreater;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public SimpleDirectoryDao(IConnectionCreater connectionCreater)
        {
            this.ConnectionCreater = connectionCreater;
        }

        public Directory InsertDirectory(Directory directory)
        {
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO directories (name, directory_path) VALUES (@name, @directory_path)", conn);

                    insertCommand.Parameters.Add(new SqlParameter("name", directory.Name));
                    insertCommand.Parameters.Add(new SqlParameter("directory_path", directory.Path));
                    insertCommand.ExecuteNonQuery();
                    log.Info("Directory with the name: " + directory.Name + " successfully added to the database");

                }
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while adding a directory to the database", e);
            }
            return GetDirectoryByName(directory.Name);

        }


        public Directory GetDirectoryById(int id)
        {
            Directory directory = null;
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM directories WHERE id = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            String name = reader.GetString(1);
                            String path = reader.GetString(2);
                            directory = new Directory(newid, name, path);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting a directory from the database", e);
            }
            return directory;
        }

        private Directory GetDirectoryByName(String name)
        {
            Directory directory = null;
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM directories WHERE name = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", name));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            String newName = reader.GetString(1);
                            String path = reader.GetString(2);
                            directory = new Directory(id, newName, path);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting a directory to the database", e);
            }
            return directory;
        }

        public void DeleteDirectoryById(int id)
        {
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("Delete FROM directories WHERE id = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", id));
                    command.ExecuteNonQuery();
                    log.Info("Directory with the id: " + id + " successfully deleted from the database");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while deleting a directory from the database", e);
            }
        }

        public List<Directory> GetAll()
        {
            List<Directory> directories = new List<Directory>();
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM directories", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            String name = reader.GetString(1);
                            String path = reader.GetString(2);
                            Directory directory = new Directory(newid, name, path);
                            directories.Add(directory);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting all directories from the database", e);
            }
            return directories;

        }
    }
}
