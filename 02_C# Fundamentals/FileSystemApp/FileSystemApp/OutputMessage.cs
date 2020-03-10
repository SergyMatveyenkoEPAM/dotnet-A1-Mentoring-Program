using System;

namespace FileSystemAppLibrary
{
    public class OutputMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public int NumberOfDirectories { get; set; }
        public int NumberOfFiles { get; set; }
    }
}
