using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SystemWatcherApp.Configs;
using SystemWatcherApp.Resources;

namespace SystemWatcherApp
{
    class Program
    {
        readonly static List<string> startDirectories;
        readonly static List<FileSystemWatcher> watchers;
        readonly static string defaultDirectory = @"D:\_02_BCL\Default\";
        static CultureInfo currentCulture;
        static List<RuleElement> rules;

        static Program()
        {
            rules = new List<RuleElement>();
            startDirectories = new List<string>();
            watchers = new List<FileSystemWatcher>();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            WatcherSectionGroup watcherSectionGroup = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["watcherSection"] as WatcherSectionGroup;

            foreach (ConfigurationSection section in watcherSectionGroup.Sections)
            {
                if (section.GetType() == typeof(RuleSection))
                {
                    RuleSection ruleSection = (RuleSection)section;
                    RuleElementCollection ruleElementCollection = ruleSection.Rules;
                    foreach (RuleElement ruleElement in ruleElementCollection)
                    {
                        rules.Add(ruleElement);
                    }
                }

                else if (section.GetType() == typeof(LocaleSection))
                {
                    LocaleSection localeSection = (LocaleSection)section;
                    switch (localeSection.LocaleCode)
                    {
                        case "ru":
                            currentCulture = new CultureInfo("ru-RU");
                            break;
                        default:
                            currentCulture = new CultureInfo("en-US");
                            break;
                    }
                }

                else if (section.GetType() == typeof(TrackingFoldersSection))
                {
                    TrackingFoldersSection trackingFoldersSection = (TrackingFoldersSection)section;
                    TrackingFolderElementCollection trackingFolderElementCollection = trackingFoldersSection.TrackingFolders;
                    foreach (TrackingFolderElement trackingFolderElement in trackingFolderElementCollection)
                    {
                        startDirectories.Add(trackingFolderElement.Address);
                    }
                }
            }

            Thread.CurrentThread.CurrentUICulture = currentCulture;

            Run();
        }

        public static void Run()
        {
            // establish environment
            {
                foreach (var startDirectory in startDirectories)
                {
                    Directory.CreateDirectory(startDirectory);
                }

                foreach (var rule in rules)
                {
                    Directory.CreateDirectory(rule.Address);
                }

                Directory.CreateDirectory(defaultDirectory);
            }

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

            Console.WriteLine(string.Format(Resource.File_Has_Been_Created, e.FullPath, File.GetCreationTime(e.FullPath).ToString(currentCulture)));

            foreach (var rule in rules)
            {
                if (Regex.IsMatch(Path.GetFileName(e.FullPath), rule.Pattern))
                {
                    string numericPrefix = rule.IsRequiredNumeration ? (Directory.GetFiles(rule.Address).Length + 1) + "_" : "";

                    string datePostfix = rule.IsRequiredMoveDate ? DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") : "";

                    newAddress = rule.Address + numericPrefix + Path.GetFileName(e.FullPath) + datePostfix;

                    isMatch = true;

                    Console.WriteLine(string.Format(Resource.The_Rule_Was_Matched, rule.Pattern));
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
        }
    }
}
