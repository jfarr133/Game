////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	StudentForm.cs
//
// summary:	Implements the student Windows Form
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to give all the buttons and GUI function for the student form. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace StudentForm
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Form for viewing the student. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class StudentForm : Form
    {
        /// <summary>   The second sck. </summary>
        Socket sck2;
        /// <summary>   The listener. </summary>
        Listener listener;
        /// <summary>   The values student. </summary>
        static ValueStudent valuesStudent;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public StudentForm()
        {
            InitializeComponent();
            listener = new Listener(8);
            listener.SocketAccepted += new Listener.SocketAcceptHandler(listener_SocketAccepted);
            Load += new EventHandler(Form1_Load);
            sck2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                     ProtocolType.Tcp);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by Form1 for load events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            // Delay connection for 7 seconds
            Task.Delay(7000).ContinueWith(t => connect());
            lbNotConnected.ForeColor = Color.FromArgb(193, 77, 77);
            btnSubmit.Enabled = false;
            btnSubmit.Text = "Loading";
            listener.Start();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Connects this object. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void connect()
        {
            //Connect to selected IP
            sck2.Connect("127.0.0.1", 9);
            lbConnected.ForeColor = Color.FromArgb(87, 160, 38);
            lbNotConnected.ForeColor = Color.Silver;
            btnSubmit.Enabled = true;
            btnSubmit.Text = "Submit";
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Listener socket accepted. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="e">    A Socket to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += new Client.ClientReceivedHandler(client_Received);
            client.Disconnected += Client_Disconnected;

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.Tag = client;
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Client disconnected. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Client_Disconnected(Client sender)
        {
           
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Client received. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="data">     The data. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void client_Received(Client sender, byte[] data)
        {
            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = Encoding.Default.GetString(data);
                lstQuestion.Items.Add(i);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnSubmit for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstQuestion.Items.Count == 0 || txtAnswer.Text == "")
                {
                    MessageBox.Show("Incorrect values");
                }
                else 
                {
                    // Send txtAnswer value back to teacher
                    ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    byte[] messageTwo = new byte[2000];
                    messageTwo = enc.GetBytes(txtAnswer.Text);
                    sck2.Send(messageTwo);

                    Invoke((MethodInvoker)delegate
                    {
                        for (int i = 0; i < lstQuestion.Items.Count; i++)
                        {
                            Client client = lstQuestion.Items[i].Tag as Client;

                            lstQuestion.Items.RemoveAt(i);
                            break;
                        }
                    });
                }
                txtAnswer.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
