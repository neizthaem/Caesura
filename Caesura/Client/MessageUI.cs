using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MessageUI : Form
    {
        public Client client;
        public MessageUI(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = client.checkMail();
            if (temp != null)
            {
                foreach (Mail m in temp)
                {
                    addMailItem(m.id, m.username, m.message);
                }
            }
           
        }

        private void addMailItem(int id, String from, String message)
        {
            ListViewItem i = new ListViewItem();
            i.Text = id.ToString();
            i.SubItems.Add(from);
            i.SubItems.Add(message);
            listView1.Items.Add(i);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void MessageUI_Load(object sender, EventArgs e)
        {
            //addMailItem(0, "Neizthaem", "Jeff is Bad");
            //addMailItem(1, "Neizthaem", "lolololol");
        }
    }
}
