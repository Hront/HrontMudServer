using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HrontMUD
{
    internal class GameLoop
    {
        public const int PulseDuration = 10; // Миллисекунд.
        public uint PulseCount = 0;
        public uint RoundCount = 0;

        public GameLoop()
        {
            //List<Event> PulseEvents = new(); // Создаем список событий вызываемых по пульсу (PulseDuration).
            //List<Event> RoundEvents = new();//По раунду.
            //List<Event> TickEvents = new(); // По тику.

            System.Timers.Timer worldPulse = new(PulseDuration); // Создаем таймер PulseDuration миллисекунд.
            worldPulse.Elapsed += new ElapsedEventHandler(PulseEventMethod);// Когда прошло время, срабатывает ElapsedEventHandler вызывая обработчик PulseEventMethod.
            worldPulse.Start();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++ Обработчик события (в данном случае прошло пульс = 10мс.) ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void PulseEventMethod(object? sender, ElapsedEventArgs e)
        {
            TimeUpdate();
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //+++++++++++++++++++++++++++++++++++++++++++++ Своего рода хронометр. Считает пульсы раунды и тики и вызывает соответствующие методы ++++++++++++++++++++++++++++++++++++++++++++++++++
        void TimeUpdate()
        {
            PulseTimeUpdate();
            PulseCount++;

            if (PulseCount == 200)
            {
                RoundTimeUpdate();
                RoundCount++;
                PulseCount = 0;
            }

            if (RoundCount == 50)
            {
                TickTimeUpdate();
                RoundCount = 0;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++ Методы срабатывающие по таймеру (пульс, раунд, тик) ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        void PulseTimeUpdate()
        {

        }
        void RoundTimeUpdate()
        {
            foreach (Connection conn in Globals.clientList) 
            {
                conn.SendMessage("===раунд===");
            }
            Console.WriteLine("Прошло 1 раунд.");
        }
        void TickTimeUpdate()
        {

        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Методы отправки сообщений клиентам. ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Ответ клиенту на команду.
        public void AnswerToSender()
        {
        }
        //Сообщение в приват.
        public void SendMsgToPrivate()
        {
            var found = Globals.clientList.Find(item => item.player.userName == "KAKAHA");
            if (found != null)
            {
                found.SendMessage("Text");
            }
        }
        //Сообщение в группу.
        public void SendMsgToGroup()
        {
        }
        //Сообщение в комнату.
        public void SendMsgToRoom()
        {
        }
        //Сообщение всем.
        public void SendMsgToAll()
        {
            foreach (var item in Globals.clientList)
            {
                item.SendMessage("");
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }
}
