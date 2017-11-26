using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Server
    {
        private Socket listenSocket;
        private IPEndPoint ip;
       

        public Server(int port)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip = new IPEndPoint(IPAddress.Any, port);
        }

        public void StartServer (int backlog)
        {
            listenSocket.Bind(ip);
            listenSocket.Listen(backlog);
        }


        public void ClientAccept ()
        {
            Socket clientSocket = listenSocket.Accept();
            Console.WriteLine($"[{DateTime.Now.ToString()}] [{((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString()}] подключился");
            Thread cl = new Thread(() => MessageDecode(clientSocket));
            cl.Start();
        }


        private void MessageDecode (Socket clientSocket)
        {
            byte[] buffer = new byte[clientSocket.SendBufferSize];
            int readByte;

            do
            {
                readByte = clientSocket.Receive(buffer);
                byte[] rData = new byte[readByte];
                Array.Copy(buffer, rData, readByte);
                string data = Encoding.ASCII.GetString(rData);

                Console.WriteLine($"[{DateTime.Now.ToString()}] [{((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString()}] написал: " + data);

                //if (commands.ContainsKey(data))
                //    Process.Start(commands[data]);


            } while (readByte > 0);

            Console.WriteLine($"[{DateTime.Now.ToString()}] Клиент отключился");
            Console.ReadKey();
        }

        //static Dictionary<string, ProcessStartInfo> CreateCommands()
        //{
        //    Dictionary<string, ProcessStartInfo> commands = new Dictionary<string, ProcessStartInfo>();
        //    commands["music"] = new ProcessStartInfo(@"E:\YandexDisk\Work\C#\SpeechRecognition\bin\Debug\player\AIMP.exe", "/PLAY");
        //    commands["video"] = new ProcessStartInfo("notepad.exe");

        //    return commands;
        //}

    }
}
