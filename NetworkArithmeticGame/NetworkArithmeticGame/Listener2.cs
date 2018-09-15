using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkArithmeticGame
{
    class Listener2
    {
        Socket s2;

        public bool Listening2
        {
            get;
            private set;
        }

        public int Port2
        {
            get;
            private set;
        }

        public Listener2(int port2)
        {
            Port2 = port2;
            s2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (Listening2)
                return;

            s2.Bind(new IPEndPoint(0, Port2));
            s2.Listen(0);

            s2.BeginAccept(callback, null);
            Listening2 = true;
        }
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

        public delegate void SocketAcceptHandler2(Socket f);
        public event SocketAcceptHandler2 SocketAccepted;

    }
}
