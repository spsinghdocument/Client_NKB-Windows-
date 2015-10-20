using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.LogWriterClass
{
   internal class logwritercls
    {
        public static void logs(string  filename ,string fileval )
        {
        try
        {
        using (StreamWriter w = new StreamWriter(Environment.CurrentDirectory+Path.DirectorySeparatorChar+ System.DateTime.Now.DayOfWeek.ToString() + filename + ".txt", true))
        {
            w.Write("Date : " + System.DateTime.Now.ToShortDateString() + " Time    :  " + System.DateTime.Now.ToShortTimeString() + "  Second : " + System.DateTime.Now.Second.ToString()+"     milisecond  :  "+ System.DateTime.Now.Millisecond.ToString()+"====>>>>>>>");
            w.WriteLine(fileval);
        }
        }
        catch 
        {
           
        }
        }
    }
}
