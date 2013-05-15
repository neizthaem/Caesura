using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace Client
{
    public partial class ClientUI : Form
    {
        private bool connect;
        private Client client;

        public ClientUI()
        {
            this.InitializeComponent();
            this.refreshText();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.connect = false;
            updateItems();
        }

        private void ClientUI_Load(object sender, EventArgs e)
        {
            while (loginWin() == false) { };
            this.refreshText();
            locationsList.SetItemCheckState(1, CheckState.Checked);
        }

        private bool loginWin()
        {
            Login login = new Login();
            login.ShowDialog();

            if (login.state == 0)
            {
                this.Close();
                return true;
            }
            

            try
            {
                client = new Client();
                client.connect();
                if (login.state == 2)
                {
                    if (!(client.register(login.username, login.pass)))
                    {
                        MessageBox.Show(strings.alreadyRegistered);
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show(strings.serverTimeout);
                return false;
            }

            client.login(login.username, login.pass);

            if (client.loggedIn)
                this.connect = true;
            else
            {
                MessageBox.Show(strings.incorrect);
                return false;
            }

            return true;

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
            // Empty the current items
            viewer.Items.Clear();
            
            // Parse the tags to be used
            var tags = searchBox.Text.Split(' ').ToList();
            while (tags.Remove(String.Empty)) { };

            // Determine the search location(s) / Method
            var source = 0;
            if (locationsList.CheckedIndices.Contains(0))
                source = 0;
            if (locationsList.CheckedIndices.Contains(1))
                source = 1;

            // Apply the respective search procedure and collect results
            var matches = new List<String>();
            switch (source)
            {
                case 0: // Owned Files Source
                    if (searchBox.Text == "")
                        matches = client.getOwned();
                    else
                        matches = client.getFilesWithTagsOwnedBy(client.username, tags.ToArray());
                    break;
                case 1: // Server Files
                    matches = client.getFromTag(tags);
                    break;
                default:
                    Console.WriteLine("Unhandled source case statement assuming case 1");
                    break;
            }

            // Create the view items and add them
            foreach (String f in matches)
            {
                var newItem = new ListViewItem(f);
                String ext = null;
                if (f.Length > 4) { ext = f.Substring(f.Length - 3); }

                switch (ext)
                {
                    case "txt":
                        newItem.ImageIndex = 1;
                        break;
                    case "jpg":
                        newItem.ImageIndex = 2;
                        break;
                    case "png":
                        newItem.ImageIndex = 3;
                        break;
                    case "mp3":
                        newItem.ImageIndex = 4;
                        break;
                    case "mkv":
                        newItem.ImageIndex = 5;
                        break;
                    case "mov":
                        newItem.ImageIndex = 5;
                        break;
                    case "avi":
                        newItem.ImageIndex = 5;
                        break;
                    default:
                        newItem.ImageIndex = 0;
                        break;
                }

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
                client.AddOwnership(client.username, file);
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            this.refreshText();
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
            Tags temp = new Tags(client);
            temp.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void jAPANESEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ja-JP");
            this.refreshText();
        }


        private void refreshText()
        {
            this.downloadButton.Text = strings.Download;
            this.searchButton.Text = strings.Search;
            this.label1.Text = strings.TagSearch;
            this.tagsButton.Text = strings.TagButton;
            this.label2.Text = strings.SearchLocation;
            this.inboxButton.Text = strings.InboxButton;
            this.fileToolStripMenuItem.Text = strings.File;
            this.hELPToolStripMenuItem.Text = strings.Help;
            this.mESSAGEToolStripMenuItem.Text = strings.Messages;
            this.exitToolStripMenuItem.Text = strings.Exit;
            this.aboutToolStripMenuItem.Text = strings.About;
            this.inboxToolStripMenuItem.Text = strings.Inbox;
            this.sendToolStripMenuItem.Text = strings.Send;
            this.toolStripMenuItem1.Text = strings.Language;
        }

        private void inboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageUI message = new MessageUI(client);
            message.ShowDialog();
        }
    }
}
