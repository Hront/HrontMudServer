using HrontMUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrontMUD
{
    internal class Commands
    {
        internal static void CmdWho(Connection client)
        {
            foreach(Connection clients in Globals.clientList)
            {
                client.SendMessage(clients.player.userName);
            }
        }
    }
}
