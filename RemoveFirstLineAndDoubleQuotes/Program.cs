using System;
using System.IO;

namespace RemoveFirstLineAndDoubleQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 Counter;

            string FileNameIn;
            string FileNameOut;

            FileNameIn = args[0];
            FileNameOut = args[1];

            try
            {
                StreamReader reading = File.OpenText(FileNameIn);
                string str;

                Counter = 0;

                while ((str = reading.ReadLine()) != null)
                {
                    if (Counter > 0)
                    {
                        File.AppendAllText(FileNameOut, str.Replace("\"", "") + "\r\n");
                    }

                    Counter += 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
        }
    }
}