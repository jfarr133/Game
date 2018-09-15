using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

/*
 * Author: Jakob Farrow 2104990817 or 455089250
 * Purpose: The functionality for this page is to listen and wait for a signal from the teacher. 
 * Version Control: 1.0
 * Date: 15/09/18
 */

namespace StudentForm
{
    class Listener
    {
        Socket s;

        public bool Listening
        {
            get;
            private set;
        }

        public int Port
        {
            get;
            private set;
        }

        public Listener(int port)
        {
            Port = port;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (Listening)
                return;

            s.Bind(new IPEndPoint(0, Port));
            s.Listen(0);

            s.BeginAccept(callback, null);
            Listening = true;
        }
        public void Stop()
        {
            if (!Listening)
                return;

            s.Close();
            s.Dispose();
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            s.BeginAccept(callback, null);
            Listening = true;
        }
        void callback(IAsyncResult ar)
        {
            try
            {
                Socket s = this.s.EndAccept(ar);

                if (SocketAccepted != null)
                {
                    SocketAccepted(s);
                }
                this.s.BeginAccept(callback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public delegate void SocketAcceptHandler(Socket e);
        public event SocketAcceptHandler SocketAccepted;

    }
}
