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
    public partial class Login : Form
    {
        public bool quit;
        public string pass;
        public string username;
        public Login()
        {
            this.quit = false;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.quit = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.username = textBox1.Text;
            this.pass = textBox2.Text;
            this.Close();
        }
    }
}
