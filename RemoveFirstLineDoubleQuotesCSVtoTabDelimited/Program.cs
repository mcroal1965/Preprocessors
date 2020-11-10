using System;
using System.IO;

namespace RemoveFirstLineDoubleQuotesCSVtoTabDelimited
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileNameIn;
            string FileNameOut;

            FileNameIn = args[0];
            FileNameOut = args[1];

            StreamWriter output_file = new StreamWriter(FileNameOut);
            Int32 nextchar;
            Int32 tabchar = 9;
            Int32 commachar = 44;
            Int32 dqchar = 34;
            Int32 LFchar = 10;
            Int32 dqflag = 0;
            Int32 LFflag = 0;

            try
            {
                using StreamReader sr = new StreamReader(FileNameIn);
                while (sr.Peek() >= 0) //look at the next character in the file and make sure there is a next character
                {
                    nextchar = sr.Read(); //get the next character as an int value (aka the ASCII value)

                    if (LFflag == 1) //already found the end of the 1st row so figure out if we write char asis or swap to a tab
                    {
                        if (nextchar == commachar && dqflag == 0) //found comma outside the double quote 
                        {
                            output_file.Write(Char.ToString((char)tabchar)); //so write a tab instead
                        }
                        else
                        {
                            if (nextchar == dqchar)  //found a double quote
                            {
                                if (dqflag == 0)
                                {
                                    dqflag = 1; //toggle the flag and don't write the doublequote
                                }
                                else
                                {
                                    dqflag = 0; //toggle the flag and don't write the doublequote
                                }
                            }
                            else
                            {
                                output_file.Write(Char.ToString((char)nextchar));  //write the character to the file
                            }
                        }
                    }
                    else
                    {
                        if (nextchar == LFchar) //found linefeed at end of first row so start writing out to file on the next character
                        {
                            LFflag = 1;
                        }
                    }
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
                Environment.Exit(1);
            }


        }
    }
}
