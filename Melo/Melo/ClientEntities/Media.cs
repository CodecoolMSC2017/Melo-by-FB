using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Melo.ClientEntities
{
    public class Media
    {
        public String FilePath { get; set; }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Extension { get; set; }
        public String Comment { get; set; }


        public Media(String path, String name)
        {
            this.FilePath = path;
            this.Name = name;
            this.Extension = Path.GetExtension(FilePath);
            Comment = null;
        }

        public Media(String path, String name,int id)
        {
            this.FilePath = path;
            this.Name = name;
            this.Extension = Path.GetExtension(FilePath);
            this.Id = id;
            Comment = null;
        }
    }
}
