using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using FileSystemApp;

namespace ConsoleFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string startDirectory = @"d:\A1 Mentoring Program\02_C# Fundamentals\FileSystemApp\ConsoleFileSystem\";

            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"));

            fileSystemVisitor.StartMessage += message => Console.WriteLine(message);
            fileSystemVisitor.EndMessage += message => Console.WriteLine(message);
            fileSystemVisitor.DirectoryFound += message => Console.WriteLine(message);
            fileSystemVisitor.FilteredDirectoryFound += message => Console.WriteLine(message);
            fileSystemVisitor.FileFound += message => Console.WriteLine(message);
            fileSystemVisitor.FilteredFileFound += message => Console.WriteLine(message);

            if (string.IsNullOrEmpty(startDirectory) || !Directory.Exists(startDirectory))
            {
                Console.WriteLine("Path to the start directory is incorrect");
            }

            // without ".ToList()" we can not get the correct order of notification
            var results = fileSystemVisitor.GetAllFoldersAndFiles(startDirectory).ToList();
            
            Console.WriteLine("Searching results:");

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
