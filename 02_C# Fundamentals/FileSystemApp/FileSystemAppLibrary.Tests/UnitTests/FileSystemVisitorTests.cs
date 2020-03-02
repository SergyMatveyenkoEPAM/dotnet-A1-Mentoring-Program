using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileSystemAppLibrary.Tests.FakeEnvironmentModels;

namespace FileSystemAppLibrary.Tests.UnitTests
{
    [TestFixture]
    class FileSystemVisitorTests
    {
        [Test]
        public void GetAllFoldersAndFiles_Entities_NumberOfDirectoryFound()
        {
            // Arrange
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), new FakeSystemEntitiesInfo(), new FakeEntityFinder());

            // Act
           int numberOfEntitiesFound = fileSystemVisitor.GetAllFoldersAndFiles("fakeAddress").ToList().Count;

            //Assert
            Assert.IsTrue((new FakeEntityFinder()).CatalogTree.Count == numberOfEntitiesFound);
        }

    }
}
