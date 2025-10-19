#include <iostream>
#include <stdio.h>
#include <cmath>

using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");

    float x = 0; float  xStep = 0; float xEnd = 0;
    float y = 0; float yStep = 0; float yEnd = 0;

    printf("¬ведите x, шаг x, и конец x\n");
    scanf_s("%f", &x);
    scanf_s("%f", &xStep);
    scanf_s("%f", &xEnd);

    printf("¬ведите y, шаг y, и конец y\n");
    scanf_s("%f", &y);
    scanf_s("%f", &yStep);
    scanf_s("%f", &yEnd);

    printf("  X\t  Y\t  F\n");
    for (; x <= xEnd; x += xStep) {
        x = round(x * 100) / 100;
        for (float j = y; j <= yEnd; j += yStep)
        {
            y = round(y * 100) / 100;
            if ((x + j) / (x * j) < 0) {
                printf("%.2f\t %.2f\t %.2f\n", x, j, -1 * powf(abs((x + j) / (x * j)), (1.0 / 3)));
            }
            else if (x * j != 0)
            {
                printf("%.2f\t %.2f\t %.2f\n", x, j, powf((x + j) / (x * j), (1.0/3)));
            }
            else {
                printf("%.2f\t %.2f\t ------\n", x, j);
            }
        }
    }
    return 0;
}