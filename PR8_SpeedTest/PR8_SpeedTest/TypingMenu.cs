using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR8_SpeedTest
{
    class TypingMenu
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public TypingData Element { get; set; }
        public Timer Timer { get; set; } = null;
        public bool Active { get; set; } = false;
        public TypingMenu(int min, int sec, TypingData element)
        {
            Element = element;
            Timer = new Timer(this, min, sec);
            Start();
        }

        public string Text = "И вдруг она услышала какой-то нежный звон. Наденька посмотрела в окошко, откуда шел этот звон, и увидела, что к ней заглядывает целая связка воздушных шариков. Ей показалось, что они хотят вырваться от чего-то, потому что, шарики то напрягались на своих ниточках, то бессильно падали как завядшие головки тюльпанов.";

        public void Start()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Text);
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Нажмите: Enter когда будете готовы");
            Console.SetCursorPosition(0, 0);

            Active = true;
            Stopwatch sw = new Stopwatch();
            while (Active)
            {
                Console.SetCursorPosition(Left, Top);
                var tt = Console.ReadKey(true);
                if (tt.Key == ConsoleKey.Enter)
                {
                    if (Element.Min == 0 && !Timer.TimerThread.IsAlive)
                    {
                        Timer.TimerThread.Start();
                        sw.Start();
                    }
                    else Active = false;
                }
                else if (tt.KeyChar == Text[Element.Min] && Timer.TimerThread.IsAlive)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Text[Element.Min]);
                    Console.ForegroundColor = ConsoleColor.White;

                    Left += 1;
                    if (Left == Console.BufferWidth)
                    {
                        Top += 1;
                        Left = 0;
                    }

                    Element.Min += 1;
                    if (sw.ElapsedMilliseconds <= 1000)
                    {
                        Element.SecNext++;
                        if (Element.SecNext > Element.Sec) Element.Sec = Element.SecNext;
                    }
                    else
                    {
                        Element.SecNext = 0;
                        sw.Reset();
                        sw.Start();
                    }

                    if (Element.Min == Text.Length) Active = false;
                }
            }
            Element.Save(this);
        }

        public void Load(List<TypingData> list)
        {
            Console.Clear();
            Console.WriteLine($"Таблица рекордов: \n");
            foreach (TypingData item in list)
            {
                Console.WriteLine($"{item.Name} Слов в минуту:{item.Min} Слов в секунду:{item.Sec}");
            }
            Console.WriteLine($"Нажмите Enter если хотите попробовать ещё раз");

            while (true)
            {
                var tt = Console.ReadKey(true);
                if (tt.Key == ConsoleKey.Enter) break;
            }

            NameTable.Load();
        }
    }
}