using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrontMUD
{
    internal class Commands
    {
        internal static void CmdWho(Clients client)
        {
            foreach(Clients clients in Globals.ClientList)
            {
                client.SendMessage(clients.Name);
            }
        }
    }
}
