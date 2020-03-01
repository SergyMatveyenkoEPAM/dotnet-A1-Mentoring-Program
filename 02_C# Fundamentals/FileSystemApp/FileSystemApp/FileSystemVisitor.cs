using System;
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

        bool includeDirectory;
        bool includeFile;

        public event EventHandler<OutputMessageEventArgs> StartMessage;
        public void StartMessageFire()
        {
            StartMessage?.Invoke(this, new OutputMessageEventArgs
            {
                Message = "-------------Searching was started-------------"
            });
        }

        public event EventHandler<OutputMessageEventArgs> EndMessage;
        public void EndMessageFire()
        {
            EndMessage?.Invoke(this, new OutputMessageEventArgs
            {
                Message = "-------------Searching was finished-------------"
            });
        }

        public event EventHandler<OutputMessageEventArgs> FileFound;
        public event EventHandler<OutputMessageEventArgs> FilteredFileFound;
        public event EventHandler<OutputMessageEventArgs> DirectoryFound;
        public event EventHandler<OutputMessageEventArgs> FilteredDirectoryFound;

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
            if (!string.IsNullOrEmpty(startAddress) && Directory.Exists(startAddress))
            {
                var entities = FindEntities(startAddress);

                foreach (var item in entities)
                {
                    if (Directory.Exists(item))
                    {
                        ++NumberOfDirectories;

                        DirectoryFound?.Invoke(this, new OutputMessageEventArgs
                        {
                            Message = "Directory was found: " + Path.GetFileName(item),
                            NumberOfDirectories = NumberOfDirectories
                        });

                        if (Filter != null)
                        {
                            if (Filter(item))
                            {
                                ++NumberOfFilteredDirectories;

                                FilteredDirectoryFound?.Invoke(this, new OutputMessageEventArgs
                                {
                                    Message = "Filtered directory was found: " + Path.GetFileName(item),
                                    NumberOfDirectories = NumberOfFilteredDirectories
                                });
                            }
                            else
                            {
                                continue;
                            }
                        }
                        yield return item;
                    }

                    if (File.Exists(item))
                    {
                        ++NumberOfFiles;

                        FileFound?.Invoke(this, new OutputMessageEventArgs
                        {
                            Message = "File was found: " + Path.GetFileName(item),
                            NumberOfFiles = NumberOfFiles
                        });

                        if (Filter != null)
                        {
                            if (Filter(item))
                            {
                                ++NumberOfFilteredFiles;

                                FilteredFileFound?.Invoke(this, new OutputMessageEventArgs
                                {
                                    Message = "Filtered file was found: " + Path.GetFileName(item),
                                    NumberOfFiles = NumberOfFilteredFiles
                                });
                            }
                            else
                            {
                                continue;
                            }
                        }
                        yield return item;
                    }
                }
            }
        }

        private IEnumerable<string> FindEntities(string startAddress)
        {
            var directories = Directory.GetDirectories(startAddress);

            var files = Directory.GetFiles(startAddress);

            var entities = directories.Concat(files).AsEnumerable();

            {  /*      foreach (var directory in directories)
            {
                yield return directory;
            }

            foreach (var file in files)
            {
                yield return file;
            } */
            }
            foreach (var directory in directories)
            {
                entities = entities.Concat(FindEntities(directory));
            }

            return entities;
        }

        private IEnumerable<string> GetDirectories(string startAddress)
        {
            var directories = Directory.GetDirectories(startAddress);
            return directories;
            {   //foreach (var item in directories)
                //{
                //    ++NumberOfDirectories;

                //    DirectoryFound?.Invoke(this, new OutputMessageEventArgs
                //    {
                //        Message = "Directory was found: " + Path.GetFileName(item),
                //        NumberOfDirectories = NumberOfDirectories
                //    });

                //    if (Filter != null)
                //    {
                //        if (Filter(item))
                //        {
                //            ++NumberOfFilteredDirectories;

                //            FilteredDirectoryFound?.Invoke(this, new OutputMessageEventArgs
                //            {
                //                Message = "Filtered directory was found: " + Path.GetFileName(item),
                //                NumberOfDirectories = NumberOfFilteredDirectories
                //            });
                //        }
                //        else
                //        {
                //            continue;
                //        }
                //    }
                //    yield return item;
                //}
            }
        }

        private IEnumerable<string> GetFiles(string startAddress)
        {
            var files = Directory.GetFiles(startAddress);
            return files;
            {   //foreach (var item in files)
                //{
                //    ++NumberOfFiles;

                //    FileFound?.Invoke(this, new OutputMessageEventArgs
                //    {
                //        Message = "File was found: " + Path.GetFileName(item),
                //        NumberOfFiles = NumberOfFiles
                //    });

                //    if (Filter != null)
                //    {
                //        if (Filter(item))
                //        {
                //            ++NumberOfFilteredFiles;

                //            FilteredFileFound?.Invoke(this, new OutputMessageEventArgs
                //            {
                //                Message = "Filtered file was found: " + Path.GetFileName(item),
                //                NumberOfFiles = NumberOfFilteredFiles
                //            });
                //        }
                //        else
                //        {
                //            continue;
                //        }
                //    }
                //    yield return item;
                //}
            }
        }
    }
}
