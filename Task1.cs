using System;
using System.Collections.Generic;

class MainClass {
    static int NOD(int a, int b) {
        if (b == 0) {
            return a;
        }
        return NOD(b, a % b);
    }

    static bool IsPrime(int a) {
        if (a == 1) {
            return false;
        }
        for (int i = 2; i <= Math.Sqrt(a); i++) {
            if (a % i == 0) {
                return false;
            }
        }
        return true;
    }

    static int EilerFunc(int p) {
        int result = p;
        for (int i = 2; i * i <= p; i++) {
            if (p % i == 0) {
                while (p % i == 0) {
                    p /= i;
                }
                result -= result / i;
            }
        }
        if (p > 1) {
            result -= result / p;
        }
        return result;
    }

    static int ModExp(int basis, int power, int modP) {
        if (power == 0) {
            return 1;
        }
        int z = ModExp(basis % modP, power / 2, modP) % modP;
        if (power % 2 == 0) {
            return (z * z) % modP;
        }
        else {
            return ((basis % modP) * ((z * z) % modP)) % modP;
        }
    }

    static Tuple<int, int> Remainder(ref int basis, ref int power, ref int modP) {
        Tuple<int, int> ost = Tuple.Create(1, 1);

        ost = Tuple.Create(ModExp(basis, power, modP), 1);

        int logByNumber = (int)Math.Log(power, 2);
        List<int> binary = new List<int>();
        int power2 = power;
        while (power2 > 0) {
            binary.Add(power2 % 2);
            power2 /= 2;
        }

        int resultForPrime = 1;
        for (int i = 0; i <= logByNumber; i++) {
            if (binary[i] == 1) {
                int oneDegree = (int)Math.Pow(2, i);
                resultForPrime *= ModExp(basis, oneDegree, modP);
            }
        }
        ost = Tuple.Create(ost.Item1, resultForPrime % modP);

        return ost;
    }

    public static void Main(string[] args) {
        int basis = 0, basis2 = 0, power = 0, power2 = 0, modP = 0;
        Console.Write("Введите основание числа: ");
        basis = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите степень числа: ");
        power = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите модуль числа: ");
        modP = Convert.ToInt32(Console.ReadLine());

        bool zahod = false;

        if (basis < modP && power == (modP - 1) && IsPrime(modP)) {
            Console.WriteLine($"По теореме Ферма получим: {basis}^{power} mod {modP} = 1");
            var ost = Remainder(ref basis, ref power, ref modP);
            Console.WriteLine($"Проверим, используя логарифм: {ost.Item2}");
            zahod = true;
        }
        if (NOD(basis, modP) == 1 && power == EilerFunc(modP)) {
            zahod = true;
            Console.WriteLine($"По теореме Эйлера получим: {basis}^{power} mod {modP} = 1");
            var ost = Remainder(ref basis, ref power, ref modP);
            Console.WriteLine($"Проверим, используя логарифм: {ost.Item2}");
        }
        if (!zahod) {
            power = EilerFunc(modP);
            int result = ModExp(basis, power, modP);
            var ost = Remainder(ref basis, ref power, ref modP);
            Console.WriteLine($"Результат, используя свойства сравнений = {ost.Item1}");
            Console.WriteLine($"Результат, используя логарифм = {ost.Item2}");
        }

        Console.Write("Введите основание второго числа, которое будете сравнивать по модулю: ");
        basis2 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите степень числа второго числа, которое будете сравнивать по модулю: ");
        power2 = Convert.ToInt32(Console.ReadLine());

        if (ModExp(basis, power, modP) > ModExp(basis2, power2, modP)) {
            Console.WriteLine($"{basis}^{power} mod {modP} > {basis2}^{power2} mod {modP}");
        }
        else if (ModExp(basis, power, modP) < ModExp(basis2, power2, modP)) {
            Console.WriteLine($"{basis}^{power} mod {modP} < {basis2}^{power2} mod {modP}");
        }
        else {
            Console.WriteLine($"{basis}^{power} mod {modP} = {basis2}^{power2} mod {modP}");
        }
    }
}
