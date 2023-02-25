using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HrontMUD
{
    internal class Autent
    {
        Connection connection;
        Player newplayer = new();
        internal Autent(Connection conn)
        {
            this.connection = conn;
            GetNewPlayer();
        }
        
        public async void GetNewPlayer() 
        {
            try
            {
                connection.SendMessage("Enter your Name");
                var name = await connection.Reader.ReadLineAsync();
                if (name != null)
                {
                    newplayer.userName = name;
                }
                Console.WriteLine(name);
                connection.SendMessage("Enter your Rase");
                var rase = await connection.Reader.ReadLineAsync();
                if (rase != null)
                {
                    newplayer.rase = rase;
                }
                Console.WriteLine(rase);
                await Task.Run(connection.ClientLoop);
            }
            catch
            {
                Console.WriteLine($"Disconected:{connection.client.Client.RemoteEndPoint}");
                connection.Close();
            }
        }
        public Player NewPlayer() 
        {
            return newplayer;
        }
    }
}
