using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
using System.Net.Sockets;
using System.Text;
//using System.Threading.Tasks;
//using consoleNetworkTest1;
using System.Windows.Forms;

namespace consoleNetworkTest1
{
    
    class Program
    {

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
    /*
    class Program
    {
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect("127.0.0.1", 8);
            s.Close();
            s.Dispose();
        }
    }*/ /*
    class Program
    {
        static Listener l;
        static void Main(string[] args)
        {
            l = new Listener(8);
            l.SocketAccepted += new Listener.SocketAcceptHandler(l_SocketAccepted);
            l.Start();

            Console.Read();
        }

        static void l_SocketAccepted(Socket e)
        {
            Console.WriteLine("New Connection: {0}\n{1}\n===========", e.RemoteEndPoint, DateTime.Now);
        }
    }*/
}
