using System;
using System.IO;

namespace EncompassDIPprep
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 Counter;
            string outputstring;

            string FolderIn = args[0];
            try
            {
                Directory.GetFiles(FolderIn);
            }
            catch 
            {
                Console.WriteLine("Folder does not exist::" + FolderIn);
                Environment.Exit(1);
            }

            DateTime YYYYMMDDHHMMx = DateTime.Now;
            string YYYYMMDDHHMM = YYYYMMDDHHMMx.ToString("yyyyMMddHHmmss");
            string IndexFileOut = FolderIn + @"\" + YYYYMMDDHHMM + "_" + "Index.csv";       

            try
            {
                foreach (string f in Directory.GetFiles(FolderIn, "Index.csv", SearchOption.TopDirectoryOnly))
                {
                    StreamReader reading = File.OpenText(FolderIn + @"\Index.csv");
                    string str;

                    Counter = 0;

                    while ((str = reading.ReadLine()) != null)
                    {
                        if (Counter > 0)
                        {                                                  
                            int Endindex = str.IndexOf("|");
                            outputstring = str.Substring(FolderIn.Length + 1, Endindex - FolderIn.Length - 1);

                            string sourceFile = FolderIn + @"\" + outputstring;
                            string destinationFile = FolderIn + @"\" + outputstring.Replace(outputstring, YYYYMMDDHHMM + "_" + outputstring);

                            File.AppendAllText(IndexFileOut, str.Replace(outputstring, YYYYMMDDHHMM + "_" + outputstring) + "\r\n");

                            try
                            {
                                File.Copy(sourceFile, destinationFile, true);
                                File.Delete(sourceFile);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error:" + ex.Message);
                                Environment.Exit(1);
                            }
                        }    
                        
                        Counter += 1;
                    }

                    reading.Close();
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Environment.Exit(1);
            }
            
            finally
            {
                try
                {
                    File.Delete(FolderIn + @"\Index.csv");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                    Environment.Exit(1);
                }
                Environment.Exit(0);
            }
        }
    }
}
