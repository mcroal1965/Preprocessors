using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RemoveCommaFromBusinessNames
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string FileNameIn = null;
                string FileNameOut = null;

                if (args.Length == 2)
                {
                    FileNameIn = args[0];
                    FileNameOut = args[1];
                }
                else
                {
                    Console.WriteLine("Error: Invalid arguments {File Path In} {File Path Out}");
                    Console.WriteLine("Press any key to exit . . .");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                if (!File.Exists(FileNameIn))
                {
                    Console.WriteLine("Error: File not found at " + FileNameIn);
                    Console.WriteLine("Press any key to exit . . .");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                List<string> lineOutStrings = new List<string>();

                StreamReader reader = new StreamReader(FileNameIn);

                while (!reader.EndOfStream)
                {
                    string Line = reader.ReadLine();
                    string[] values = Line.Split(',');

                    int offset = Line.Count(c => c == ',') + 1 - 6;

                    string field1 = values[0];
                    string field2 = values[1];
                    string field3 = values[2];

                    string businessName = values[3];
                    for (int i = 1; i <= offset; i++)
                    {
                        businessName = businessName + values[3 + i];
                    }

                    string field5 = values[4 + offset];
                    string field6 = values[5 + offset];

                    string lineOutString = field1 + "," + field2 + "," + field3 + "," + businessName + "," + field5 + "," + field6;

                    lineOutStrings.Add(lineOutString);
                }

                File.WriteAllLines(FileNameOut, lineOutStrings);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
                Console.WriteLine("Press any key to exit . . .");
                Console.ReadKey();
                Environment.Exit(1);
            }
            finally
            {
                Console.WriteLine("Press any key to exit . . .");
                Environment.Exit(0);
            }
        }
    }
}
