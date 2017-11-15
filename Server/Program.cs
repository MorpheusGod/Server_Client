using System;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

// Сервер
namespace Server
{
    class Program
    {

       
        
        static void Main(string[] args)
        {
            int port = 904;

            // Инциализация сервера.
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, port);
            listenerSocket.Bind(ipEnd);
            Console.WriteLine($"[{DateTime.Now.ToString()}] Сервер запущен...");
            Console.WriteLine($"[{DateTime.Now.ToString()}] Ожидаю входящие подключения..");
            listenerSocket.Listen(0);

            while (true)
            {

                Socket clientSocket = listenerSocket.Accept();
                Thread clientThread = new Thread(() => ClientConnection(clientSocket));
                Console.WriteLine($"[{DateTime.Now.ToString()}] [{((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString()}] подключился");
                clientThread.Start();
            }
            
        }

        static void ClientConnection(Socket clientSocket)
        {
            
            // Прием данных.
            byte[] buffer = new byte[clientSocket.SendBufferSize];
            int readByte;
            Dictionary<string, ProcessStartInfo> commands = CreateCommands();
            do
            {
                // Вывод полученных данных от клиентов.
                readByte = clientSocket.Receive(buffer);
                byte[] rData = new byte[readByte];
                Array.Copy(buffer, rData, readByte);
                string data = Encoding.ASCII.GetString(rData);

                Console.WriteLine($"[{DateTime.Now.ToString()}] [{((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString()}] написал: " +data);
                if (commands.ContainsKey(data))
                    Process.Start(commands[data]);

            } while (readByte > 0);

            Console.WriteLine($"[{DateTime.Now.ToString()}] Клиент отключился");
            Console.ReadKey();
        }

        static Dictionary<string, ProcessStartInfo> CreateCommands()
        {
            Dictionary<string, ProcessStartInfo> commands = new Dictionary<string, ProcessStartInfo>();
            commands["music"] = new ProcessStartInfo(@"E:\YandexDisk\Work\C#\SpeechRecognition\bin\Debug\player\AIMP.exe", "/PLAY");
            commands["video"] = new ProcessStartInfo("notepad.exe");
            return commands;
        }
    }
}



//string info = "AIMP started...";
//clientSocket.Send(Encoding.ASCII.GetBytes(info));


//if (d == "music")
//{
//    Process.Start(playerpath32, "/PLAY");
//    Process.Start(Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess
//    ? Environment.ExpandEnvironmentVariables(playerpath64) : playerpath32);
//    string info = "AIMP started...";
//    clientSocket.Send(Encoding.ASCII.GetBytes(info));
//}

// Ответ от сервера
//  clientSocket.Send(new byte[4] { 65, 45, 32, 01 });
