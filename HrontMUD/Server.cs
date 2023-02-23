using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace HrontMUD
{
    internal class Server
    {
        private readonly TcpListener listener = new(IPAddress.Loopback, 4444); //Создаем слушающий сокет.
        public Server()
        {
            Globals globals = new(); // Cоздаем экземпляр класса Globals.
            Commands commands = new(); //Методы вызываемые командами.
            GameLoop gameLoop = new();
        }
        internal async Task ListenAsync()
        {
            try
            {
                listener.Start();
                while (true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    Connection client = new(tcpClient);
                    Console.WriteLine($"Client connected: {tcpClient.Client.RemoteEndPoint}");
                    await Task.Run(client.ClientLoop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ServerStop();
            }
        }
        private void ServerStop()
        {
            foreach (var client in Globals.clientList)
            {
                client.Close();
            }
            listener.Stop();
        }
    }
}
