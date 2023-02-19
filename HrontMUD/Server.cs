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
        private readonly TcpListener server = new(IPAddress.Loopback, 4444); //Создаем слушающий сокет.
        public Server()
        {
            Globals globals = new(); // Cоздаем экземпляр класса Globals.
            Commands commands = new(); //Методы вызываемые командами.
            GameLoop gameLoop = new();
        }
        internal async void Acceptor() //Асинхронный метод принимаеющий новые подключения и команды от клиентов.
        {
            server.Start(); //Старт прослушивания.
            while (true)
            {
                Console.WriteLine("Ждем новые подключения");
                Connection client = new(await server.AcceptTcpClientAsync());//Принимаем подключения и создаем экземпляр класса Clients под каждое подключение.
                client.GetCommand();//Вызываем асинхронный метод класса Clients для получения команд от клиента.
            }
        }
    }
}
