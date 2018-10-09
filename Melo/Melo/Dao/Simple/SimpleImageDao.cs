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
    class SimpleImageDao : IImageDao
    {

        private IConnectionCreater ConnectionCreater;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public SimpleImageDao(IConnectionCreater connectionCreater)
        {
            this.ConnectionCreater = connectionCreater;
        }

        public void DeleteImageById(int id)
        {
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("Delete FROM pictures WHERE id = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", id));
                    command.ExecuteNonQuery();
                    log.Info("Image with the id: " + id + " successfully deleted from the database");
                }
            } catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while deleting an image from the database", e);
            }
        }

        public List<Image> GetAll()
        {
            
            List<Image> images = new List<Image>();
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM pictures", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            String path = reader.GetString(2);
                            String name = reader.GetString(3);
                            Image image = new Image(path, name, newid);
                            images.Add(image);

                        }
                    }
                }
            } catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting all images from the database", e);
            }
            
            return images;
        }

        public Image GetImageById(int id)
        {
            Image image = null;
            try { 
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM pictures WHERE id = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            String path = reader.GetString(2);
                            String name = reader.GetString(3);
                            image = new Image(path, name, newid);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting an image from the database", e);
            }
            return image;
        }

        public void InsertImage(Image image, int directoryId)
        {
            try { 
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO pictures (name, file_path, extension, directory_id) VALUES (@name, @file_path, @extension, @directory_id)", conn);

                    insertCommand.Parameters.Add(new SqlParameter("name", image.Name));
                    insertCommand.Parameters.Add(new SqlParameter("file_path", image.FilePath));
                    insertCommand.Parameters.Add(new SqlParameter("extension", image.Extension));
                    insertCommand.Parameters.Add(new SqlParameter("directory_id", directoryId));
                    insertCommand.ExecuteNonQuery();
                    log.Info("Image with the name: " + image.Name + " successfully added to the database");

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while adding an image to the database", e);
            }
        }
        }
    }

