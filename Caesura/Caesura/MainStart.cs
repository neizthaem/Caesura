using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesura
{
    public class MainStart
    {

        public static void Main()
        {
            UserRegGUI test = new UserRegGUI();

            Application.EnableVisualStyles();
            Application.Run(new UserRegGUI());
            

        }

    }
}
