using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace VerifyCase
{
    class Program
    {
        static int Main(string[] args)
        {
            var files = Directory.GetFiles(".", "*", SearchOption.AllDirectories)
                .Select(f => (f.StartsWith($".{Path.DirectorySeparatorChar}") || f.StartsWith($".{Path.AltDirectorySeparatorChar}")) ? f.Substring(2) : f)
                .ToArray();
            Log($"Got {files.Length} files.", ConsoleColor.Cyan);

            Array.Sort(files);

            var similarFiles = new List<string>();

            for (int i = 0; i < files.Length - 1; i++)
            {
                if (string.Compare(files[i], files[i + 1], true) == 0)
                {
                    similarFiles.Add(files[i]);
                    similarFiles.Add(files[i + 1]);
                }
            }

            var distinctFiles = similarFiles.Distinct().ToArray();

            Array.Sort(distinctFiles);

            if (distinctFiles.Length > 0)
            {
                Log($"Found {distinctFiles.Length} files with difference in casing.", ConsoleColor.Red);

                foreach (string filename in distinctFiles)
                {
                    Log(filename, ConsoleColor.Red);
                }

                return 1;
            }

            Log("Ok!", ConsoleColor.Green);
            return 0;
        }

        static void Log(string message, ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
            finally
            {
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
