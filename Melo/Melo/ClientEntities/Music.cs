using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Melo.ClientEntities
{
    public class Music:Media
    {
        public String Artist { get; set; }
        public String Album { get; set; }
        public String Title { get; set; }


        public Music(String path, String name):base(path,name)
        {
            TagLib.File tagFile = TagLib.File.Create(FilePath);
            Artist = tagFile.Tag.FirstAlbumArtist;
            Album = tagFile.Tag.Album;
            Title = tagFile.Tag.Title;
        }

        public Music(String path, String name, int id) : base(path, name, id)
        {
            TagLib.File tagFile = TagLib.File.Create(FilePath);
            Artist = tagFile.Tag.FirstAlbumArtist;
            Album = tagFile.Tag.Album;
            Title = tagFile.Tag.Title;
        }
    }
}
