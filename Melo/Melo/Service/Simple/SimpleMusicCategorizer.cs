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
    public class SimpleMusicCategorizer : IMusicCategorizer
    {
        IMusicService musicService;
        public SimpleMusicCategorizer(IMusicService musicService)
        {
            this.musicService = musicService;
        }

        public List<Album> CreateAlbumList()
        {
            List<Album> albums = new List<Album>();
            List<Music> musics = musicService.GetAll();
            HashSet<String> albumNames = new HashSet<String>();

            foreach (Music music in musics)
            {
                albumNames.Add(music.Album);
            }

            foreach(String albumName in albumNames)
            {
                List<Music> albumMusics = musicService.GetAllByAlbum(albumName);
                Album album = new Album(albumMusics[0].Album, albumMusics);
                albums.Add(album);
            }
            return albums;
        }

        public List<Artist> CreateArtistList()
        {
            List<Artist> artists = new List<Artist>();
            List<Music> musics = musicService.GetAll();
            HashSet<String> artistNames = new HashSet<String>();

            foreach (Music music in musics)
            {
                artistNames.Add(music.Artist);
            }

            foreach (String artistName in artistNames)
            {
                List<Music> artistMusics = musicService.GetAllByArtist(artistName);
                Artist artist = new Artist(artistMusics[0].Artist, artistMusics);
                artists.Add(artist);
            }
            return artists;
        }
    }
}
