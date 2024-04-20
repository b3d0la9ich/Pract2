#include <iostream>
#include <cmath>
#include <string>
#include <cctype>
using namespace std;
// Функция для проверки простоты числа
bool is_prime(int p) {
    if (p <= 1) return false;
    for (int i = 2; i <= sqrt(p); i++) {
        if (p % i == 0) return false;
    }
    return true;
}

int NOD (int a, int b) {
    if (b == 0) {
        return a;
    }
    return NOD(b , a % b);
}

bool is_Eiler(int a, int x, int p) {
    if (NOD(a,p) == 1) {
        return true;
    }
    return false;
}

int Eiler(int p) {
    int result = p;
    for (int i = 2; i * i <= p; i++) {
        if (p % i == 0) {
            while (p % i == 0)
                p /= i;
            result -= result / i;
        }
    }
    if (p > 1)
        result -= result / p;
    return result;
}

bool is_Int (string n) {
    if (n[0] == '-') {
        n[0] = '1';
    }
    for (char c : n) {
        if (!isdigit(c)) {
            cout << "Некорректные данные" << endl;
            return false;
        }
    }
    return true;
}

// Функция для вычисления a^x mod p используя метод двоичного разложения степени
int pow_mod(int a, int x, int p) {
    int result = 1;
    a = a % p; // Уменьшаем a по модулю p
    while (x > 0) {
        if (x % 2 == 1) {
            result = (result * a) % p;
        }
        a = (a * a) % p;
        x /= 2;
    }
    return result;
}

int main() {
    string astr, xstr, pstr, bstr, ystr;
    int a, x, p, b, y;
    
    cout << "Введите a: ";
    cin >> astr;
    if (!is_Int(astr))  {
        return 1;
    };
    a = stoi(astr);
    cout << "Введите x: ";
    cin >> xstr;
    if (!is_Int(xstr))  {
        return 1;
    };
    x = stoi(xstr);
    cout << "Введите b: ";
    cin >> bstr;
    if (!is_Int(bstr))  {
        return 1;
    };
    b = stoi(bstr);
    cout << "Введите y: ";
    cin >> ystr;
    if (!is_Int(ystr))  {
        return 1;
    };
    y = stoi(ystr);
    cout << "Введите p: ";
    cin >> pstr;
    if (!is_Int(pstr))  {
        return 1;
    };
    p = stoi(pstr);
    // Проверяем, простое ли число p
    if (!is_prime(p)) {
        if (!is_Eiler(a,x,p)) {
            cout << "Для" << a << "^" << x << " mod " << p << " теорема Эйлера не выполняется" << endl;
        }
        if (!is_Eiler(b,y,p)) {
            cout << "Для" << b << "^" << y << " mod " << p << " теорема Эйлера не выполняется" << endl;
        }
        if (is_Eiler(a,x,p) && is_Eiler(b,y,p)) {
            if (pow_mod(a,x%Eiler(p),p)==pow_mod(b,y%Eiler(p),p)) {
                cout << Eiler(p) << endl;
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x%Eiler(p),p) << ") = (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y%Eiler(p),p) << ")" << endl;
            }
            else {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x%Eiler(p),p) << ") != (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y%Eiler(p),p) << ")" << endl;
            }
        }
        else {
            if (pow_mod(a,x,p)==pow_mod(b,y,p)) {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x,p) << ") = (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y,p) << ")" << endl;
            }
            else {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x,p) << ") != (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y,p) << ")" << endl;
            }
        }
    } 
    else {
        if (!is_Eiler(a,x,p)) {
            cout << "Для" << a << "^" << x << " mod " << p << " теорема Ферма не выполняется" << endl;
        }
        if (!is_Eiler(b,y,p)) {
            cout << "Для" << b << "^" << y << " mod " << p << " теорема Ферма не выполняется" << endl;
        }
        if (is_Eiler(a,x,p) && is_Eiler(b,y,p)) {
            if (pow_mod(a,x%(p-1),p)==pow_mod(b,y%(p-1),p)) {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x%(p-1),p) << ") = (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y%(p-1),p) << ")" << endl;
            }
            else {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x%(p-1),p) << ") != (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y%(p-1),p) << ")" << endl;
            }
        }
        else {
            if (pow_mod(a,x,p)==pow_mod(b,y,p)) {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x,p) << ") = (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y,p) << ")" << endl;
            }
            else {
                cout << " (" << a << "^" << x << " mod " << p << " = " << pow_mod(a,x,p) << ") != (" << b << "^" << y << " mod " << p << " = " << pow_mod(b,y,p) << ")" << endl;
            }
        }
    }
    return 0;
}