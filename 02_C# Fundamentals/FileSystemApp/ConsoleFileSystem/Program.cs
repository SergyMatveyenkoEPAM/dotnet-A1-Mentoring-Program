using FileSystemAppLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isEnough = false;
            int maxNumberOfDirectories = 41;
            int maxNumberOfFilteredDirectories = 41;
            int maxNumberOfFiles = 41;
            int maxNumberOfFilteredFiles = 41;

            string startDirectory = @"d:\A1 Mentoring Program\02_C# Fundamentals\FileSystemApp\ConsoleFileSystem\";

            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"), new SystemEntitiesInfo());

            // subscribe for events
            fileSystemVisitor.StartMessage += (sender, e) => Console.WriteLine(e.Message);
            fileSystemVisitor.EndMessage += (sender, e) => Console.WriteLine(e.Message);
            fileSystemVisitor.DirectoryFound += (sender, e) =>
            {
                isEnough = e.NumberOfDirectories >= maxNumberOfDirectories;
                Console.WriteLine(e.Message);
            };
            fileSystemVisitor.FilteredDirectoryFound += (sender, e) =>
            {
                isEnough = e.NumberOfDirectories >= maxNumberOfFilteredDirectories;
                Console.WriteLine(e.Message);
            };
            fileSystemVisitor.FileFound += (sender, e) =>
            {
                isEnough = e.NumberOfFiles >= maxNumberOfFiles;
                Console.WriteLine(e.Message);
            };
            fileSystemVisitor.FilteredFileFound += (sender, e) =>
            {
                isEnough = e.NumberOfFiles >= maxNumberOfFilteredFiles;
                Console.WriteLine(e.Message);
            };

            // check if directory exists
            if (string.IsNullOrEmpty(startDirectory) || !Directory.Exists(startDirectory))
            {
                Console.WriteLine("Path to the start directory is incorrect.");
            }

            List<string> results = new List<string>();

            foreach (var item in fileSystemVisitor.GetAllFoldersAndFiles(startDirectory)/*.ToList()*/)
            {
                if (isEnough)
                {
                    break;
                }

                results.Add(item);
            }

            Console.WriteLine("\nSearching results:");

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
