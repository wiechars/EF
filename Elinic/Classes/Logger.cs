using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Elinic.Classes
{
    public class Logger
    {

        public  void LogErrorMessage(String e)
        {
            using (StreamWriter w = File.AppendText(("c:\\Elinic\\errorlog.txt")))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", e);
                w.WriteLine("-------------------------------");
                // Update the underlying file.
                w.Flush();
            }
        }
    }
}