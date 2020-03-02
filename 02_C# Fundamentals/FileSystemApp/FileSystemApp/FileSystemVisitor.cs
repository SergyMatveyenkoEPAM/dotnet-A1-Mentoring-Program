﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemAppLibrary
{
    public class FileSystemVisitor
    {
        public int NumberOfDirectories { get; set; }
        public int NumberOfFilteredDirectories { get; set; }
        public int NumberOfFiles { get; set; }
        public int NumberOfFilteredFiles { get; set; }
        private SystemEntitiesInfo _systemEntitiesInfo;

        public event EventHandler<OutputMessageEventArgs> StartMessage;
        public event EventHandler<OutputMessageEventArgs> EndMessage;
        public event EventHandler<OutputMessageEventArgs> FileFound;
        public event EventHandler<OutputMessageEventArgs> FilteredFileFound;
        public event EventHandler<OutputMessageEventArgs> DirectoryFound;
        public event EventHandler<OutputMessageEventArgs> FilteredDirectoryFound;

        public Predicate<string> Filter { get; set; }

        public FileSystemVisitor(Predicate<string> filter, SystemEntitiesInfo systemEntitiesInfo)
        {
            Filter = filter;
            _systemEntitiesInfo = systemEntitiesInfo;
        }

        public IEnumerable<string> GetAllFoldersAndFiles(string startAddress)
        {
            if (string.IsNullOrEmpty(startAddress) && !Directory.Exists(startAddress))
            {
                yield break;
            }

            StartMessage?.Invoke(this, new OutputMessageEventArgs
            {
                Message = "-------------Searching was started-------------"
            });

            var entities = FindEntities(startAddress);

            foreach (var item in entities)
            {
                if (_systemEntitiesInfo.IfDirectoryExists(item) && HandleDirectory(item))
                {
                    yield return item;
                }

                if (_systemEntitiesInfo.IfFileExists(item) && HandleFile(item))
                {
                    yield return item;
                }
            }

            EndMessage?.Invoke(this, new OutputMessageEventArgs
            {
                Message = "-------------Searching was finished-------------"
            });
        }

        private IEnumerable<string> FindEntities(string startAddress)
        {
            var entities = Directory.GetFileSystemEntries(startAddress, "*", SearchOption.AllDirectories).ToList();
            entities.Sort();

            return entities;
        }

        private bool HandleDirectory(string item)
        {
            ++NumberOfDirectories;

            DirectoryFound?.Invoke(this, new OutputMessageEventArgs
            {
                Message = "Directory was found: " + Path.GetFileName(item),
                NumberOfDirectories = NumberOfDirectories
            });

            if (Filter != null)
            {
                if (!Filter(item))
                {
                    return false;
                }

                ++NumberOfFilteredDirectories;

                FilteredDirectoryFound?.Invoke(this, new OutputMessageEventArgs
                {
                    Message = "Filtered directory was found: " + Path.GetFileName(item),
                    NumberOfDirectories = NumberOfFilteredDirectories
                });
            }

            return true;
        }

        private bool HandleFile(string item)
        {
            ++NumberOfFiles;

            FileFound?.Invoke(this, new OutputMessageEventArgs
            {
                Message = "File was found: " + Path.GetFileName(item),
                NumberOfFiles = NumberOfFiles
            });

            if (Filter != null)
            {
                if (!Filter(item))
                {
                    return false;
                }

                ++NumberOfFilteredFiles;

                FilteredFileFound?.Invoke(this, new OutputMessageEventArgs
                {
                    Message = "Filtered file was found: " + Path.GetFileName(item),
                    NumberOfFiles = NumberOfFilteredFiles
                });
            }

            return true;
        }
    }
}
