using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Diagnostics;
using Structure;
using System.Runtime.InteropServices;
using System.Threading;
using AMS.Profile;
using System.Windows.Forms;
using System.Data;

namespace Client
{

    internal class MainClass
    {
       
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]
        private static void Main()
        {
           
      if(mutex.WaitOne(TimeSpan.Zero, true)) {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run(new MDIParent1());
            mutex.ReleaseMutex();
        } else {
            MessageBox.Show("only one instance at a time");
        }

        }
      
    }
  
}
