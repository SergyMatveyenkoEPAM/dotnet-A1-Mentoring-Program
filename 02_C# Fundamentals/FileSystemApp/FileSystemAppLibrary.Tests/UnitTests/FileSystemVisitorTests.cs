using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemAppLibrary.Tests.UnitTests
{
    [TestFixture]
    class FileSystemVisitorTests
    {
        private SystemEntitiesInfo _fakeSystemEntitiesInfo;
        private EntityFinder _entityFinder;

        public FileSystemVisitorTests()
        {
            _fakeSystemEntitiesInfo = Mock.Of<SystemEntitiesInfo>();
            Mock.Get(_fakeSystemEntitiesInfo).Setup(s => s.IfDirectoryExists(It.IsAny<string>())).Returns<string>(path => !Path.GetFileName(path).Contains("."));
            Mock.Get(_fakeSystemEntitiesInfo).Setup(s => s.IfFileExists(It.IsAny<string>())).Returns<string>(path => Path.GetFileName(path).Contains("."));

            _entityFinder = Mock.Of<EntityFinder>();
            Mock.Get(_entityFinder).Setup(e => e.FindEntities(It.IsAny<string>())).Returns<string>(startAddress =>
                new List<string>
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
                });
        }

        [Test]
        public void GetAllFoldersAndFiles_Entities_NumberOfDirectoryFound()
        {
            // Arrange
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), _fakeSystemEntitiesInfo, _entityFinder);

            // Act
            int numberOfEntitiesFound = fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList().Count;

            //Assert
            Assert.IsTrue(_entityFinder.FindEntities("").Count() == numberOfEntitiesFound);
        }

        [Test]
        public void GetAllFoldersAndFiles_StartMessage_GetMessage()
        {
            // Arrange
            string message = "";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), _fakeSystemEntitiesInfo, _entityFinder);
            fileSystemVisitor.StartMessage += (sender, e) => message = e.Message;

            // Act
            fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList();

            //Assert
            Assert.IsTrue(message == "-------------Searching was started-------------");
        }

        [Test]
        public void GetAllFoldersAndFiles_EndMessage_GetMessage()
        {
            // Arrange
            string message = "";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), _fakeSystemEntitiesInfo, _entityFinder);
            fileSystemVisitor.EndMessage += (sender, e) => message = e.Message;

            // Act
            fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList();

            //Assert
            Assert.IsTrue(message == "-------------Searching was finished-------------");
        }

        [Test]
        public void GetAllFoldersAndFiles_DirectoryFoundMessage_GetLastMessage()
        {
            // Arrange
            string message = "";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), _fakeSystemEntitiesInfo, _entityFinder);
            fileSystemVisitor.DirectoryFound += (sender, e) => message = e.Message;

            // Act
            fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList();

            //Assert
            Assert.IsTrue(message == "Directory was found: " + Path.GetFileName(_entityFinder.FindEntities("").Last()));
        }

        [Test]
        public void GetAllFoldersAndFiles_DirectoryFound_NumberOfDirectories()
        {
            // Arrange
            int numberOfDirectories = -1;
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), _fakeSystemEntitiesInfo, _entityFinder);
            fileSystemVisitor.DirectoryFound += (sender, e) => numberOfDirectories = e.NumberOfDirectories;

            // Act
            fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList();

            //Assert
            Assert.IsTrue(numberOfDirectories == _entityFinder.FindEntities("").Where(s => !Path.GetFileName(s).Contains(".")).Count());
        }

        [Test]
        public void GetAllFoldersAndFiles_FilteredDirectoryFound_NumberOfDirectories()
        {
            // Arrange
            int numberOfDirectories = -1;
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("TierTwo"), _fakeSystemEntitiesInfo, _entityFinder);
            fileSystemVisitor.FilteredDirectoryFound += (sender, e) => numberOfDirectories = e.NumberOfDirectories;

            // Act
            fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList();

            //Assert
            Assert.IsTrue(numberOfDirectories == _entityFinder.FindEntities("").Where(s => !Path.GetFileName(s).Contains(".") && !s.Contains("TierTwo")).Count());
        }



    }
}
