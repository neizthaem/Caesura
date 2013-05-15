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
    public partial class Login : Form
    {


        public int state;
        public string pass;
        public string username;
        public Login()
        {
            this.state = 0;
            
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.state = 1;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.username = textBox1.Text;
            this.pass = textBox2.Text;
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.username = textBox1.Text;
                this.pass = textBox2.Text;
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ja-JP");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            }
            this.refreshText();
        }

        private void refreshText()
        {
            this.label1.Text = strings.Username;
            this.label2.Text = strings.Password;
            this.button1.Text = strings.Login;
            this.button2.Text = strings.Quit;
            this.Text = strings.Title;
            
        }
    }
}
