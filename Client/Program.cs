using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

// Клиент
namespace Client
{
    class Program
    {

       

        static void Main(string[] args)
        {
            DateTime ThToday = DateTime.Now;
            string ThData = ThToday.ToString();
            Socket master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 904;
            string ip = "192.168.1.226";
            string sendData = "";
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
            Console.WriteLine("******Remote Client******");
            Console.WriteLine($"[{DateTime.Now.ToString()}] Старт программы. Пробую подключиться к серверу..");
            try
            {
                master.Connect(ipEnd);
                Console.WriteLine($"[{DateTime.Now.ToString()}] Подключен...");
                do
                {
                    Console.Write("Data to send:");
                    sendData = Console.ReadLine();
                    master.Send(Encoding.ASCII.GetBytes(sendData));
             
                } while (sendData.Length > 0);
                master.Close();
            }catch(Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToString()}] Сервер сдох и не отвечает:)");
                Console.WriteLine("Нажмите Enter для выхода.");
                Console.ReadKey();
            }
        }  
}
}

//byte[] pbd = new byte[17];
//master.Receive(pbd);
//string fromServer = Encoding.ASCII.GetString(pbd);
//Console.WriteLine("Server answer: " + fromServer);
