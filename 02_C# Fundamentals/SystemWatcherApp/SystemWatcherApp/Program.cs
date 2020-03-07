using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SystemWatcherApp
{
    class Program
    {
        readonly static List<string> startDirectories;
        readonly static List<FileSystemWatcher> watchers;
        readonly static string defaultDirectory = @"e:\_02_BCL\Default\";
        readonly static string filesWithA = @"e:\_02_BCL\FilesWithA\";
        readonly static Regex filesWithARegex = new Regex(@"a+", RegexOptions.IgnoreCase);
        readonly static string filesWithNumber = @"e:\_02_BCL\FilesWithNumber\";
        readonly static Regex filesWithNumberRegex = new Regex(@"[0-9]+");

        static Dictionary<Regex, string> rules;

        static Program()
        {
            rules = new Dictionary<Regex, string> { { filesWithARegex, filesWithA }, { filesWithNumberRegex, filesWithNumber } };
            startDirectories = new List<string> { @"e:\_02_BCL\Start1\", @"e:\_02_BCL\Start2\", @"e:\_02_BCL\Start3\" };
            watchers = new List<FileSystemWatcher>();
        }

        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            // establish environment
            foreach (var startDirectory in startDirectories)
            {
                Directory.CreateDirectory(startDirectory);
            }
            Directory.CreateDirectory(defaultDirectory);
            Directory.CreateDirectory(filesWithA);
            Directory.CreateDirectory(filesWithNumber);

            // Create a new FileSystemWatcher and set its properties.
            foreach (var startDirectory in startDirectories)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = startDirectory;
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.EnableRaisingEvents = true;

                watchers.Add(watcher) ;
            }

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'Ctrl+C\' or \'Ctrl+Break\' to quit the sample.");
            while (true) ;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            string newAddress = "";
            bool isMatch = false;
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File " + e.FullPath+ " has been created " + File.GetCreationTime(e.FullPath));
            foreach (var rule in rules)
            {
                if (rule.Key.IsMatch(Path.GetFileName(e.FullPath)))
                {
                    newAddress = rule.Value + Path.GetFileName(e.FullPath);
                    isMatch = true;
                    Console.WriteLine("The rule \""+ rule.Key + "\" was matched");
                    break;
                }
            }

            if (!isMatch)
            {
                newAddress = defaultDirectory + Path.GetFileName(e.FullPath);
                Console.WriteLine("No rule matched");
            }

            File.Move(e.FullPath, newAddress);
            Console.WriteLine("The file has been moved to " + newAddress);
            // if (e.Name.Contains("a"))
            //{
            //    Console.WriteLine(e.FullPath + "\n" + filesWithA + Path.GetFileName(e.FullPath));
            //}
        }


    }
}
