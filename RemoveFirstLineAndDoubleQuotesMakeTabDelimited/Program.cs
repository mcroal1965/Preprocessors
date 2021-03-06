﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string FileNameIn;
                string FileNameOut;
                FileNameIn = args[0];
                FileNameOut = args[1];

                Int32 counter = 0;
                Int32 ConvertFlag = 1;

                string output_file = FileNameOut;
                string input_file = File.ReadAllText(FileNameIn);

                List<char> c = new List<char>();
                List<string> outputlines = new List<string>();

                foreach (char line in input_file)
                {
                    if (counter > 0)
                    {
                        if (line is '\"')
                        {
                            if (ConvertFlag is 1)
                            {
                                ConvertFlag = 0;
                            }
                            else
                            {
                                if (ConvertFlag is 0)
                                {
                                    ConvertFlag = 1;
                                }
                            }
                        }

                        if (line is ',')
                        {
                            if (ConvertFlag is 0)
                            {
                                c.Add(line);
                            }
                            else
                            {
                                c.Add((char)09);
                            }
                        }
                        else
                        {
                            if (line != '\"')
                            {
                                c.Add(line);
                            }
                        }
                    }

                    if (line is (char)10)
                    {
                        counter++;
                    }

                }

                foreach (char i in c)
                {
                    outputlines.Add(i.ToString());
                }

                string output = string.Join("", outputlines.ToArray());
                File.AppendAllText(output_file, output);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
