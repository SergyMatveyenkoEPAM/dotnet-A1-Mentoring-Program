using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemAppLibrary
{
    public class EntityFinder
    {
        public virtual IEnumerable<string> FindEntities(string startAddress)
        {
            var entities = Directory.GetFileSystemEntries(startAddress, "*", SearchOption.AllDirectories).ToList();
            entities.Sort();

            return entities;
        }
    }
}
