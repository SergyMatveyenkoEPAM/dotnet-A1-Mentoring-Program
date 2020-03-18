using System.IO;

namespace FileSystemAppLibrary
{
    public class SystemEntitiesInfo
    {
        public virtual bool IfFileExists(string path)
        {
            return File.Exists(path);
        }

        public virtual bool IfDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
