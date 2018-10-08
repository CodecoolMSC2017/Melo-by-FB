using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.Service.Interface;

namespace Melo.Service.Simple
{
    public class SimpleFileInputMonitor : IFileInputMonitor
    {
        private FileSystemWatcher fileSystemWatcher;
        public static int Raised { get; set; }

        public SimpleFileInputMonitor(String path)
        {

            fileSystemWatcher = new FileSystemWatcher(path);
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.Created += new FileSystemEventHandler(FileCreated);

        } 

        public void FileCreated(Object sender, FileSystemEventArgs e)
        {
                Raised++;

        } 

    }
}
