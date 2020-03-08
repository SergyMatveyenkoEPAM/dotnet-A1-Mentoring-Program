using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using SystemWatcherApp.Resources;

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
        static CultureInfo currentCulture;

        static Dictionary<Regex, string> rules;

        static Program()
        {
            rules = new Dictionary<Regex, string> { { filesWithARegex, filesWithA }, { filesWithNumberRegex, filesWithNumber } };
            startDirectories = new List<string> { @"e:\_02_BCL\Start1\", @"e:\_02_BCL\Start2\", @"e:\_02_BCL\Start3\" };
            watchers = new List<FileSystemWatcher>();
        }

        static void Main(string[] args)
        {
            string culture;
            do
            {
                Console.WriteLine("Для выбора русской локализации введите \"ру\". To select English localization, enter \"en\"");
                culture = Console.ReadLine();
            } while (culture != "ру" && culture != "en");

            switch (culture)
            {
                case "ру":
                    currentCulture = new CultureInfo("ru-RU");
                    break;
                default:
                    currentCulture = new CultureInfo("en-US");
                    break;
            }

            Thread.CurrentThread.CurrentUICulture = currentCulture;

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

                watchers.Add(watcher);
            }


            // Wait for the user to quit the program.
            Console.WriteLine(Resource.Suggesting_The_Way_To_Quit);
            while (true) ;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            string newAddress = "";
            bool isMatch = false;
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine(string.Format(Resource.File_Has_Been_Created, e.FullPath, File.GetCreationTime(e.FullPath).ToString(currentCulture)));
            foreach (var rule in rules)
            {
                if (rule.Key.IsMatch(Path.GetFileName(e.FullPath)))
                {
                    newAddress = rule.Value + Path.GetFileName(e.FullPath);
                    isMatch = true;
                    Console.WriteLine(string.Format(Resource.The_Rule_Was_Matched, rule.Key));
                    break;
                }
            }

            if (!isMatch)
            {
                newAddress = defaultDirectory + Path.GetFileName(e.FullPath);
                Console.WriteLine(Resource.No_Rule_Matched);
            }

            File.Move(e.FullPath, newAddress);
            Console.WriteLine(string.Format(Resource.The_File_Has_Been_Moved_To, newAddress));
            // if (e.Name.Contains("a"))
            //{
            //    Console.WriteLine(e.FullPath + "\n" + filesWithA + Path.GetFileName(e.FullPath));
            //}
        }


    }
}
