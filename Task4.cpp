#include <iostream>
#include <string>
#include <vector>
#include <cmath>

using namespace std;

bool is_prime(int p) {
    if (p <= 1) return false;
    for (int i = 2; i <= sqrt(p); i++) {
        if (p % i == 0) return false;
    }
    return true;
}

int Random(int min, int max) {
    return rand() % (max - min + 1) + min;
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

int NOD (int a, int b) {
    if (b == 0) {
        return a;
    }
    return NOD(b , a % b);
}

int NODsKoef(int c, int m, int& x, int& y) {
    if (m == 0) {
        x = 1;
        y = 0;
        return c;
    }

    int x1, y1;
    int nod = NODsKoef(m, c % m, x1, y1);

    x = y1;
    y = x1 - (c / m) * y1;

    return nod;
}

int ObratnCh(int c, int m) {
    int x, y;
    int gcd = NODsKoef(c, m, x, y);
    if (gcd != 1) {
        return -1;
    } 
    else {
        return (x % m + m) % m;
    }
}

long long pow_mod(long long a, long long x, long long p) {
    long long result = 1;
    a = a % p;
    while (x > 0) {
        if (x % 2 == 1) {
            result = (result * a) % p;
        }
        a = (a * a) % p;
        x /= 2;
    }
    return result;
}

int encoding(string message, int N, int OpenKey, vector<int>& encoded) {
    for (char c : message) {
        long long ASC = static_cast<long long>(c);
        if (ASC > N) {
            return -1;
        }
        long long encr = pow_mod(ASC, OpenKey, N);
        encoded.push_back(encr);
    }
    return 0;
}

void decoding(long long encSimv, int N, int ClosedKey, vector<int>& decoded) {
    long long decr = pow_mod(encSimv, ClosedKey, N);
        decoded.push_back(decr);
}

int main() {

    srand(time(0));
    int p=10, q=10;

    while (!is_prime(p)) {
        p = Random (1000, 7000);
    }
    while (!is_prime(q)) {
        q = Random (1000, 7000);
    }

    int N = p*q;
    int Eilr = Eiler(N);
    int OpenKey = Random(1000, Eilr);

    while (NOD(OpenKey,Eilr) !=1 ) {
        OpenKey = Random(1000, Eilr);
    }

    int ClosedKey = ObratnCh(OpenKey, Eilr);

    cout << "Enter the encryption text" << endl;
    string message;
    getline(cin, message);
    
    vector <int> encoded;
    int enc = encoding(message, N, OpenKey, encoded);
    if (enc == -1) {
        cout << "Error!" << endl;
        return -1;
    }
    vector <int> decoded;
    cout << "Encrypted text: " << endl;
    for (long long i : encoded) {
        decoding(i, N, ClosedKey, decoded);
        cout << i << " ";
    }
    cout << endl;
    cout << "Decrypted text: " << endl;
    for (long long i : decoded) {
        char c = static_cast<char>(i);
        cout << c;
    }
    return 0;
}