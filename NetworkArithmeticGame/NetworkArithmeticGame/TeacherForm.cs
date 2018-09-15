////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Form1.cs
//
// summary:	Implements the form 1 class
////////////////////////////////////////////////////////////////////////////////////////////////////

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

using System.IO;
using NetworkArithmeticGame;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Sockets;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to give all the buttons and GUI function for the teacher form. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A form 1. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class TeacherForm : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the answer. </summary>
        ///
        /// <value> The answer. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        double firstNumber, secondNumber, answer;
        /// <summary>   The tog move. </summary>
        int TogMove;
        /// <summary>   The value x coordinate. </summary>
        int MValX;
        /// <summary>   The value y coordinate. </summary>
        int MValY;

        /// <summary>   The sck. </summary>
        Socket sck;
        /// <summary>   The listener. </summary>
        Listener2 listener;
        /// <summary>   The values. </summary>
        List<Values> values = new List<Values>();
        /// <summary>   The equation. </summary>
        Values2 equation;
        /// <summary>   The value node. </summary>
        LinkListNode2 valueNode = new LinkListNode2();
        /// <summary>   The binary tree. </summary>
        NetworkBinaryTree binaryTree = new NetworkBinaryTree();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TeacherForm()
        {
            InitializeComponent();

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
            Task.Delay(7000).ContinueWith(t => connect());
            lbNotConnected.ForeColor = Color.FromArgb(193, 77, 77);
            listener.Start();
            btnSend.Enabled = false;
            btnSend.Text = "Please Wait";
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Connects this object. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void connect()
        {
            btnSend.Enabled = true;
            sck.Connect("127.0.0.1", 8);
            lbConnected.ForeColor = Color.FromArgb(110, 198, 51);
            lbNotConnected.ForeColor = Color.FromArgb(80, 80, 80);
            btnSend.Text = "Send";
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Listener socket accepted. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="f">    A Socket to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Client received 2. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender2">  The second sender. </param>
        /// <param name="data2">    The second data. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void client_Received2(Client2 sender2, byte[] data2)
        {
            equation = new Values2(Convert.ToInt32(txtFirstNumber.Text), Convert.ToInt32(txtSecondNumber.Text), cboxOperator.Text, Convert.ToInt32(txtAnswer.Text));
            binaryTree2();

            Invoke((MethodInvoker)delegate
            {
                if (txtAnswer.Text == Encoding.Default.GetString(data2))
                {
                    MessageBox.Show("You were correct");
                    btnSend.Enabled = true;
                }
                else if (txtAnswer.Text != Encoding.Default.GetString(data2))
                {
                    int num;
                    bool answer = int.TryParse(txtAnswer.Text, out num);
                    valueNode.AddValuesNode(new LinkListNode(num));
                    LinkedListNodes();
                    MessageBox.Show("You were incorrect");
                }
                
            });

            btnSend.Enabled = true;
            txtFirstNumber.Text = "";
            cboxOperator.Text = "";
            txtSecondNumber.Text = "";
            txtAnswer.Text = "";
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Linked list nodes. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void LinkedListNodes()
        {
            string equation = txtFirstNumber.Text + cboxOperator.Text + txtSecondNumber.Text + "=";
            StringBuilder sb = new StringBuilder();
            if (txtRichLinkedList.Text == "")
            {
                sb.Append("HEAD <-> ");
                sb.Append(equation + valueNode.getCurrentNode().tostring());
            }
            else
            {
                sb.Append(txtRichLinkedList.Text);
                sb.Append(" <-> ");
                sb.Append(equation + valueNode.getCurrentNode().tostring());
            }
            txtRichLinkedList.Text = sb.ToString();
            
            //return sb.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Binary tree 2. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void binaryTree2()
        {
            if (binaryTree.top == null)
            {
                binaryTree.top = new NetworkBinaryTreeNode(equation);
            }
            else
            {
                binaryTree.Add(equation);
            }
            txtLinkedList.Clear();
            txtLinkedList.Text = "IN-ORDER: ";
            txtLinkedList.Text += binaryTree.printInOrder(binaryTree);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Client disconnected. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender2">  The second sender. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Client_Disconnected(Client2 sender2)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by topPanel for mouse down events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Mouse event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1; MValX = e.X; MValY = e.Y;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by topPanel for mouse up events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Mouse event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void topPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by topPanel for mouse move events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Mouse event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnExit for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnMinimize for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnSort1 for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSort1_Click(object sender, EventArgs e)
        {
            dataGrid.Sort(dataGrid.Columns[0], ListSortDirection.Ascending);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnSort2 for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSort2_Click(object sender, EventArgs e)
        {
            dataGrid.Sort(dataGrid.Columns[0], ListSortDirection.Descending);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnSort3 for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSort3_Click(object sender, EventArgs e)
        {
            dataGrid.Sort(dataGrid.Columns[1], ListSortDirection.Descending);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Hash demo. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void hashDemo()
        {
            Hashtable HashTable = new Hashtable();

            HashTable.Add("First Number", txtFirstNumber.Text);
            HashTable.Add("Operator", cboxOperator.Text);
            HashTable.Add("Second Number", txtSecondNumber.Text);
            HashTable.Add("Answer", txtAnswer.Text);

            var BinaryConverter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var save = File.Create("BinaryTree"))
            {
                //streamwriter.Write(HashTable);
                BinaryConverter.Serialize(save, HashTable);
                MessageBox.Show("Your file has been saved");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnSend for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstNumber.Text == "" || txtSecondNumber.Text == "" || 
                    txtAnswer.Text == "" || cboxOperator.Text == "")
                {
                    MessageBox.Show("Please input values to send");
                }
                else
                {
                    ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    byte[] message = new byte[1500];
                    message = enc.GetBytes(txtFirstNumber.Text + " " + cboxOperator.Text + " " + txtSecondNumber.Text);// Get the string from the textbox and convert it to raw bytes
                    sck.Send(message);
                    btnSend.Enabled = false;

                    addDataGrid(txtFirstNumber.Text, cboxOperator.Text, txtSecondNumber.Text, txtAnswer.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            if (txtFirstNumber.Text == "" || txtSecondNumber.Text == "" || 
                txtAnswer.Text == "" || cboxOperator.Text == "")
            {
                MessageBox.Show("Please input values to send");
            }
            else
            { 
            btnSend.Enabled = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a data grid. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="firstName">    The person's first name. </param>
        /// <param name="Operator">     The operator. </param>
        /// <param name="secondNumber"> The second number. </param>
        /// <param name="answer">       The answer. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void addDataGrid(string firstName, string Operator, string secondNumber, string answer)
        {
            String[] row = { firstName, Operator, secondNumber, answer };

            dataGrid.Rows.Add(row);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnDisplayPreorder for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnDisplayPreorder_Click(object sender, EventArgs e)
        {
            txtLinkedList.Clear();
            txtLinkedList.Text = "PRE-ORDER: ";
            txtLinkedList.Text += binaryTree.printPreOrder(tree: binaryTree);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnDisplayInorder for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnDisplayInorder_Click(object sender, EventArgs e)
        {
            txtLinkedList.Clear();
            txtLinkedList.Text = "IN-ORDER: ";
            txtLinkedList.Text += binaryTree.printInOrder(tree: binaryTree);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnDisplayPostOrder for click events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnDisplayPostOrder_Click(object sender, EventArgs e)
        {
            txtLinkedList.Clear();
            txtLinkedList.Text = "POST-ORDER: ";
            txtLinkedList.Text += binaryTree.printPostOrder(tree: binaryTree);
        }

        private void btnSavePreorder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The binary tree has been saved");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by txtSecondNumber for text changed events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by cboxOperator for text changed events. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void cboxOperator_TextChanged(object sender, EventArgs e)
        {
            txtAnswer.Text = "";
            txtSecondNumber.Text = "";
        }
    }
}
