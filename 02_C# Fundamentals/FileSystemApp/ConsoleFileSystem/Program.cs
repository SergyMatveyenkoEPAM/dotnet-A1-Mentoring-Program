using FileSystemApp;
using System;
using System.IO;
using System.Linq;

namespace ConsoleFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string startDirectory = @"e:\EPAM\dotnet-A1-Mentoring-Program\02_C# Fundamentals\FileSystemApp\ConsoleFileSystem\";

            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(address => !address.Contains("Debug"));

            fileSystemVisitor.StartMessage += message => Console.WriteLine(message);
            fileSystemVisitor.EndMessage += message => Console.WriteLine(message);
            fileSystemVisitor.DirectoryFound += message => Console.WriteLine(message);
            fileSystemVisitor.FilteredDirectoryFound += message => Console.WriteLine(message);
            fileSystemVisitor.FileFound += message => Console.WriteLine(message);
            fileSystemVisitor.FilteredFileFound += message => Console.WriteLine(message);
            fileSystemVisitor.Enough += TerminateSearching;

            // let's take 5 entities
            RecipientEventArgs recipient = new RecipientEventArgs(quantityWeNeed: 5);
           

            if (string.IsNullOrEmpty(startDirectory) || !Directory.Exists(startDirectory))
            {
                Console.WriteLine("Path to the start directory is incorrect");
            }

            // without ".ToList()" we can not get the correct order of notification
            var results = fileSystemVisitor.GetAllFoldersAndFiles(startDirectory).ToList();
            
            Console.WriteLine("\nSearching results:");

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        static void TerminateSearching(object sender, RecipientEventArgs e)
        {
            
        }
    }
}
