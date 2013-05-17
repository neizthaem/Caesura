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
    public partial class SendForm : Form
    {

        public Client client;

        public SendForm(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String to = textBox1.Text;
            String message = textBox2.Text;
            client.sendMessage(to, message);
            MessageBox.Show("Message sent");
            this.Close();
        }
    }
}
