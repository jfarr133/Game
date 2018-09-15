using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
//using consoleNetworkTest1;
using System.Windows.Forms;

namespace Multi_Con_C
{

    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            //Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // s.Connect("127.0.0.1", 8);
            // s.Close();
            // s.Dispose();
        }
    }


}
