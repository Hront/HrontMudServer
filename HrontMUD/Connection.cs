using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace HrontMUD
{
    internal class Connection
    {
        private readonly TcpClient client;
        private readonly StreamWriter Writer;
        private readonly StreamReader Reader;
        public Player? player;

        internal Connection(TcpClient tcpClient)
        {
            client = tcpClient;
            var stream = client.GetStream();
            Reader = new(stream);
            Writer = new(stream);
        }
        public async void ClientLoop()
        {
            try
            {
                for (; ; )
                {
                    SendMessage("Введите имя персонажа.");
                    var name = await Reader.ReadLineAsync();
                    if (name != null && name != "create")
                    {
                        Player? result = Globals.playerList.Find(x => x.userName == name);
                        if (result != null)
                        {
                            player = result;
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
                        string? newname = await Reader.ReadLineAsync();
                        if (newname != null)
                        {
                            Player newplayer = new(newname);
                            player = newplayer;
                            Globals.playerList.Add(newplayer);
                            break;
                        }
                    }
                }
                Globals.clientList.Add(this); //добавляется в лист класса Globals.
                while (true)
                {
                    var text = await Reader.ReadLineAsync();
                    Console.WriteLine($"{player.userName}:{text}");
                }
            }
            catch
            {
                Console.WriteLine($"Disconected: {player?.userName}:{client.Client.RemoteEndPoint}");
                Globals.clientList.Remove(this);
                Close();
            }
            finally
            {
                Close();
            }
        }
        public void Close()
        {
            Writer.Close();
            Reader.Close();
            client.Close();
        }
        internal async void SendMessage(string text)
        {
            await Writer.WriteLineAsync(text);
            await Writer.FlushAsync();
        }
        public void AnswerToSender()
        {
        }
        public void SendMsgToPrivate()
        {
            var found = Globals.clientList.Find(item => item.player.userName == "KAKAHA");
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
            foreach (var item in Globals.clientList)
            {
                item.SendMessage("");
            }
        }
    }
}
