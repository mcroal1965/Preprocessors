using System;
using System.IO;

namespace RemoveFirstLineLINEOUT
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Int32 Counter;

                string FileNameIn;
                string FileNameOut;

                //FileNameIn = args[0];
                //FileNameOut = args[1];
                FileNameIn = @"C:\CB\test\NewTextDocument.txt";
                FileNameOut = @"C:\CB\test\NewTextDocument2.txt";

                StreamReader reading = File.OpenText(FileNameIn);
                string str;

                Counter = 0;

                while ((str = reading.ReadLine()) != null)
                {
                    if (str.ToUpper().Trim() == "LINEOUT" && Counter == 0) { }
                    else
                        Counter += 1;

                    if (Counter > 0)
                        File.AppendAllText(FileNameOut, str + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
        }
    }
}
