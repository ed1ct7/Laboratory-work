#include <iostream>
using namespace std;
void main()
{
    setlocale(LC_ALL, "Russian");
    int hCoord[2] = { 0,0 };
    int newCoords[2] = { 0,0 };

    cout << "¬ведите изначальные координаты кон€" << endl;
    cin >> hCoord[0] >> hCoord[1];
    cout << "¬ведите новые координаты кон€" << endl;
    cin >> newCoords[0] >> newCoords[1];

    bool Answer1 = ((hCoord[0] <= 8 && hCoord[0] >= 1) 
        || (hCoord[1] <= 8 && hCoord[1] >= 1)); //ѕроверка на действительность изначальной точки 

    bool Answer2 = ((newCoords[0] <= 8 && newCoords[0] >= 1) 
        || (newCoords[1] <= 8 && newCoords[1] >= 1)); //ѕроверка на действительность желаемой точки 

    bool Answer3 = ( abs(newCoords[0] - hCoord[0]) == 1 && 
        abs(newCoords[1] - hCoord[1]) == 2) || (abs(newCoords[1] - 
            hCoord[1]) == 1 && abs(newCoords[0] - hCoord[0]) == 2); //ѕроверка действительности хода

    bool Answer = Answer1 && Answer2 && Answer3;

    cout << boolalpha << Answer;
}
