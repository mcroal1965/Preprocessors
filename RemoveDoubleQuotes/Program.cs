using System;
using System.IO;

namespace RemoveDoubleQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileNameIn;
            string FileNameOut;

            FileNameIn = args[0];
            FileNameOut = args[1];

            try
            {
                StreamReader reading = File.OpenText(FileNameIn);
                string str;

                while ((str = reading.ReadLine()) != null)
                {
                    File.AppendAllText(FileNameOut, str.Replace("\"", "") + "\r\n");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);

            }
        }
    }
}

