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
    class SimpleMusicDao: IMusicDao
    {
        private IConnectionCreater ConnectionCreater;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SimpleMusicDao(IConnectionCreater connectionCreater)
            {
                this.ConnectionCreater = connectionCreater;
            }

        public void DeleteMusicById(int id)
        {
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("Delete FROM musics WHERE id = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", id));
                    command.ExecuteNonQuery();
                    log.Info("Music with the id: " + id + " successfully deleted from the database");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while deleting a music from the database", e);
            }
        }

        public List<Music> GetAll()
        {
            List<Music> musics = new List<Music>();
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM musics", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            int directoryId = reader.GetInt32(1);
                            String path = reader.GetString(2);
                            String name = reader.GetString(3);
                            Music music = new Music(path, name, newid, directoryId);
                            musics.Add(music);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                log.Error("Sql exception occured while getting all musics from the database", e);
            }
            return musics;
        }

        public List<Music> GetAllByAlbum(string album)
        {
            List<Music> musics = new List<Music>();
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM musics WHERE album = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", album));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            int directoryId = reader.GetInt32(1);
                            String path = reader.GetString(2);
                            String name = reader.GetString(3);
                            Music music = new Music(path, name, newid,directoryId);
                            musics.Add(music);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting all musics from the database", e);

            }
            return musics;
        }

        public List<Music> GetAllByArtist(string artist)
        {
            List<Music> musics = new List<Music>();
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM musics WHERE artist = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", artist));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            int directoryId = reader.GetInt32(1);
                            String path = reader.GetString(2);
                            String name = reader.GetString(3);
                            Music music = new Music(path, name, newid,directoryId);
                            musics.Add(music);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting all musics from the database", e);

            }
            return musics;
        }

        public Music GetMusicById(int id)
        {
            Music music = null;
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM musics WHERE id = @0", conn);
                    command.Parameters.Add(new SqlParameter("0", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int newid = reader.GetInt32(0);
                            int directoryId = reader.GetInt32(1);
                            String path = reader.GetString(2);
                            String name = reader.GetString(3);
                            music = new Music(path, name, newid, directoryId);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while getting a music from the database", e);
            }
            return music;
        }

        public void InsertMusic(Music music, int directoryId)
        {
            try
            {
                using (SqlConnection conn = ConnectionCreater.connect())
                {
                    conn.Open();
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO musics (name, file_path, extension,directory_id,artist,album,title) VALUES (@name, @file_path, @extension, @directory_id, @artist, @album, @title)", conn);

                    insertCommand.Parameters.Add(new SqlParameter("name", music.Name));
                    insertCommand.Parameters.Add(new SqlParameter("file_path", music.FilePath));
                    insertCommand.Parameters.Add(new SqlParameter("extension", music.Extension));
                    insertCommand.Parameters.Add(new SqlParameter("artist", music.Artist));
                    insertCommand.Parameters.Add(new SqlParameter("album", music.Album));
                    insertCommand.Parameters.Add(new SqlParameter("title", music.Title));
                    insertCommand.Parameters.Add(new SqlParameter("directory_id", directoryId));
                    insertCommand.ExecuteNonQuery();
                    log.Info("Music with the name: " + music.Name + " successfully added to the database");

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                log.Error("Sql exception occured while adding a music to the database", e);
            }
        }

        public void UpdateMusic(Music music)
        {
            int dirId = GetMusicById(music.Id).DirectoryId;
            DeleteMusicById(music.Id);
            InsertMusic(music, dirId);
        }
    }
}
