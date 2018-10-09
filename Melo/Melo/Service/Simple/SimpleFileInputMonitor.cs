using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melo.Service.Interface;
using log4net;

namespace Melo.Service.Simple
{
    public class SimpleFileInputMonitor : IFileInputMonitor
    {
        private FileSystemWatcher fileSystemWatcher;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                log.Info("A new file added one of the watched directories");

        } 

    }
}
