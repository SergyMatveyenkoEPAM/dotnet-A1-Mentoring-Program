using System;
using System.IO;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace SystemWatcherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            string rootDirectory = @"d:\_02_BCL\Root\";
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            if (string.IsNullOrEmpty(rootDirectory))
            {
                string[] args = System.Environment.GetCommandLineArgs();

                // If a directory is not specified, exit program.
                if (args.Length != 2)
                {
                    // Display the proper way to call the program.
                    Console.WriteLine("Usage: SystemWatcherApp.exe (directory)");
                    return;
                }

                watcher.Path = args[1];
            }
            watcher.Path = rootDirectory;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
          //  watcher.Filter = "*.*";

            // Add event handlers.
            watcher.Created += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'Ctrl+C\' or \'Ctrl+Break\' to quit the sample.");
            while (true) ;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
           // if (e.Name.Contains("a"))
            {
                Console.WriteLine(e.FullPath + "\n" + @"d:\_02_BCL\FilesWithA\"+Path.GetFileName(e.FullPath));
            }
        }

      
    }
}
