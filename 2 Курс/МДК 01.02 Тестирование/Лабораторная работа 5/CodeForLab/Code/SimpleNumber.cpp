//25. —оставить функцию, провер€ющую, €вл€ютс€ ли данное число простым.
//¬тора€ функци€ должна вернуть ближайшее большее данного простое
//число.Ќапример, дл€ числа 11 перва€ функци€ возвращает true, втора€ Ц
//13. ƒл€ числа 14 перва€ функци€ должна вернуть false, а втора€ Ц 17.

#include <iostream>
#include <cmath>

// ‘ункци€ проверки, €вл€етс€ ли число простым
bool isPrime(int n) {
    if (n <= 1) {
        return false;
    }
    if (n == 2) {
        return true;
    }
    if (n % 2 == 0) {
        return false;
    }
    for (int i = 3; i <= std::sqrt(n); i += 2) {
        if (n % i == 0) {
            return false;
        }
    }
    return true;
}

// ‘ункци€ поиска ближайшего большего простого числа
int nextPrime(int n) {
    int candidate = n + 1;
    while (true) {
        if (isPrime(candidate)) {
            return candidate;
        }
        candidate++;
    }
}
