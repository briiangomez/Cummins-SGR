namespace SGR.Communicator.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class WebFileIndexModel
    {
        public string BaseDirectory { get; set; }

        public List<DirectoryInfo> Directories { get; set; }

        public List<FileInfo> Files { get; set; }

        public string RelativeDirectory { get; set; }

        public string RelativeDirectoryCus { get; set; }

        public FileSystemInfo Selected { get; set; }
    }
}

