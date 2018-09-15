////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Client2.cs
//
// summary:	Implements the client 2 class
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
 * Purpose: The functionality for this page is to get/recieve the data sent from the student. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A client 2. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class Client2
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier 2. </summary>
        ///
        /// <value> The identifier 2. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ID2
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

        /// <summary>   The second sck. </summary>
        Socket sck2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="accepted"> The accepted. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Client2(Socket accepted)
        {
            sck2 = accepted;
            ID2 = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)sck2.RemoteEndPoint;
            sck2.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Async callback, called on completion of callback. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="arr">  The result of the asynchronous operation. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void callback(IAsyncResult arr)
        {
            try
            {
                sck2.EndReceive(arr);

                byte[] buft = new byte[9192];

                int rec2 = sck2.Receive(buft, buft.Length, 0);

                if (rec2 < buft.Length)
                {
                    Array.Resize<byte>(ref buft, rec2);

                }
                if (Received != null)
                {
                    Received(this, buft);
                }
                sck2.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
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
            sck2.Close();
            sck2.Dispose();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Client received handler 2. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender2">  The second sender. </param>
        /// <param name="data2">    The second data. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public delegate void ClientReceivedHandler2(Client2 sender2, byte[] data2);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Client disconnect handler 2. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="sender2">  The second sender. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public delegate void ClientDisconnectHandler2(Client2 sender2);

        /// <summary>   Event queue for all listeners interested in Received events. </summary>
        public event ClientReceivedHandler2 Received;
        /// <summary>   Event queue for all listeners interested in Disconnected events. </summary>
        public event ClientDisconnectHandler2 Disconnected;
    }
}
