namespace PR8_SpeedTest
{
    class NameTable
    {
        static void Main(string[] args) => Load();

        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Напишите название для таблицы рекордов: ");
            string text = Console.ReadLine();

            new TypingMenu(0, 20, new TypingData(text));
        }
    }
}
