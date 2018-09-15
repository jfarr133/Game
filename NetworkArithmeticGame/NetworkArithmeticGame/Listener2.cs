////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Listener2.cs
//
// summary:	Implements the listener 2 class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to listen and wait for a signal from the student. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace NetworkArithmeticGame
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A listener 2. </summary>
    ///
    /// <remarks>   Jakob, 15/09/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class Listener2
    {
        /// <summary>   The second s. </summary>
        Socket s2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the listening 2. </summary>
        ///
        /// <value> True if listening 2, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool Listening2
        {
            get;
            private set;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the port 2. </summary>
        ///
        /// <value> The port 2. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Port2
        {
            get;
            private set;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="port2">    The second port. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Listener2(int port2)
        {
            Port2 = port2;
            s2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Starts this object. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Start()
        {
            if (Listening2)
                return;

            s2.Bind(new IPEndPoint(0, Port2));
            s2.Listen(0);

            s2.BeginAccept(callback, null);
            Listening2 = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stops this object. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Stop()
        {
            if (!Listening2)
                return;

            s2.Close();
            s2.Dispose();
            s2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            s2.BeginAccept(callback, null);
            Listening2 = true;
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
                Socket s2 = this.s2.EndAccept(arr);

                if (SocketAccepted != null)
                {
                    SocketAccepted(s2);
                }
                this.s2.BeginAccept(callback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Socket accept handler 2. </summary>
        ///
        /// <remarks>   Jakob, 15/09/2018. </remarks>
        ///
        /// <param name="f">    A Socket to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public delegate void SocketAcceptHandler2(Socket f);
        /// <summary>   Event queue for all listeners interested in SocketAccepted events. </summary>
        public event SocketAcceptHandler2 SocketAccepted;

    }
}
