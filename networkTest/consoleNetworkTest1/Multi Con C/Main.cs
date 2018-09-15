using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Multi_Con_C
{
    public partial class Main : Form
    {
        public static string getMessage;
        Socket sck;
        public Main()
        {
            InitializeComponent();
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                     ProtocolType.Udp);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            sck.Connect("127.0.0.1", 8);
            MessageBox.Show("Connected");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            /* int s = sck.Send(Encoding.Default.GetBytes(txtMsg.Text));

             if (s > 0)
             {
                 MessageBox.Show("Data Sent");
             } */

            try
            {
                ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] message = new byte[1500];
                message = enc.GetBytes(txtMsg.Text);// Get the string from the textbox and convert it to raw bytes
                sck.Send(message);
                Main.getMessage = message.ToString();
                // Add your message to the listbox 
                //lstMessage.Items.Add(txtSendMessage.Text + txtSendMessage2.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            sck.Close();
            sck.Dispose();
            Close();
        }
    }
}
