using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace consoleNetworkTest1
{
    public partial class Main : Form
    {
        private static string _getMessage;
        public static string getMessage
        {
            get
            {
                return _getMessage;
            }
        }
        Socket socket;
        EndPoint epLocalPC, epRemotePC;
        Listener listener;
        public Main()
        {
            InitializeComponent();
            _getMessage = Main.getMessage;
            listener = new Listener(8);
            listener.SocketAccepted += new Listener.SocketAcceptHandler(listener_SocketAccepted);
            Load += new EventHandler(Main_Load);
        }

        void Main_Load(object sender, EventArgs e)
        {
            listener.Start();
        }

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            //client.Received += new Client.ClientReceivedHandler(client_Received);
            client.Disconnected += Client_Disconnected;

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.SubItems.Add(client.ID);
                i.SubItems.Add("XX");
                i.SubItems.Add("XX");
                i.Tag = client;
                lstClients.Items.Add(i);

                //lstInput.Items.Add(i);
            });
        }

        private void Client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClients.Items.Count; i++)
                {
                    Client client = lstClients.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {
                        lstClients.Items.RemoveAt(i);
                        break;
                    }
                }
            });
        }
        void client_Received(/*Client sender, byte[] data,*/ IAsyncResult aResult)
        {
            /* Invoke((MethodInvoker)delegate
             {
                 for (int i = 0; i < lstClients.Items.Count; i++)
                 {
                     Client client = lstClients.Items[i].Tag as Client;

                     if (client.ID == sender.ID)
                     {
                         lstClients.Items[i].SubItems[2].Text = Encoding.Default.GetString(data);
                         lstClients.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                         break;
                     }
                 }
             }); */

            int size = socket.EndReceiveFrom(aResult, ref epRemotePC);
            // return the size of bytes in message 
            try
            {
                if (size > 0) // seeing if there is bytes on the network port
                {
                    byte[] socketData = new byte[1464];
                    socketData = (byte[])aResult.AsyncState; // get the bytes
                                                             //Convert the bytes into a ASCII string
                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string recievedMessage = eEncoding.GetString(socketData);
                    lstInput.Items.Add(recievedMessage);

                }
                byte[] buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                                        ref epRemotePC, new AsyncCallback(client_Received), buffer);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
