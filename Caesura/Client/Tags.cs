using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server;
using Microsoft.Win32;


namespace Client
{
    public partial class Tags : Form
    {
        private Client client;


        public Tags()
        {
            client = new Client();
            InitializeComponent();

            //Add in all tags
            var items = listBox1.Items;
            List<String> temp = Search.database.getListOfAllTags();

            foreach (String x in temp)
            {
                items.Add(x);
            }
            }

    }
}
