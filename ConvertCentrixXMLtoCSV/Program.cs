using System;
using System.IO;
using System.Xml;

namespace ConvertCentrixXMLtoCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileNameIn;
            string FileNameOut;
            FileNameIn = args[0];
            FileNameOut = args[1];

            //FileNameIn = @"C:\Users\mikec\OneDrive\Documents\AgileBanking\CB\index.xml";
            //FileNameOut = @"C:\Users\mikec\OneDrive\Documents\AgileBanking\CB\index2.xml";

            string IndexName = "";

            string AccountNumber = "";
            string DocDate = "";
            string DocTypeName = "";
            string DisputeID = "";
            string DisputeReason = "";
            string FileFormat = "";

            string FileName = "";
          

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(FileNameIn);
            XmlNodeList elemList = XmlDoc.GetElementsByTagName("Document");
            for (int i = 0; i < elemList.Count; i++)
            {                
                for (int v = 0; v < elemList[i].ChildNodes.Count; v++)
                {
                    for (int f = 0; f < elemList[i].ChildNodes[v].ChildNodes.Count; f++)
                    {
                        string InnerText = elemList[i].ChildNodes[v].ChildNodes.Item(f).InnerText;
                        string OuterXml = elemList[i].ChildNodes[v].ChildNodes.Item(f).OuterXml;
                        if (elemList[i].ChildNodes[v].ChildNodes.Item(f).Name == "Index")
                            IndexName = OuterXml.Substring(OuterXml.IndexOf('"') + 1, OuterXml.LastIndexOf('"') - OuterXml.IndexOf('"') - 1);

                        if (IndexName == "ACCOUNT NUMBER")
                            AccountNumber = InnerText;                        
                        if (IndexName == "DOC DATE")
                            DocDate = InnerText;
                        if (IndexName == "DOC TYPE NAME")
                            DocTypeName = InnerText;
                        if (IndexName == "DISPUTE ID")
                            DisputeID = InnerText;
                        if (IndexName == "DISPUTE REASON")
                            DisputeReason = InnerText;
                        if (IndexName == "FILE FORMAT")
                            FileFormat = InnerText;

                        if (elemList[i].ChildNodes[v].ChildNodes.Item(f).Name == "Page")
                            FileName = InnerText;
                    }                    
                }
                //Console.WriteLine(FilePathOut + FileName + ".csv");
                //Console.WriteLine(AccountNumber + ',' + DocDate + ',' + DocTypeName + ',' + DisputeID + ',' + DisputeReason + ',' + FileFormat);
                File.AppendAllText(FileNameOut, AccountNumber + ',' + DocDate + ',' + DocTypeName + ',' + DisputeID + ',' + DisputeReason + ',' + FileFormat + ','+ @"D:\Nautilus\Centrix\" + FileName + "\r\n");
            }
        }
    }
}
