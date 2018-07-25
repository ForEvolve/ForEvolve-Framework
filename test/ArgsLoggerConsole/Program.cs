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
#pragma warning disable CS0642 // Possible mistaken empty statement
                using (File.CreateText(logFileName)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
            }
            File.WriteAllLines(logFileName, args);
        }
    }
}
