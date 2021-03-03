using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sentier2._0
{
    public class consoleUtility
    {
        public consoleUtility() { }
        public void logTime(string str) {
            Console.WriteLine(DateTime.Now.ToString() + ", " + str + " called.");
        }
    }
}
