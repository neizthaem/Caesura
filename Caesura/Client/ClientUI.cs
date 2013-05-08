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
using System.Threading;

namespace Client
{
    public partial class ClientUI : Form
    {
        private bool connect;
        private Client client;

        public ClientUI()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.connect = false;
            updateItems();
        }

        private void ClientUI_Load(object sender, EventArgs e)
        {
            //client = new Client();
            loginWin();
        }

        private void loginWin()
        {
            Login login = new Login();
            login.ShowDialog();
            if (!(login.quit))
            {

                try
                {
                    client = new Client();
                    client.connect();
                }
                catch (Exception)
                {
                    MessageBox.Show("Server is not running");
                    this.Close();

                }


                client.login(login.username, login.pass);

                if (!(client.loggedIn))
                {
                    MessageBox.Show("Bad username/password");
                    Thread.Sleep(2000);
                    loginWin();
                }
                else
                {
                    this.connect = true;
                }

            }
            else
            {
                this.Close();
            }
        }
        private void ClientUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.connect)
            {
                client.disconnect();
            }
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                updateItems();
        }

        private void updateItems()
        {
            viewer.Items.Clear();


            if (locationsList.CheckedIndices.Contains(1))
            {
                var tags = searchBox.Text.Split(' ').ToList<String>();
                var matches = client.getFromTag(tags);

                foreach (String f in matches)
                {
                    var newItem = new ListViewItem(f);
                    newItem.ImageIndex = 0;
                    viewer.Items.Add(newItem);
                }
            }
            else
            {
                if (locationsList.CheckedIndices.Contains(0))
                {
                    List<String> temp = client.getOwned();

                    foreach (String f in temp)
                    {
                        var newItem = new ListViewItem(f);
                        newItem.ImageIndex = 0;
                        viewer.Items.Add(newItem);
                    }


                }
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

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void locationsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void viewer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void locationsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (locationsList.CheckedItems.Count >= 1 && e.CurrentValue != CheckState.Checked)
            {
                //e.NewValue = e.CurrentValue;
                locationsList.SetItemCheckState(0, CheckState.Unchecked);
                locationsList.SetItemCheckState(1, CheckState.Unchecked);
                locationsList.SetItemCheckState(e.Index, CheckState.Checked);

            }
        }

        private void tagsButton_Click(object sender, EventArgs e)
        {
            Tags temp = new Tags();
            temp.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
