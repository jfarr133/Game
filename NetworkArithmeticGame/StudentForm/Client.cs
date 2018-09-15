////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Client.cs
//
// summary:	Implements the client class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to get/recieve the data sent from the teacher. 
 * Version Control: 1.0
 * Date: 15/09/18
 */


namespace StudentForm
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A client. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class Client
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ID
        {
            get;
            private set;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the end point. </summary>
        ///
        /// <value> The end point. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        /// <summary>   The sck. </summary>
        Socket sck;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="accepted"> The accepted. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Client(Socket accepted)
        {
            sck = accepted;
            ID = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)sck.RemoteEndPoint;
            sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Async callback, called on completion of callback. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="ar">   The result of the asynchronous operation. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void callback(IAsyncResult ar)
        {
            try
            {
                sck.EndReceive(ar);

                byte[] buf = new byte[8192];

                int rec = sck.Receive(buf, buf.Length, 0);

                if (rec < buf.Length)
                {
                    Array.Resize<byte>(ref buf, rec);

                }
                if (Received != null)
                {
                    Received(this, buf);
                }
                sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Close();

                if (Disconnected != null)
                {
                    Disconnected(this);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Closes this object. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Close()
        {
            sck.Close();
            sck.Dispose();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Handler, called when the client received. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   The sender. </param>
        /// <param name="data">     The data. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public delegate void ClientReceivedHandler(Client sender, byte[] data);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Handler, called when the client disconnect. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender">   The sender. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public delegate void ClientDisconnectHandler(Client sender);

        /// <summary>   Event queue for all listeners interested in Received events. </summary>
        public event ClientReceivedHandler Received;
        /// <summary>   Event queue for all listeners interested in Disconnected events. </summary>
        public event ClientDisconnectHandler Disconnected;
    }
}
