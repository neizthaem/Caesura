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
        public MessageUI()
        {
            InitializeComponent();
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // client.checkMail
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
