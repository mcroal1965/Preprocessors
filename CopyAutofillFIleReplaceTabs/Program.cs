using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CopyAutofillFIleReplaceTabs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string FileNameIn;
                string FileOutPath;
                //FileNameIn = args[0];
                //FileOutPath = args[1];

                FileNameIn = @"C:\Test\Test.txt";
                FileOutPath = @"C:\Test\Path\";

                if (FileOutPath.Substring(FileOutPath.Length) == @"\")
                {
                    FileOutPath = FileOutPath.Substring(0, FileOutPath.Length - 1);
                }

                string input_file = File.ReadAllText(FileNameIn);
                string output_path = FileOutPath + FileNameIn.Substring(FileNameIn.LastIndexOf(@"\"));

                File.WriteAllText(output_path, input_file.Replace((char)09, '|'));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Environment.Exit(1);
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
