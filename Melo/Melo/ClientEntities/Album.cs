using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melo.ClientEntities
{
    public class Album:MusicCategory
    {

        public Album(String Name, List<Music> musics):base(Name,musics,"Album")
        {
        }
    }
}
