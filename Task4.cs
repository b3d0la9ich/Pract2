using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static bool IsPrime(int p)
    {
        if (p <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(p); i++)
        {
            if (p % i == 0) return false;
        }
        return true;
    }

    static int Random(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max + 1);
    }

    static int Eiler(int p)
    {
        int result = p;
        for (int i = 2; i * i <= p; i++)
        {
            if (p % i == 0)
            {
                while (p % i == 0)
                    p /= i;
                result -= result / i;
            }
        }
        if (p > 1)
            result -= result / p;
        return result;
    }

    static int NOD(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        return NOD(b, a % b);
    }

    static int NODsKoef(int c, int m, ref int x, ref int y)
    {
        if (m == 0)
        {
            x = 1;
            y = 0;
            return c;
        }

        int x1 = 0, y1 = 0;
        int nod = NODsKoef(m, c % m, ref x1, ref y1);

        x = y1;
        y = x1 - (c / m) * y1;

        return nod;
    }

    static int ObratnCh(int c, int m)
    {
        int x = 0, y = 0;
        int gcd = NODsKoef(c, m, ref x, ref y);
        if (gcd != 1)
        {
            return -1;
        }
        else
        {
            return (x % m + m) % m;
        }
    }

    static long PowMod(long a, long x, long p)
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

    static int Encoding(string message, int N, int OpenKey, List<int> encoded)
    {
        foreach (char c in message)
        {
            long ASC = (long)c;
            if (ASC > N)
            {
                return -1;
            }
            long encr = PowMod(ASC, OpenKey, N);
            encoded.Add((int)encr);
        }
        return 0;
    }

    static void Decoding(long encSimv, int N, int ClosedKey, List<int> decoded)
    {
        long decr = PowMod(encSimv, ClosedKey, N);
        decoded.Add((int)decr);
    }

    static void Main(string[] args)
    {
        Random rnd = new Random();
        int p = 10, q = 10;

        while (!IsPrime(p))
        {
            p = Random(1000, 7000);
        }
        while (!IsPrime(q))
        {
            q = Random(1000, 7000);
        }

        int N = p * q;
        int Eilr = Eiler(N);
        int OpenKey = Random(1000, Eilr);

        while (NOD(OpenKey, Eilr) != 1)
        {
            OpenKey = Random(1000, Eilr);
        }

        int ClosedKey = ObratnCh(OpenKey, Eilr);

        Console.WriteLine("Enter the encryption text");
        string message = Console.ReadLine();

        List<int> encoded = new List<int>();
        int enc = Encoding(message, N, OpenKey, encoded);
        if (enc == -1)
        {
            Console.WriteLine("Error!");
            return;
        }
        List<int> decoded = new List<int>();
        Console.WriteLine("Encrypted text: ");
        foreach (long i in encoded)
        {
            Decoding(i, N, ClosedKey, decoded);
            Console.Write(i + " ");
        }
        Console.WriteLine();
        Console.WriteLine("Decrypted text: ");
        foreach (long i in decoded)
        {
            char c = (char)i;
            Console.Write(c);
        }
    }
}
