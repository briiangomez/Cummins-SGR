using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGR.Models
{
    public class Logger
    {
        
        public static void AddLine(string line)
        {
            string path = Environment.CurrentDirectory + "/logger.txt";
            File.AppendAllText(path,line + "\n");
            //File.AppendText(Environment.NewLine);
        }
    }
}
