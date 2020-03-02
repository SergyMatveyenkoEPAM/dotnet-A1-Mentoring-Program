using System.IO;

namespace FileSystemAppLibrary.Tests.FakeEnvironmentModels
{
    public class FakeSystemEntitiesInfo : SystemEntitiesInfo
    {
        public override bool IfFileExists(string path)
        {
            return Path.GetFileName(path).Contains(".");
        }

        public override bool IfDirectoryExists(string path)
        {
            return Path.GetFileName(path).Contains(".");
        }
    }
}
