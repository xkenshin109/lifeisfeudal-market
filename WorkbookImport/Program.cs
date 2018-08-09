using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using CoreManagement;
using CoreManagement.Models;

namespace WorkbookImport
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterWorkbook.ReadFile(@"C:\Users\Jeremy\Downloads\Caridia Market Sheet 4.0.xlsx");
        }
    }
}
