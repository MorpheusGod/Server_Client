using System;

// Сервер

namespace Server
{
    class Program
    { 
        static void Main(string[] args)
        {
            int port = 904;

            Server srv = new Server(port);

            try
            {

                srv.StartServer(500);
                Console.WriteLine($"[{DateTime.Now.ToString()}] Сервер запущен.....");

            }catch
            {
                Console.WriteLine($"[{DateTime.Now.ToString()} Не удается запустить сервер!!!!!!]");
            }

           while (true)
            {
                srv.ClientAccept();
            } 
        }
    }
}

