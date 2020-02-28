using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileSystemApp
{
    public delegate void StartMessage(string message);
    public delegate void EndMessage(string message);
    public delegate void FileFound(string message);
    public delegate void FilteredFileFound(string message);
    public delegate void DirectoryFound(string message);
    public delegate void FilteredDirectoryFound(string message);

    public class FileSystemVisitor
    {
        public event StartMessage StartMessage;
        public event EndMessage EndMessage;
        public event FileFound FileFound;
        public event FilteredFileFound FilteredFileFound;
        public event DirectoryFound DirectoryFound;
        public event FilteredDirectoryFound FilteredDirectoryFound;
        
        public Predicate<string> Filter { get; set; }

        public FileSystemVisitor()
        {

        }

        public FileSystemVisitor(Predicate<string> filter)
        {
            Filter = filter;
        }

        public IEnumerable<string> GetAllFoldersAndFiles(string startAddress)
        {
            if (string.IsNullOrEmpty(startAddress) || !Directory.Exists(startAddress))
            {
                var flag = false;
            }

            StartMessage?.Invoke("-------------Searching was started-------------");

            // without ".ToList()" we can not get the correct order of notification
            var result = FindEntities(startAddress).ToList();

            EndMessage?.Invoke("-------------Searching was finished-------------");

            return result;
        }

        private IEnumerable<string> FindEntities(string startAddress)
        {
            var directories = Directory.GetDirectories(startAddress).Where(d =>
            {
                DirectoryFound?.Invoke("Directory was found: " + Path.GetFileName(d));

                if (Filter != null)
                {
                    if (Filter(d))
                    {
                        FilteredDirectoryFound?.Invoke("Filtered directory was found: " + Path.GetFileName(d));
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
                // without ".ToList()" messages appears twice
            }).ToList();

            var files = Directory.GetFiles(startAddress).Where(f =>
            {
                FileFound?.Invoke("File was found: " + Path.GetFileName(f));

                if (Filter != null)
                {
                    if (Filter(f))
                    {
                        FilteredFileFound?.Invoke("Filtered file was found: " + Path.GetFileName(f));
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
                // without ".ToList()" messages appears twice
            }).ToList();

            var entityes = directories.Concat(files).AsEnumerable();

            foreach (var directory in directories)
            {
                entityes = entityes.Concat(FindEntities(directory));
            }

            return entityes;
        }

        //private IEnumerable<string> GetDirectories(string startAddress)
        //{

        //}
    }
}
