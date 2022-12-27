using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR8_SpeedTest
{
    class Timer
    {
        public int Min { get; set; }
        public int Sec { get; set; }
        public Thread TimerThread { get; set; }
        public TypingMenu Typing { get; set; }
        public Timer(TypingMenu typing, int min, int sec)
        {
            Min = min;
            Sec = sec;
            Typing = typing;
            TimerThread = new Thread(new ThreadStart(Render));
        }
        public void Render()
        {
            while (Typing.Active)
            {
                if (Typing.Element != null)
                {
                    Console.SetCursorPosition(0, 12);
                    Console.WriteLine(string.Format("{{0, -{0}}}", Console.BufferWidth), $"{Min}:{Sec}");
                    Console.SetCursorPosition(Typing.Left, Typing.Top);
                    if (Sec == 0)
                    {
                        if (Min == 0)
                        {
                            Typing.Active = false;
                            return;
                        }
                        Min -= 1;
                        Sec = 60;
                    }
                    else Sec -= 1;
                    Thread.Sleep(1000);
                }
            }
        }

        public void TimerStop()
        {
        }
    }
}