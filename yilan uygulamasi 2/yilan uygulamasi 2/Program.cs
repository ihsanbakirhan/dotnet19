using System;

namespace yilan_uygulamasi_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int xPosition = 35;
            int yPosition = 20;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine((char)2);
            buildwall();
        }
        private static void buildwall()
        {
            for (int i = 1; i < 41; i++)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("|");
                Console.SetCursorPosition(70, i);
                Console.Write("|");
            }
            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
                Console.SetCursorPosition(i, 40);
                Console.Write("-");
            }
            Console.Read();
        }
    }
}

