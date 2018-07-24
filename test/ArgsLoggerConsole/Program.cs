using System;
using System.IO;

namespace ArgsLoggerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string logFileName = "ArgsLoggerConsole.txt";
            if (!File.Exists(logFileName))
            {
                using (File.CreateText(logFileName)) ;
            }
            File.WriteAllLines(logFileName, args);
        }
    }
}
