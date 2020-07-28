using System;
using System.IO;
using System.Collections.Generic;

namespace CoreTrackerSync
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string FolderIn = args[0];

                string[] files = Directory.GetFiles(FolderIn, "dipimport*.txt", SearchOption.TopDirectoryOnly);

                foreach (string file in files)
                {
                    try
                    {
                        List<string> BeginSection = new List<string>();
                        List<string> OutputLines = new List<string>();

                        OutputLines.Clear();
                        BeginSection.Clear();
                     
                        string DiskGroupNum = "";
                        string FileName = "";
                        string FileFullName = "";
                        string Source = "";
                        string Target = "";
                        int foundbegin = 0;

                        OutputLines.Add(">>>>Self Configuring Tagged DIP<<<<");

                        string[] lines = File.ReadAllLines(file);

                        foreach (string line in lines)
                        {                       
                            try
                            {
                                if (line.Contains(">>DiskgroupNum:") is true)
                                {
                                    DiskGroupNum = "";
                                    Source = "";

                                    DiskGroupNum = line.Substring(line.LastIndexOf(":") + 1).Trim();
                                }

                                if (line.Contains("END:") is true)
                                {
                                    if (BeginSection.Contains(">>FileTypeNum: 47") == false)
                                    {
                                        foreach (string item in BeginSection)
                                        {
                                            OutputLines.Add(item.Trim());
                                        }
                                    }

                                    OutputLines.Add(line.Trim());

                                    string filepath = FolderIn + @"\" + "DIPIndex.txt";
                                    string output = string.Join(Environment.NewLine, OutputLines.ToArray());

                                    File.AppendAllText(filepath, output.Trim());
                                    File.Delete(file);
                                }

                                if (line.Contains("BEGIN:") is true)
                                {
                                    if (foundbegin == 0)
                                    {
                                        if (BeginSection.Contains(">>FileTypeNum: 47") == false)
                                        {
                                            foreach (string item in BeginSection)
                                            {
                                                OutputLines.Add(item.Trim());
                                            }
                                        }

                                        BeginSection.Clear();
                                        foundbegin = 1;

                                        BeginSection.Add(line.Trim());
                                    }
                                    else
                                    {
                                        if (BeginSection.Contains(">>FileTypeNum: 47") == false)
                                        {
                                            foreach (string item in BeginSection)
                                            {
                                                OutputLines.Add(item.Trim());
                                            }
                                        }

                                        BeginSection.Clear();
                                        foundbegin = 0;                                        

                                        BeginSection.Add(line.Trim());
                                    }
                                }
                                else
                                {
                                    if (
                                        line.Contains("DiskgroupNum") is false &&
                                        line.Contains("Document Handle") is false &&
                                        line.Contains("VolumeNum") is false && 
                                        line.Contains("FileSize") is false && 
                                        line.Contains("ImageType") is false && 
                                        line.Contains("Xdpi") is false && 
                                        line.Contains("Ydpi") is false && 
                                        line.Contains("Autofill Status: ") is false && 
                                        line.Contains(">>>>") is false && 
                                        line.Contains("DocRevNum") is false && 
                                        line.Contains("Rendition") is false && 
                                        line.Contains("Compress: 0") is false && 
                                        line.Contains("Deposit Name") is false &&
                                        line.Contains("Deposit Application") is false &&
                                        line.Contains("Deposit Type") is false &&
                                        line.Contains("Deposit Status") is false &&
                                        line.Contains("Deposit Open Date") is false &&
                                        line.Contains("Deposit Tax ID") is false &&
                                        line.Contains("Deposit Branch") is false &&
                                        line.Contains("XR Entity CIFNum") is false &&
                                        line.Contains("XR Entity Tax ID") is false &&
                                        line.Contains("XR Entity Name") is false &&
                                        line.Contains("XR Deposit Number") is false &&
                                        line.Contains("XR Relationship") is false &&
                                        line.Contains("Entity Tax ID") is false &&
                                        line.Contains("Entity Name") is false &&
                                        line.Contains("Entity Status") is false &&
                                        line.Contains("Entity Status Date") is false &&
                                        line.Contains("Entity Security") is false &&
                                        line.Contains("Entity HousehodlID") is false
                                        )
                                    {
                                        if (line.Contains(">>DocDate: ") is true)
                                        {
                                            BeginSection.Add(line.Trim());
                                            BeginSection.Add("Autofill Status: CORETRACKER");
                                        }
                                        else
                                        {
                                            if (line.Contains(">>FileName:") is true)
                                            {
                                                FileName = "";
                                                FileFullName = "";
                                                Target = "";
                                                Source = "";

                                                FileName = line.Substring(line.LastIndexOf(@"\") + 1).Trim();
                                                FileFullName = line.Substring(line.LastIndexOf(":") + 1).Trim();

                                                Target = @"\\nauttest-vw3\g$\CoreTrackerDIPsync\DIPimages\" + FileName;

                                                if (DiskGroupNum is "113")
                                                {
                                                    Source = @"\\nautimg-vw4\Diskgroup$\Deposit\" + FileFullName;
                                                }

                                                if (DiskGroupNum is "111")
                                                {
                                                    Source = @"\\nautimg-vw4\Diskgroup$\Entity\" + FileFullName;
                                                }

                                                if (DiskGroupNum is "117")
                                                {
                                                    Source = @"\\nautimg-vw4\Diskgroup$\Workflow\" + FileFullName;
                                                }

                                                try
                                                {
                                                    File.Copy(Source, Target);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }

                                                BeginSection.Add(@">>FileName: \DIPImages\" + FileName);
                                            }
                                            else
                                            {
                                                BeginSection.Add(line.Trim());
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: In file " + file + " on line " + line + " with an error message of: " + ex.Message + " Press any key to exit.");
                                Console.ReadKey();
                                Environment.Exit(1);
                            }
                        }       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: On file " + file + " with an error message of: " + ex.Message + " Press any key to exit.");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message + " Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
