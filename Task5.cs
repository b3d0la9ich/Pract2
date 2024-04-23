using System;

class Program
{
    static long PowMod(long a, int x, int p)
    {
        long result = 1;
        a = a % p;
        while (x > 0)
        {
            if (x % 2 == 1)
            {
                result = (result * a) % p;
            }
            a = (a * a) % p;
            x /= 2;
        }
        return result;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Введите этажи нужного числа:");
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());

        int VrCh = (int)PowMod(a, b, 10);
        int result = (int)PowMod(VrCh, c, 10);
        Console.WriteLine("Последняя цифра: " + result);
    }
}
