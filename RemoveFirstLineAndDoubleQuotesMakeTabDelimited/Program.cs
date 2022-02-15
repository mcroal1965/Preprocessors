using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

//02-14-2022 Prefixing FJ and removing windows login slashes
//02-15-2022 Fixed missing tabs when fields are missing

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string FileNameIn = @"C:\Users\brent\source\CB\Nautilus_Export.csv";
                //string FileNameOut = @"C:\Users\brent\source\CB\Temp_Nautilus_Export.txt";
                string FileNameIn = args[0];
                string FileNameOut = args[1];

                Int32 counter = 0;
                Int32 ConvertFlag = 1;

                string output_file = FileNameOut;
                string input_file_chars = File.ReadAllText(FileNameIn);

                List<char> c = new List<char>();
                List<string> tab_lines = new List<string>();
                List<string> output_lines = new List<string>();

                foreach (char line in input_file_chars)
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
                                if (line is (char)13)
                                {
                                    c.Add((char)09);
                                    c.Add(line);
                                }
                                else
                                {
                                    c.Add(line);
                                }
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
                    tab_lines.Add(i.ToString());
                }

                string tab_line = string.Join("", tab_lines.ToArray());
                string[] input_file_lines = tab_line.Split(new[] { '\n' });

                foreach (string line in input_file_lines)
                {
                    if (line != "" && line != null)
                    {
                        var pieces = line.Split(new[] { '\t' }, 13);

                        string file_number = pieces[0];
                        string division = pieces[5];
                        string windows_login = pieces[9];
                        string supv_number = pieces[11];

                        if (windows_login.Contains('/'))
                        {
                            windows_login = windows_login.Substring(windows_login.LastIndexOf('/') + 1);
                            pieces[9] = windows_login;
                        }

                        if (division == "First Jersey")
                        {
                            file_number = "FJ" + (file_number).PadLeft(4, '0');
                            pieces[0] = file_number;

                            supv_number = "FJ" + (supv_number).PadLeft(4, '0');
                            pieces[11] = supv_number;
                        }

                        foreach (string piece in pieces)
                        {
                            if (piece != " " && piece != '\t'.ToString() && piece != '\n'.ToString() && piece != '\r'.ToString())
                                output_lines.Add(piece.ToString() + '\t');
                        }
                        output_lines.Add('\n'.ToString());
                    }
                }

                File.AppendAllText(output_file, string.Join("", output_lines.ToArray()));
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
