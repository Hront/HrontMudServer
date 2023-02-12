using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HrontMUD
{
    internal class Clients
    {
        private readonly TcpClient client;
        private readonly StreamWriter writer;
        private readonly StreamReader reader;
        internal Clients(TcpClient tcpClient)
        {
            Globals.ClientList.Add(this); //При создании экземпляра класса Clients он добавляется в лист класса Globals.
            client = tcpClient;
            var stream = client.GetStream();
            reader = new (stream);
            writer = new (stream);
        }
        internal async void GetCommand() //Асинхронный метод для принятия команд от клиента.
        {
            try
            {
                while (true)
                {
                    var command = await reader.ReadLineAsync();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Соединение разорвано!");
                Globals.ClientList.Remove(this);//Если соединение разорвано удаляем этот эеземпляр класса из листа в Globals.
                client.Close();
            }
        }
        internal void SendMessage(string text) //Метод для отсылки сообщений клиенту. (Пока не придумал куда его деть:))
        {
            writer.WriteLine(text);
            writer.Flush();
        }
    }
}
