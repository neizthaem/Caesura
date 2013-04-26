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

namespace Client
{
    public partial class ClientGUI : Form
    {
        private Client client;



        public ClientGUI()
        {
            client = new Client();
            InitializeComponent();

            //Add in all tags
            var items = checkedListBox1.Items;
            List<String> temp = Search.database.getListOfAllTags();

            foreach (String x in temp)
            {
                items.Add(x);
            }


            client.connect();
            client.login("Testuser", "Test");

        }





        private void button1_Click(object sender, EventArgs e)
        {
            List<string> tags = checkedListBox1.CheckedItems.Cast<string>().ToList();

            HashSet<String> files = new HashSet<String>();
            var fileTemp = client.getFromTag(tags);

            foreach (String x in fileTemp)
            {
                files.Add(x);
            }

            fileTemp = files.ToList();
            fileTemp.Sort();
            listBox1.DataSource = fileTemp;


        }

        private void button2_Click(object sender, EventArgs e)
        {

            string file = listBox1.SelectedItem.ToString();

            if (System.IO.File.Exists("C:\\Caesura\\" + file))
            {
                System.IO.File.Delete("C:\\Caesura\\" + file);
            }

            client.requestFile(file);



        }

        private void ClientGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.disconnect();
        }
    }
}
