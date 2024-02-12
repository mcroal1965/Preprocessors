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
                        if (str.IndexOf("\"") > 0)
                        {
                            int firstQuote = str.IndexOf("\"") + 1;
                            string jobTitleDescription = str.Substring(firstQuote - 1, 1 + str.Substring(firstQuote, str.Length - firstQuote).IndexOf("\""));
                            str = str.Replace(jobTitleDescription, jobTitleDescription.Replace(",", ""));
                        }

                        string[] splitStr = str.Split(",");
                        string[] newSplitStr = new string[splitStr.Length];

                        for (int i = 0; i < splitStr.Length; i++)
                        {
                            newSplitStr[i] = splitStr[i].Replace("\"", "").Replace(",", "");
                        }

                        string newStr = string.Join(",", newSplitStr) + "\r\n";

                        File.AppendAllText(FileNameOut, newStr);
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