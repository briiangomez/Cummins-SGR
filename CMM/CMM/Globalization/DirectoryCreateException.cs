namespace CMM.Globalization
{
    using CMM;
    using System;
    using System.IO;

    public class DirectoryCreateException : Exception, ICMMException
    {
        public DirectoryCreateException() : base("No permission to create a directory.")
        {
        }

        public DirectoryCreateException(IOException exception) : base("No permission to create a directory.", exception)
        {
        }
    }
}

