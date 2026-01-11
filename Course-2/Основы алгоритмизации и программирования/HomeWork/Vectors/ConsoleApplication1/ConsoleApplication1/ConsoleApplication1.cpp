#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

bool Pred(int x) {
    return (x % 2 != 0 && x != 0);
}

int main()
{
    vector<int> vec;
    vec = { 0,1,2,3,4,5,6,7,8,9,9,9,9 };
    
    int uneven = 0;
    for (auto iter = vec.begin(); iter < vec.end(); iter++)
    {
        if (Pred(*iter)){
            uneven += *iter;
        }
    }
    if (uneven <= 0) {
        remove(vec.begin(), vec.end(), max_element(vec.begin(), vec.end()));
    }

    for (auto iter = vec.begin(); iter < vec.end(); iter++)
    {
        cout << *iter << endl;
    }
}
