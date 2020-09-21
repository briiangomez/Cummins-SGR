namespace CMM.IO
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class StreamExtensions
    {
        public static void CopyTo(this Stream src, Stream dest)
        {
            byte[] buffer = new byte[0x10000];
            try
            {
                int num;
                while ((num = src.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(buffer, 0, num);
                }
            }
            finally
            {
                dest.Flush();
            }
        }

        public static string ReadString(this Stream stream)
        {
            return stream.ReadString(Encoding.UTF8);
        }

        public static string ReadString(this Stream stream, Encoding encoding)
        {
            stream.Seek(0L, SeekOrigin.Begin);
            TextReader reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }

        public static string SaveAs(this Stream stream, string filePath)
        {
            return stream.SaveAs(filePath, true);
        }

        public static string SaveAs(this Stream stream, string filePath, bool isOverwrite)
        {
            byte[] buffer = new byte[stream.Length];
            int num = stream.Read(buffer, 0, (int) stream.Length);
            return SaveAs(buffer, filePath, isOverwrite);
        }

        public static string SaveAs(byte[] data, string filePath, bool isOverwrite)
        {
            string directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            if (!(isOverwrite || !File.Exists(filePath)))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);
                int num = 1;
                do
                {
                    filePath = Path.Combine(directoryName, string.Format("{0}-{1}{2}", fileNameWithoutExtension, num, extension));
                    num++;
                }
                while (File.Exists(filePath));
            }
            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Write(data, 0, data.Length);
            }
            return filePath;
        }

        public static void WriteString(this Stream src, string s)
        {
            src.WriteString(s, Encoding.UTF8);
        }

        public static void WriteString(this Stream src, string s, Encoding encoding)
        {
            TextWriter writer = new StreamWriter(src, encoding);
            writer.Write(s);
            writer.Flush();
        }
    }
}

