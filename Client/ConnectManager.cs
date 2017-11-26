using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class ConnectManager
    {
        private Socket client;
        private IPEndPoint ipEnd;

        public ConnectManager(string ip , int port)
        {
            client = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipEnd = new IPEndPoint (IPAddress.Parse(ip), port);
        }

        public void Connect ()
        {
            client.Connect(ipEnd);
        }

        public void SendMessage (string message)
        {
            if (message != string.Empty)
            {
                client.Send(Encoding.ASCII.GetBytes(message));

            } else
            {
                client.Close();
            }
        }



    }
}
