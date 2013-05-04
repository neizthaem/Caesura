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
    public partial class ClientUI : Form
    {

        private Client client;

        public ClientUI()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            updateItems();
        }

        private void ClientUI_Load(object sender, EventArgs e)
        {
            client = new Client();

            client.connect();
            client.login("Testuser", "Test");
        }

        private void ClientUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.disconnect();
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                updateItems();
        }

        private void updateItems()
        {
            viewer.Items.Clear();

            var tags = searchBox.Text.Split(' ').ToList<String>();
            var matches = client.getFromTag(tags);

            foreach(String f in matches)
            {
                var newItem = new ListViewItem(f);
                newItem.ImageIndex = 0;
                viewer.Items.Add(newItem);
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {

            ListViewItem i = viewer.SelectedItems[0];            

            string file = i.Text;

            FileDialog fdialog = new SaveFileDialog();
            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                client.requestFile(file, fdialog.FileName);
                Search.database.AddOwnership("Testuser", file);
            }
        }

    }
}
