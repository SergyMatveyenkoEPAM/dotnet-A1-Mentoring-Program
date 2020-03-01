using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystemAppLibrary.Tests.UnitTests
{
    [TestFixture]
    class FileSystemVisitorTests
    {
        [Test]
        public void Find_Request_True()
        {
            // Arrange
            int a = 1;
            int b = 1;
            int c;

            // Act
            c = a + b;

            //Assert
            Assert.IsTrue(c == 2);
        }

    }
}
