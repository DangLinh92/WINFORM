using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Wisol.MES.Classes
{
    public static class PrinterClass
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
    }
}
