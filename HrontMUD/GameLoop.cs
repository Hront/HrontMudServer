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
        private void PulseEventMethod(object? sender, ElapsedEventArgs e)
        {
            TimeUpdate();
        }
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
        void PulseTimeUpdate()
        {
//            Console.WriteLine("Прошло 10 миллисекунд.");
        }
        void RoundTimeUpdate()
        {
            Console.WriteLine("Прошло 1 раунд.");
        }
        void TickTimeUpdate()
        {
            Console.WriteLine("Прошло 1 тик.");
        }
    }
}
