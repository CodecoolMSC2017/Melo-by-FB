using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Melo.ClientEntities;
using Melo.Service.Interface;
using Melo.Dao.Interface;
using Melo.Dao.Simple;
using NAudio.Wave;

namespace Melo.Service.Simple
{
    class SimpleMusicService : IMusicService
    {
        IMusicDao musicDao;
        public SimpleMusicService(IConnectionCreater connectionCreater)
        {
            musicDao = new SimpleMusicDao(connectionCreater);
        }

        public void Add(Music music, int directoryId)
        {
            musicDao.InsertMusic(music, directoryId);
        }

        public void DeleteById(int id)
        {
            musicDao.DeleteMusicById(id);
        }

        public List<Music> GetAll()
        {
            return musicDao.GetAll();
        }

        public List<Music> GetAllByAlbum(string album)
        {
            return musicDao.GetAllByAlbum(album);
        }

        public List<Music> GetAllByArtist(string artist)
        {
            return musicDao.GetAllByArtist(artist);
        }

        public Music GetById(int id)
        {
            return musicDao.GetMusicById(id);
        }

        public void UpdateMusic(Music music)
        {
            TagLib.File tagFile = TagLib.File.Create(music.FilePath);
            tagFile.Tag.Album = music.Album;
            tagFile.Tag.Title = music.Title;
            tagFile.Tag.Artists = new string[] { music.Artist };
            tagFile.Save();
            musicDao.UpdateMusic(music);
        }

        public void Combine(string[] mp3Files, string mp3OuputFile, int dirId)
        {
            if (File.Exists(mp3OuputFile))
            {
                string[] splitted = mp3OuputFile.Split('.');

                for (int i = 0; i < 100; i++)
                {
                    string newName = splitted[0] + i + "." + splitted[1];
                    if (!File.Exists(newName))
                    {
                        mp3OuputFile = newName;
                        break;
                    }
                }
            }


            using (var w = new BinaryWriter(File.Create(mp3OuputFile)))
            {
                new List<string>(mp3Files).ForEach(f => w.Write(File.ReadAllBytes(f)));
            }
            FileInfo file = new FileInfo(mp3OuputFile);
            Music music = new Music(file.FullName, file.Name);
            Add(music, dirId);
        }

        private void CreateWavFromMp3(String input, String output)
        {
            
            using (var reader = new Mp3FileReader(input))
            {
                WaveFileWriter.CreateWaveFile(output, reader);
            }
        }

        private void SplitWav(TimeSpan start, TimeSpan end, String input, String output)
        {
            using (WaveFileReader reader = new WaveFileReader(input))
            {
                using (WaveFileWriter writer = new WaveFileWriter(output, reader.WaveFormat))
                {
                    int segement = reader.WaveFormat.AverageBytesPerSecond / 1000;

                    int startPosition = (int)start.TotalMilliseconds * segement;
                    startPosition = startPosition - startPosition % reader.WaveFormat.BlockAlign;

                    int endBytes = (int)end.TotalMilliseconds * segement;
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    int endPosition = (int)reader.Length - endBytes;


                    reader.Position = startPosition;
                    byte[] buffer = new byte[1024];
                    while (reader.Position < endPosition)
                    {
                        int segment = (int)(endPosition - reader.Position);
                        if (segment > 0)
                        {
                            int bytesToRead = Math.Min(segment, buffer.Length);
                            int bytesRead = reader.Read(buffer, 0, bytesToRead);
                            if (bytesRead > 0)
                            {
#pragma warning disable CS0618 // Type or member is obsolete
                                writer.WriteData(buffer, 0, bytesRead);
#pragma warning restore CS0618 // Type or member is obsolete
                            }
                        }
                    }
                }
            }
        }

        private void ConvertWavToMp3(String input, String output)
        {
            using (var reader = new WaveFileReader(input))
            {
                try
                {
                    MediaFoundationEncoder.EncodeToMp3(reader, output);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void TrimAudio(TimeSpan start, TimeSpan end, Music music)
        {

            try
            {
                String input = music.FilePath;

                //Check and create right outputFileName
                string[] splitted = music.FilePath.Split('.');
                string outputName = splitted[0] + "-splitted.mp3";
                if (File.Exists(outputName))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string newName = splitted[0] + "-splitted" + i + ".mp3";
                        if (!File.Exists(newName))
                        {
                            outputName = newName;
                            break;
                        }
                    }
                }
                //Get current directory
                String currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                //Create WavFile from mp3
                String tempWavOutputFileName = Path.Combine(currentDirectory, "temp1.wav");
                CreateWavFromMp3(input, tempWavOutputFileName);

                //Split the new wav
                String splittedTempWavOutputFileName = Path.Combine(currentDirectory, "temp2.wav");
                SplitWav(start, end, tempWavOutputFileName, splittedTempWavOutputFileName);

                //Convert temp Wav to Mp3
                ConvertWavToMp3(splittedTempWavOutputFileName, outputName);

                //Add new audio to database
                FileInfo file = new FileInfo(outputName);
                Add(new Music(file.FullName,file.Name), music.DirectoryId);
            }
            finally
            {
                //Delete temp files
                DeleteFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp1.wav"));
                DeleteFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp2.wav"));
            }
        }
    }
}
