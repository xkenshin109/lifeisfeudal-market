using System;
using System.IO;
using ClosedXML;
using ClosedXML.Excel;

namespace MarketWorkbook
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:/temp/data.xlsx";
            using(var wb = new ClosedXML.Excel.XLWorkbook(filePath))
            {
                var ws = wb.Worksheet(1);
                var count = 1;
                foreach(IXLRow row in ws.RowsUsed())
                {
                    if(count == 5)
                    {
                        Console.WriteLine($"<<{(row.Cell("A").Active?"Active":"Inactive")}>> Name: " + row.Cell("A").GetValue<string>());
                        Console.WriteLine($"<<{(row.Cell("C").Active ? "Active" : "Inactive")}>> Buy Price: " + row.Cell("C").GetValue<string>());
                        Console.WriteLine($"<<{(row.Cell("D").Active ? "Active" : "Inactive")}>> Sell price: " + row.Cell("D").GetValue<string>());
                        Console.WriteLine($"<<{(row.Cell("F").Active ? "Active" : "Inactive")}>> Buy Price: " + row.Cell("F").GetValue<string>());
                        Console.WriteLine($"<<{(row.Cell("G").Active ? "Active" : "Inactive")}>> Sell Price: " + row.Cell("G").GetValue<string>());
                        Console.WriteLine($"<<{(row.Cell("H").Active ? "Active" : "Inactive")}>> Sell Price: " + row.Cell("H").GetValue<string>());
                    }
                    else
                    {
                        count++;
                    }
                }

            }        
        }
    }
}
