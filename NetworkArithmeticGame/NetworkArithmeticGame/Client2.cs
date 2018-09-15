using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkArithmeticGame
{
    class Client2
    {
        public string ID2
        {
            get;
            private set;
        }
        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        Socket sck2;
        public Client2(Socket accepted)
        {
            sck2 = accepted;
            ID2 = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)sck2.RemoteEndPoint;
            sck2.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }

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

        public void Close()
        {
            sck2.Close();
            sck2.Dispose();
        }

        public delegate void ClientReceivedHandler2(Client2 sender2, byte[] data2);
        public delegate void ClientDisconnectHandler2(Client2 sender2);

        public event ClientReceivedHandler2 Received;
        public event ClientDisconnectHandler2 Disconnected;
    }
}
