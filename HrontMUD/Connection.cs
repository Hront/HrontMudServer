using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace HrontMUD
{
    internal class Connection
    {
        private readonly TcpClient client;
        private readonly StreamWriter writer;
        private readonly StreamReader reader;
        public Player Player { get; set; }

        internal Connection(TcpClient tcpClient)
        {
            client = tcpClient;
            var stream = client.GetStream();
            reader = new(stream);
            writer = new(stream);
            Player = new Player();
        }
        internal async void Login()
        {
            //some code
            GetCommand();
        }
        internal async void GetCommand() //Асинхронный метод для принятия команд от клиента.
        {
            try
            {
                for (; ; )
                {
                    await writer.WriteLineAsync("Enter your name or (create) for new.");
                    SendMessage("Enter your name or (create) for new.");
                    string? name = await reader.ReadLineAsync();
                    if (name != null && name != "create")
                    {
                        var result = Globals.PlayerList.Find(x => x.userName.Contains(name));
                        if (result != null)
                        {
                            Player = result;
                            Globals.ClientList.Add(this); //При создании экземпляра класса Clients он добавляется в лист класса Globals.
                            break;
                        }
                        else
                        {
                            SendMessage("Name not found.");
                            continue;
                        }
                    }
                    else if (name == "create")
                    {
                        SendMessage("Enter new name.");
                        var newname = await reader.ReadLineAsync();
                        //Создание нового перса, и возврат значения Player.
                        break;
                    }
                }

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
        public void AnswerToSender()
        {
        }
        public void SendMsgToPrivate()
        {
            var found = Globals.ClientList.Find(item => item.Player.userName == "KAKAHA");
            if (found != null)
            {
                found.SendMessage("Text");
            }
        }
        public void SendMsgToGroup()
        {
        }
        public void SendMsgToRoom()
        {
        }
        public void SendMsgToAll()
        {
            foreach (var item in Globals.ClientList)
            {
                item.SendMessage("");
            }
        }
    }
}
