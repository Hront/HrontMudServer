using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace HrontMUD
{
    internal class Connection
    {
        public TcpClient client;
        public StreamWriter Writer;
        public StreamReader Reader;
        public Player player;

        internal Connection(TcpClient tcpClient)
        {
            client = tcpClient;
            var stream = client.GetStream();
            Reader = new(stream);
            Writer = new(stream);
            Autent autent = new(this);
            player = autent.NewPlayer();
        }
        public async Task ClientLoop()
        {
            try
            {
                Globals.clientList.Add(this);
                while (true)
                {
                    var text = await Reader.ReadLineAsync();
                    Console.WriteLine($"{player.userName}{player.rase}:{text}");
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
    }
}
