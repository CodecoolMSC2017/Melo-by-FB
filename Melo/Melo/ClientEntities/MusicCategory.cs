using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melo.ClientEntities
{
    public abstract class MusicCategory
    {
        public String Name { get; set; }
        public List<Music> musics { get; set; }
        public String Type { get; set; }

        public MusicCategory(String Name, List<Music> musics,String type)
        {
            this.Name = Name;
            this.musics = musics;
            this.Type = type;
        }
    }
}
