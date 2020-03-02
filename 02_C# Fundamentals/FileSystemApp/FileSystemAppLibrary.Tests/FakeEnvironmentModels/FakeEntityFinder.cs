using System.Collections.Generic;

namespace FileSystemAppLibrary.Tests.FakeEnvironmentModels
{
    public class FakeEntityFinder : EntityFinder
    {
        public List<string> CatalogTree { get; set; }

        public FakeEntityFinder()
        {
            CatalogTree = new List<string>
            {
                @"D:\TierOne_1",
                @"D:\TierOne_1\file_1.dll",
                @"D:\TierOne_1\file_2.dll",
                @"D:\TierOne_1\file_3.dll",
                @"D:\TierOne_2",
                @"D:\TierOne_3",
                @"D:\TierOne_1\TierTwo_1",
                @"D:\TierOne_1\TierTwo_1\file_1.txt",
                @"D:\TierOne_1\TierTwo_1\file_2.txt",
                @"D:\TierOne_1\TierTwo_1\file_3.txt",
                @"D:\TierOne_1\TierTwo_2",
                @"D:\TierOne_1\TierTwo_3"
            };
        }

        public override IEnumerable<string> FindEntities(string startAddress)
        {
            return CatalogTree;
        }
    }
}
