using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
        baslari:
            string cumle, sayi1, sayi2, islem;
            int s1, s2;
            double sonuc = 0;
            Console.WriteLine("işlem giriniz");
            cumle = Console.ReadLine();
            string[] dizi = cumle.Split(' ');
            var isNumeric1 = int.TryParse(dizi[0], out int first1);
            var isNumeric2 = int.TryParse(dizi[2], out int first2);

            if (isNumeric1 == false)
            {
                Console.WriteLine("sayı girmediniz");
                goto baslari;
            }
            else if (isNumeric2 == false)
            {
                Console.WriteLine("sayı girmediniz");
                goto baslari;
            }



            switch (dizi[1])
            {
                case "+": sonuc = first1 + first2; break;
                case "-": sonuc = first1 - first2; break;
                case "*": sonuc = first1 * first2; break;
                case "/": sonuc = (double)first1 / (double)first2; break;



                default:
                    break;
            }

            Console.WriteLine(first1 + dizi[1] + first2 + "=" + sonuc.ToString());
            Console.WriteLine("başka bir işlem var mı E/H");
            string d = Console.ReadLine();
            if (d == "E" || d == "e")
                goto baslari;












        }
    }
}
