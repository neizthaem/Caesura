using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Client
{
    public partial class Tags : Form
    {
        private Client client;


        public Tags(Client client)
        {
            this.client = client;
            InitializeComponent();

            //Add in all tags
            var items = listBox1.Items;
            List<String> temp = client.getListOfAllTags();

            foreach (String x in temp)
            {
                items.Add(x);
            }
            }

    }
}
