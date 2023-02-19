using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace HrontMUD
{
    internal class Login
    {
        public Login()
        {

        }
        public Player Log(Connection tcpClient, string name)
        {
            Connection client = tcpClient;
            client.SendMessage("text");
            
            var result = Globals.PlayerList.Find(x => x.userName.Contains(name));
            if (result != null)
            {
                return result;
            }
            else
            {
                Player player = new();
                return player;
            }
        }
    }
}
