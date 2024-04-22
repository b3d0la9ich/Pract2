using System;

namespace ModularExponentiation
{
    class Program
    {
        static int NOD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            return NOD(b, a % b);
        }

        static bool IsPrime(int a)
        {
            if (a == 1) return false;
            for (int i = 2; i <= Math.Sqrt(a); i++)
            {
                if (a % i == 0) return false;
            }
            return true;
        }

        static int EulerFunc(int p)
        {
            int result = p;
            for (int i = 2; i <= Math.Sqrt(p); i++)
            {
                while (p % i == 0)
                {
                    p /= i;
                }
                result -= result / i;
            }
            if (p > 1)
            {
                result -= result / p;
            }

            return result;
        }

        static int ModExp(int basis, int power, int modP)
        {
            if (power == 0)
            {
                return 1;
            }
            int z = ModExp(basis % modP, power / 2, modP) % modP;
            if (power % 2 == 0)
            {
                return (z * z) % modP;
            }
            else
            {
                return ((basis % modP) * ((z * z) % modP)) % modP;
            }
        }

        static (int, int) Remainder(ref int basis, ref int power, ref int modP)
        {
            int resultForPrime = 1;
            int logByNumber = (int)Math.Log(power, 2); // Log по основанию 2
            int power2 = power;
            while (power2 > 0)
            {
                int remainder = power2 % 2;
                if (remainder == 1)
                {
                    int oneDegree = (int)Math.Pow(2, power2);
                    resultForPrime *= ModExp(basis, oneDegree, modP);
                }
                power2 /= 2;
            }
            int remainderResult = resultForPrime % modP;
            return (1, remainderResult);
        }

        static void Main(string[] args)
        {
            int basis = 0, basis2 = 0, power = 0, power2 = 0, modP = 0;

            Console.Write("Введите основание числа: ");
            if (!int.TryParse(Console.ReadLine(), out basis))
            {
                Console.WriteLine("Ошибка: введено не число.");
                return;
            }

            Console.Write("Введите степень числа: ");
            if (!int.TryParse(Console.ReadLine(), out power))
            {
                Console.WriteLine("Ошибка: введено не число.");
                return;
            }

            Console.Write("Введите основание второго числа: ");
            if (!int.TryParse(Console.ReadLine(), out basis2))
            {
                Console.WriteLine("Ошибка: введено не число.");
                return;
            }

            Console.Write("Введите степень числа второго числа: ");
            if (!int.TryParse(Console.ReadLine(), out power2))
            {
                Console.WriteLine("Ошибка: введено не число.");
                return;
            }

            Console.Write("Введите модуль числа: ");
            if (!int.TryParse(Console.ReadLine(), out modP))
            {
                Console.WriteLine("Ошибка: введено не число.");
                return;
            }

            int result1 = ModExp(basis, power, modP);
            Console.WriteLine("1-ый результат: " + result1);
            int result2 = ModExp(basis2, power2, modP);
            Console.WriteLine("2-ой результат: " + result2);

            if (result1 > result2)
            {
                Console.WriteLine($"{basis}^{power} mod {modP} > {basis2}^{power2} mod {modP}");
            }
            else if (result1 < result2)
            {
                Console.WriteLine($"{basis}^{power} mod {modP} < {basis2}^{power2} mod {modP}");
            }
            else
            {
                Console.WriteLine($"{basis}^{power} mod {modP} = {basis2}^{power2} mod {modP}");
            }
        }
    }
}