using System;
using System.Threading;
namespace kayan_yazı
{
    class Program
    {
        static void Main(string[] args)
        {
            string yazi = Console.ReadLine();
            while (true)
            {
                Console.Clear();
                string bosluklar = "";
                for (int i = 0; i < Console.WindowWidth-yazi.Length; i++)
                {
                    bosluklar += " ";
                
                }
                yazi = yazi.Substring(1) + bosluklar + yazi[0];
                Console.Write(yazi);
                Thread.Sleep(100);

            }
        }
    }
}
