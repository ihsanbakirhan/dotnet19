using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            powerball pw = new powerball();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <=6 ; j++)
                {
                    Console.Write(pw.dene() + "  ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            
        }
    }
    class powerball
    {
        Random r = new Random();
        
        public int dene()
        {
            return r.Next(1, 56);
        }
    }
}
