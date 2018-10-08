using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Melo.Service.Interface
{
    public interface IFileInputMonitor
    {
        void FileCreated(Object sender, FileSystemEventArgs e);
    }
}
