#include <iostream>  
#include <cmath>  
#include <vector>  
  
using namespace std;  
  
int NOD (int a, int b) {  
    if (b == 0) {  
        return a;  
    }  
    return NOD(b , a % b);  
}  
  
bool Prime(int a) {  
    if (a == 1)  return false;  
    for (int i = 2; i < sqrt(a); i++) {  
        if (a % i == 0) return false;  
    }  
    return true;  
}  
  
int EilerFunc(int p) { // определяем количество взаимно простых чисел для p   
    int result = p;  
    for (int i = 2; i <= sqrt(p); i++) { //проходимся до корня из числа  
        //если число разделилось на i, то мы уменьшаем р на i так как все числа,  
        while (p % i == 0) { // которые делятся на i не являются взаимно простыми с p  
            p /= i;  
        }  
        result -= result / i; // из результата вычитаем количество чисел result/i  
    }  
    if (p > 1) { // если п - простое число, которое не разделилось, то оно простое и результатом  
        result -= result / p; // функции будет р-1  
    }  
  
    return result;  
}  
  
int modexp(int basis, int power, int modP) {  
    if (power == 0) {  
        return 1;  
    }  
    int z = modexp(basis % modP, power / 2, modP) % modP;  
    if (power % 2 == 0) {  
        return (z * z) % modP;  
    }  
    else {  
        return ((basis % modP) * ((z * z) % modP)) % modP;  
    }  
}  
  
pair<int, int> remainder(int& basis, int& power, int& modP) {  
    pair<int, int> ost = { 1,1 };  
  
    ost.first = modexp(basis, power, modP);  
  
    int logByNumber = log2(power);     //рассматриваем остатки через логарифм и 2сс.  
    vector<int> binary;  //переводим степень в 2сс.  
    int power2 = power;  
    while (power2 > 0) {  
        binary.push_back(power2 % 2);  
        power2 /= 2;  
    }  
  
    int resultForPrime = 1;  
    for (int i = 0; i <= logByNumber; i++) {  
        if (binary[i] == 1) { //бинарное не переворачиваем, а просто идем с "конца"  
            int oneDegree = pow(2, i);  
            resultForPrime *= modexp(basis, oneDegree, modP);  
        }  
    }  
    ost.second = resultForPrime % modP;  //после смотрим на остаток  
  
    return ost;  //вернем пару значений  
} 
  
int main() { 
    
    int basis = 0, basis2 = 0, power = 0, power2 = 0, modP = 0; 
 
    cout << "Введите основание числа: "; 
    cin >> basis; 
    if (cin.fail()) { 
        cout << "Ошибка: введено не число." << endl; 
        return 1; 
    } 
 
    cout << "Введите степень числа: "; 
    cin >> power; 
    if (cin.fail()) { 
        cout << "Ошибка: введено не число." << endl; 
        return 1; 
    } 
 
    cout << "Введите основание второго числа: "; 
    cin >> basis2; 
    if (cin.fail()) { 
        cout << "Ошибка: введено не число." << endl; 
        return 1; 
    } 
 
    cout << "Введите степень числа второго числа: "; 
    cin >> power2; 
    if (cin.fail()) { 
        cout << "Ошибка: введено не число." << endl; 
        return 1; 
    } 
 
    cout << "Введите модуль числа: "; 
    cin >> modP; 
    if (cin.fail()) { 
        cout << "Ошибка: введено не число." << endl; 
        return 1; 
    } 
    
    int result1 = modexp(basis, power, modP); 
    cout << "1-ый результат: " << result1 << endl; 
    int result2 = modexp(basis2, power2, modP); 
    cout << "2-ой результат: " << result2 << endl;  
 
    if (result1 > result2) { 
        cout << basis << "^" << power << " mod " << modP << " > " << basis2 << "^" << power2 << " mod " << modP << endl; 
    } else if (result1 < result2) { 
        cout << basis << "^" << power << " mod " << modP << " < " << basis2 << "^" << power2 << " mod " << modP << endl; 
    } else if (result1 = result2) { 
        cout << basis << "^" << power << " mod " << modP << " = " << basis2 << "^" << power2 << " mod " << modP << endl; 
    } 
    return 0; 
}