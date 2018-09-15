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

namespace StudentForm
{
    public partial class Form1 : Form
    {
        Socket sck2;
        Listener listener;
        static ValueStudent valuesStudent;
        

        public Form1()
        {
            InitializeComponent();
            listener = new Listener(8);
            listener.SocketAccepted += new Listener.SocketAcceptHandler(listener_SocketAccepted);
            Load += new EventHandler(Form1_Load);
            sck2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                     ProtocolType.Tcp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Delay(7000).ContinueWith(t => connect());
            lbNotConnected.ForeColor = Color.FromArgb(193, 77, 77);
            //sck2.Connect("127.0.0.1", 9);
            listener.Start();
        }
        public void connect()
        {
            sck2.Connect("127.0.0.1", 9);
            lbConnected.ForeColor = Color.FromArgb(87, 160, 38);
            lbNotConnected.ForeColor = Color.Silver;
        }
        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += new Client.ClientReceivedHandler(client_Received);
            client.Disconnected += Client_Disconnected;

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                // i.SubItems.Add(client.ID);
                // i.SubItems.Add("XX");
                // i.SubItems.Add("XX");
                i.Tag = client;
                //lstClients.Items.Add(i);

                //lstInput.Items.Add(i);
            });
        }
        private void Client_Disconnected(Client sender)
        {
           /* Invoke((MethodInvoker)delegate
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
            }); */
        }
        void client_Received(Client sender, byte[] data /* IAsyncResult aResult*/)
        {
            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = Encoding.Default.GetString(data);
                lstQuestion.Items.Add(i);
                // lstClients.Items[i].SubItems[2].Text = Encoding.Default.GetString(data);
                // lstClients.Items[i].SubItems[3].Text = DateTime.Now.ToString();



            });
            /*
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
           } */
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                //sck2.Connect("127.0.0.1", 9);
                ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] messageTwo = new byte[2000];
                messageTwo = enc.GetBytes(txtAnswer.Text);// Get the string from the textbox and convert it to raw bytes
                sck2.Send(messageTwo);

                
                //Close();

                txtAnswer.Text = "";

                Invoke((MethodInvoker)delegate
                {
                    for (int i = 0; i < lstQuestion.Items.Count; i++)
                    {
                        Client client = lstQuestion.Items[i].Tag as Client;

                        lstQuestion.Items.RemoveAt(i);
                        break;
                    }
                });
                //byte[] answer = new byte[1600];
                //answer = enc.GetBytes(txtAnswer.Text);// Get the string from the textbox and convert it to raw bytes
                //sck.Send(answer);


                //MainSmall.getMessage = message.ToString();
                // Add your message to the listbox 
                //lstMessage.Items.Add(txtSendMessage.Text + txtSendMessage2.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            //sck2.Close();
            //sck2.Dispose();

            /*if (txtAnswer.Text == valuesStudent.Result.ToString())
            {
                //valuesStudent.IsCorrect = true;
                MessageBox.Show("Correct!!!");
                //string json = JsonConvert.SerializeObject(valuesStudent);
                //SendMessage(json);
            }
            else
            {
                //valuesStudent.IsCorrect = false;
                MessageBox.Show("Incorrect");
                //string json = JsonConvert.SerializeObject(valuesStudent);
                //SendMessage(json);
            }*/
        }
    }
}
