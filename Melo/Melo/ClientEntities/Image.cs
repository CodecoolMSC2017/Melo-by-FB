using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Melo.ClientEntities
{
    public class Image:Media
    {
        public Image(String path, String name) : base(path, name)
        {

        }

        public Image(String path, String name,int id) : base(path, name,id)
        {
        }
    }
}
