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
using System.IO;
using NetworkArithmeticGame;
using Newtonsoft.Json;

namespace NetworkArithmeticGame
{
    public partial class Form1 : Form
    {
       // Socket socket;
       // EndPoint epLocalPC, epRemotePC;
        double firstNumber, secondNumber, answer;
        int TogMove;
        /// <summary>   The value x coordinate. </summary>
        int MValX;
        /// <summary>   The value y coordinate. </summary>
        int MValY;
        Socket sck;
        Listener2 listener;

        List<Values> values = new List<Values>();

        LinkListNodeTwo valueNode = new LinkListNodeTwo();

        public Form1()
        {
            InitializeComponent();
            /*socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                     ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket,
                                   SocketOptionName.ReuseAddress, true);
            txtFirstNumber.Text = getLocalIPAddress();
            txtAnswer.Text = getLocalIPAddress();*/

            dataGrid.ColumnCount = 4;
            dataGrid.Columns[0].Name = "First Number";
            dataGrid.Columns[1].Name = "Operator";
            dataGrid.Columns[2].Name = "Second Number";
            dataGrid.Columns[3].Name = "Answer";

            listener = new Listener2(9);
            listener.SocketAccepted += new Listener2.SocketAcceptHandler2(listener_SocketAccepted);
            Load += new EventHandler(Form1_Load);
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                     ProtocolType.Tcp);
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Delay(7000).ContinueWith(t => connect());
            lbNotConnected.ForeColor = Color.FromArgb(193, 77, 77);
            listener.Start();
        }
        public void connect()
        {
            sck.Connect("127.0.0.1", 8);
            lbConnected.ForeColor = Color.FromArgb(110, 198, 51);
            lbNotConnected.ForeColor = Color.FromArgb(80, 80, 80);
        }
        void listener_SocketAccepted(System.Net.Sockets.Socket f)
        {
            Client2 client = new Client2(f);
            client.Received += new Client2.ClientReceivedHandler2(client_Received2);
            client.Disconnected += Client_Disconnected;

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.Tag = client;

            });
        }
        void client_Received2(Client2 sender2, byte[] data2 /* IAsyncResult aResult*/)
        {
            Invoke((MethodInvoker)delegate
            {
                if (txtAnswer.Text == Encoding.Default.GetString(data2))
                {
                    MessageBox.Show("You were correct");
                    txtFirstNumber.Text = "";
                    cboxOperator.Text = "";
                    txtSecondNumber.Text = "";
                    txtAnswer.Text = "";
                    btnSend.Enabled = true;

                }
                else if (txtAnswer.Text != Encoding.Default.GetString(data2))
                {
                    
                    Values numberValues = new Values(Convert.ToUInt16(txtFirstNumber.Text),
                       Convert.ToUInt16(txtSecondNumber.Text), cboxOperator.Text, Convert.ToUInt16(txtAnswer.Text), false);

                       //add to list to be displayed
                       values.Add(numberValues);

                       //create new node and add to nodelist
                       LinkListNode node = new LinkListNode(numberValues);
                       valueNode.AddValuesNode(node);

                       StringBuilder sb = new StringBuilder();
                       if (txtRichLinkedList.Text == "")
                       {
                           sb.Append("Head <-> ");
                           sb.Append(valueNode.getCurrentNode().NodeToString());
                       }
                       else
                       {
                           sb.Append(txtRichLinkedList.Text);
                           sb.Append(" <-> ");
                           sb.Append(valueNode.getCurrentNode().NodeToString());
                       }

                       txtRichLinkedList.Text = sb.ToString();
                    MessageBox.Show("You were incorrect");

                    txtFirstNumber.Text = "";
                    cboxOperator.Text = "";
                    txtSecondNumber.Text = "";
                    txtAnswer.Text = "";
                    btnSend.Enabled = true;
                }

            }); 
                }
        private void Client_Disconnected(Client2 sender2)
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
        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1; MValX = e.X; MValY = e.Y;
        }
        private void topPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }
        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        
        /*private string getLocalIPAddress()
        {
            IPHostEntry localHost; // entry for local pc
            localHost = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in localHost.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString(); // once found return the ip address
                }
            }
            return "127.0.0.1";
        }
        private void MessageCallBack(IAsyncResult aResult)
        {
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
                    listBox1.Items.Add("Remote : " + recievedMessage);

                }
                byte[] buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                                        ref epRemotePC, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }*/
        private void lbBirthMonth_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        

        private void button2_MouseEnter(object sender, EventArgs e)
        {

        }

        

        private void btnDisplay_MouseLeave(object sender, EventArgs e)
        {

        }

        private void cboxOperator_TextUpdate(object sender, EventArgs e)
        {

        }

        private void btnSort1_Click(object sender, EventArgs e)
        {

        }

        public void LoadQuestionsDataGridView()
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                
                ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] message = new byte[1500];
                message = enc.GetBytes(txtFirstNumber.Text + " " + cboxOperator.Text + " " + txtSecondNumber.Text);// Get the string from the textbox and convert it to raw bytes
                sck.Send(message);

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
            addDataGrid(txtFirstNumber.Text, cboxOperator.Text, txtSecondNumber.Text, txtAnswer.Text);

            //dataGrid.AutoGenerateColumns = false;
            

            //RefreshResultDatagrid();


            /*
            DataGridViewTextBoxColumn columnFirst = new DataGridViewTextBoxColumn();
            columnFirst.DataPropertyName = txtFirstNumber.Text;
            columnFirst.Name = "First Number";
            dataGrid.Columns.Add(columnFirst);


            DataGridViewTextBoxColumn columnOperator = new DataGridViewTextBoxColumn();
            columnOperator.DataPropertyName = cboxOperator.Text;
            columnOperator.Name = "Operator";
            dataGrid.Columns.Add(columnOperator);

            DataGridViewTextBoxColumn columnSecond = new DataGridViewTextBoxColumn();
            columnSecond.DataPropertyName = txtSecondNumber.Text;
            columnSecond.Name = "Second Number";
            dataGrid.Columns.Add(columnSecond);

            DataGridViewTextBoxColumn columnResult = new DataGridViewTextBoxColumn();
            columnResult.DataPropertyName = txtAnswer.Text;
            columnResult.Name = "Answer";
            dataGrid.Columns.Add(columnResult);*/
            /*
            try
            {
                epLocalPC = new IPEndPoint(IPAddress.Parse(txtFirstNumber.Text),
                            Convert.ToInt32(txtFirstNumber.Text)); // Initialise
                                                                 // The localEndPoint
                socket.Bind(epLocalPC); //Bind the local end point to the socket

                epRemotePC = new IPEndPoint(IPAddress.Parse(txtAnswer.Text),
                           Convert.ToInt32(txtAnswer.Text)); // Initialise
                                                                 // The localEndPoint
                socket.Connect(epRemotePC); //Connect the remote end point to the to the socket

                byte[] buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref epRemotePC, new AsyncCallback(MessageCallBack), buffer);

                btnSend.Enabled = false; // connection is established
                btnSend.Text = "Connected";

                // allow the user to send message.
                //btnSend.Enabled = true;
                txtSecondNumber.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
            if (txtFirstNumber.Text == "" || txtSecondNumber.Text == "" || txtAnswer.Text == "" || cboxOperator.Text == "")
            {
                MessageBox.Show("Please input values to send");
            }
            else
            { 
            //txtFirstNumber.Text = "";
            //txtSecondNumber.Text = "";
            //cboxOperator.Text = "";
            //txtAnswer.Text = "";
            btnSend.Enabled = false;
            }
        }

        private void addDataGrid(string firstName, string Operator, string secondNumber, string answer)
        {
            String[] row = { firstName, Operator, secondNumber, answer };

            dataGrid.Rows.Add(row);
        }

        private void txtSecondNumber_TextChanged(object sender, EventArgs e)
        {
            if (cboxOperator.Text == "+")
            {
                if (txtSecondNumber.Text != "")
                {
                    firstNumber = Convert.ToDouble(txtFirstNumber.Text);
                    secondNumber = Convert.ToDouble(txtSecondNumber.Text);
                    answer = firstNumber + secondNumber;
                    txtAnswer.Text = answer.ToString();
                }
                else
                {

                }
            }
            else if (cboxOperator.Text == "-")
            {
                if (txtSecondNumber.Text != "") { 
                firstNumber = Convert.ToDouble(txtFirstNumber.Text);
                secondNumber = Convert.ToDouble(txtSecondNumber.Text);
                answer = firstNumber - secondNumber;
                txtAnswer.Text = answer.ToString();
                }
                else
                {

                }

            }
            else if (cboxOperator.Text == "x")
            {
                if (txtSecondNumber.Text != "")
                {
                    firstNumber = Convert.ToDouble(txtFirstNumber.Text);
                    secondNumber = Convert.ToDouble(txtSecondNumber.Text);
                    answer = firstNumber * secondNumber;
                    txtAnswer.Text = answer.ToString();
                }
                else
                {

                }
            }
            else if (cboxOperator.Text == "/")
            {
                if (txtSecondNumber.Text != "")
                {
                    firstNumber = Convert.ToDouble(txtFirstNumber.Text);
                    secondNumber = Convert.ToDouble(txtSecondNumber.Text);
                    answer = firstNumber / secondNumber;
                    txtAnswer.Text = answer.ToString();
                }
                else
                {

                }
            } 
            else
            {
                //txtSecondNumber.Text = "";
                //MessageBox.Show("Please Select a operator");
            }

        }

        private void cboxOperator_TextChanged(object sender, EventArgs e)
        {
            txtAnswer.Text = "";
            txtSecondNumber.Text = "";
        }
    }
}
