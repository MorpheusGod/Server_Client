using System;


// Клиент
namespace Client
{
    class Program
    {

       

        static void Main(string[] args)
        {
            int port = 904;
            string ip = "127.0.0.1";
            string SendData = string.Empty;

            ConnectManager cm = new ConnectManager(ip, port);

            Console.WriteLine("******Remote Client******");
            Console.WriteLine($"[{DateTime.Now.ToString()}] Старт программы. Пробую подключиться к серверу..");

            try
            {
                cm.Connect();
                Console.WriteLine($"[{DateTime.Now.ToString()}] Подключен...");
               
            }
            catch
            {
                Console.WriteLine($"[{DateTime.Now.ToString()}] Сервер сдох и не отвечает:)");
                Console.WriteLine("Нажмите Enter для выхода.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            do
            {
                Console.Write("Сообщение для сервера :");
                SendData = Console.ReadLine();
                cm.SendMessage(SendData);

            } while (SendData != string.Empty);

        }  
}
}

//byte[] pbd = new byte[17];
//master.Receive(pbd);
//string fromServer = Encoding.ASCII.GetString(pbd);
//Console.WriteLine("Server answer: " + fromServer);
