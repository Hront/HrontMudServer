using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HrontMUD
{
    internal class Interpreter
    {
        private Connection sender;
        private string command;
        private string target;
        private string message;
        public Interpreter(Connection client, string text)
        {
            char[] delimiterChars = { ' ', '.' };
            string[] words = text.Split(delimiterChars);
            sender = client;
            command = words[0];
            target = words[1];
            message = words[2];
        }
    }
}
