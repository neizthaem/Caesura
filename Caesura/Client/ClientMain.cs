using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    
    public class ClientMain
    {
        [STAThread]
        public static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            //Thread.CurrentThread.
            Application.EnableVisualStyles();
            Application.Run(new ClientGUI());


        }

    }
}