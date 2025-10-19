//Задание 3  Создать класс Аптека со следующими скрытыми полями :
//•	название препарата,
//•	категория препарата
//•	цена
//•	количество
//•	дата выпуска(чч.мм.гггг).
//Для установки и получения значений всех полей использовать методы Set и Get.
//В классе описать методы для нахождения общей суммы денег по каждой категории на 
// имеющиеся в аптеке товары и определения списка товаров с датой выпуска больше года назад.
// В основной программе создать массив из N элементов(типа АПТЕКА).Данные ввести с клавиатуры.
// Вывести в виде таблицы информацию о товарах по категориям : название категории, название товара, цена, количество в наличии.
//
//

#include <iostream>
#include <string>
#include <vector>
#include <chrono>
#include <ctime>  
using namespace std;

class Medication {
    string medication_name;
    string medication_category;
    float price;
    size_t amount;
    //date date_produced;
public:
    string get_medication_name() {
        return this->medication_name;
    }
    string get_medication_category() {
        return this->medication_category;
    }
    float get_price() {
        return this->price;
    }
    size_t get_amount() {
        return this->amount;
    }
  /*  ///DATE///
    tm get_date_produced(){
        return this->date_produced;
    }
    void set_date_produced(tm date) {
        this->date_produced = date;
    }
    void set_date_produced(int day, int month, int year) {
        this->date_produced = day;
        this->date_produced = month;
        this->date_produced = year;
    }
    ///DATE///*/
    void set_medication_name(string name) {
        this->medication_name = name;
    }
    void set_medication_category(string category) {
        this->medication_category = category;
    }
    void set_price(float price) {
        this->price = price;
    }
    void set_amount(size_t amount) {
        this->amount = amount;
    }
};

class Pharmacy {
public:
    void add_Medication(Medication medication) {
        if (find(Medications.begin(), Medications.end(), medication) != Medications.end())
            Medications.push_back(medication);
        else
            cout << "such medication already exist";
    }
    float money_in_total() {
        float money;
        for (auto& medication : Medications) // access by reference to avoid copying
        {
            money += medication.get_price();
        }
    }
    float money_in_total(string medication_category) {
        float money;
        for (auto& medication : Medications) // access by reference to avoid copying
        {
            if (medication.get_medication_category() == medication_category) {
                money += medication.get_price();
            }
        }
    }
private:
    vector <Medication> Medications;
};

int main() {
    using namespace std::chrono;

    // Get today's date (C++20)
    //auto today = floor<days>(system_clock::now());
    //year_month_day ymd{ today };  // Convert to year/month/day

    //std::cout << "Today is: "
    //    << static_cast<int>(ymd.year()) << "-"
    //    << static_cast<unsigned>(ymd.month()) < < "-"
    //    << static_cast<unsigned>(ymd.day()) << "\n";

    //// Store a specific date (2023-12-25)
    //year_month_day christmas{ year(2023) / 12 / 25 };
    //std::cout << "Christmas: "
    //    << static_cast<int>(christmas.year()) << "-"
    //    << static_cast<unsigned>(christmas.month()) << "-"
    //    << static_cast<unsigned>(christmas.day()) << "\n";

    return 0;
}