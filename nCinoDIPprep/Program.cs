using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nCinoDIPprep
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileIn = null;
            string FileOut = null;

            if (args.Length == 2)
            {
                FileIn = args[0];
                FileOut = args[1];
            }
            else
            {
                Console.WriteLine("Error: Invalid arguments {File Path In} {File Path Out}");
                Environment.Exit(1);
            }

            if (!File.Exists(FileIn))
            {
                Console.WriteLine("Error: File not found at " + FileIn);
                Environment.Exit(1);
            }

            try
            {
                File.Delete(FileOut);
                StreamReader reading = File.OpenText(FileIn);

                int Counter = 0;

                string str;
                while ((str = reading.ReadLine()) != null)
                {
                    if (Counter > 0)
                    {
                        string[] splitString = str.Split('|');

                        if (splitString[2].Trim() == "")
                        {
                            if (splitString[6].Trim() == "")
                                splitString[2] = "ENT Unmapped";
                            else
                                splitString[2] = "LON Unmapped";
                        }                       

                        string outputString = String.Join("|", splitString);

                        File.AppendAllText(FileOut, outputString + "\r\n");
                    }

                    Counter += 1;
                }

                reading.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Environment.Exit(1);
            }
            finally
            {
                Environment.Exit(0);
            }
        }
    }
}
