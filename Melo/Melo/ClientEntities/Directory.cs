using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melo.ClientEntities
{
    public class Directory
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Path { get; set; }


        public Directory(int id, String name, String path)
        {
            this.Id = id;
            this.Name = name;
            this.Path = path;
        }

        public Directory(String name, String path)
        {
            this.Name = name;
            this.Path = path;
        }
    }


}
