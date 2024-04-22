using System;
using System.Collections.Generic;

class Program
{
    static int NODsKoef(int c, int m, out int x, out int y)
    {
        if (m == 0)
        {
            x = 1;
            y = 0;
            return c;
        }

        int x1, y1;
        int nod = NODsKoef(m, c % m, out x1, out y1);

        x = y1;
        y = x1 - (c / m) * y1;

        return nod;
    }

    static int ObratnCh(int c, int m)
    {
        int x, y;
        int gcd = NODsKoef(c, m, out x, out y);
        if (gcd != 1)
        {
            return -1;
        }
        else
        {
            return (x % m + m) % m;
        }
    }

    static void Main(string[] args)
    {
        int c, d, m;
        Console.WriteLine("Введите c:");
        c = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите m:");
        m = int.Parse(Console.ReadLine());

        if (ObratnCh(c, m) != -1)
        {
            d = ObratnCh(c, m);
            Console.WriteLine("Обратное число " + c + "^(-1) mod " + m + " = " + d);

            Console.WriteLine("Остаток (r)\tX\t\tY\tЧастное (q)");
            List<Tuple<int, int, int, int>> table = new List<Tuple<int, int, int, int>>();
            int a = c, b = m;
            while (b != 0)
            {
                int q = a / b;
                int r = a % b;
                int x, y;
                NODsKoef(a, b, out x, out y);
                table.Add(new Tuple<int, int, int, int>(r, x, y, q));
                a = b;
                b = r;
            }
            foreach (var n in table)
            {
                Console.WriteLine(n.Item1 + "\t\t" + n.Item2 + "\t\t" + n.Item3 + "\t\t" + n.Item4);
            }
        }
        else
        {
            Console.WriteLine("Для данных значений обратного числа d по модулю не существует.");
        }
    }
}
