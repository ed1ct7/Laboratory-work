//1. Даны 3 целых числа.Написать функции Min3 и Max3 нахождения большего и
//меньшего из 3 - х чисел.

#include <iostream>

int Min3(int a, int b, int c) {
    int min_ab = (a < b) ? a : b;
    return (min_ab < c) ? min_ab : c;
}
int Max3(int a, int b, int c) {
    int max_ab = (a > b) ? a : b;
    return (max_ab > c) ? max_ab : c;
}