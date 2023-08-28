using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

//02-14-2022 Prefixing FJ and removing windows login slashes
//02-15-2022 Fixed missing tabs when fields are missing
//08-28-2023 Removed leading zero from file num and supv num

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
                                    //c.Add((char)09);
                                    //c.Add(line);
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
                        var pieces = line.Split(new[] { '\t' }, line.Count(f => (f == (char)09)) + 1);

                        string file_number = pieces[0].TrimStart(new Char[] { '0' });
                        pieces[0] = file_number;
                        string division = pieces[5];
                        string windows_login = pieces[9];
                        string supv_number = pieces[11].TrimStart(new Char[] { '0' });
                        pieces[11] = supv_number;
                        string benefit_eligibility = pieces[13];
                        string position_id = pieces[15];

                        if (windows_login.Contains('/'))
                        {
                            windows_login = windows_login.Substring(windows_login.LastIndexOf('/') + 1);
                            pieces[9] = windows_login;
                        }

                        
                        if (position_id.Substring(0,3) != "VKE" && position_id.Length > 0)
                        {
                            file_number = position_id.Substring(0, 2) + (file_number).PadLeft(4, '0');
                            pieces[0] = file_number;

                            supv_number = position_id.Substring(0, 2) + (supv_number).PadLeft(4, '0');
                            pieces[11] = supv_number;
                        }
                        

                        /*
                        if (division == "First Jersey")
                        {
                            file_number = "FJ" + (file_number).PadLeft(4, '0');
                            pieces[0] = file_number;

                            supv_number = "FJ" + (supv_number).PadLeft(4, '0');
                            pieces[11] = supv_number;
                        }
                        */

                        int i = 0;
                        foreach (string piece in pieces)
                        {
                            i++;

                            if (piece != " " && piece != '\t'.ToString() && piece != '\n'.ToString() && piece != '\r'.ToString())
                            {
                                if (i != pieces.Length)
                                {
                                    output_lines.Add(piece.ToString() + '\t');
                                }
                                else
                                {
                                    output_lines.Add(piece.ToString());
                                    break;
                                }
                            }
                        }
                   
                        output_lines.Add('\r'.ToString());
                        output_lines.Add('\n'.ToString());
                    }
                }

                output_lines.RemoveAt(output_lines.Count - 1);             

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
