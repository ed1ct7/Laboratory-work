//Задание 3  Создать класс Аптека со следующими скрытыми полями :
//•	название препарата,
//•	категория препарата
//•	цена
//•	количество
//•	дата выпуска(чч.мм.гггг).
//Для установки и получения значений всех полей использовать методы Set и Get.
// В классе описать методы для нахождения общей суммы денег по каждой категории на 
// имеющиеся в аптеке товары и определения списка товаров с датой выпуска больше года назад.
// В основной программе создать массив из N элементов(типа АПТЕКА).Данные ввести с клавиатуры.
// Вывести в виде таблицы информацию о товарах по категориям : название категории, название товара, цена, количество в наличии.

#define NOMINMAX
#define WIN32_LEAN_AND_MEAN

#include "header.h"
#include <locale.h>
#include <windows.h>
#include <stdio.h>

// Реализация методов класса Medication
Medication::Medication(string medication_name, string medication_category,
    float price, size_t amount, int day, int month, int year) {
    this->medication_name = medication_name;
    this->medication_category = medication_category;
    this->price = price;
    this->amount = amount;
    set_date_produced(day, month, year);
}

Medication::Medication() {
    int day, month, year;
    string medication_name, medication_category;
    float price;
    size_t amount;
    cout << "\nДанные о лекарстве" << endl;
    cout << "\tНазвание лекарства: "; cin >> medication_name;
    cout << "\tКатегория лекарства: "; cin >> medication_category;
    cout << "\tЦена лекарства: "; cin >> price;
    cout << "\tКоличество на складе: "; cin >> amount;


    cout << "\tДень производства: "; cin >> day;
    cout << "\tМесяц производства: "; cin >> month;
    cout << "\tГод производства: "; cin >> year;
    set_date_produced(day, month, year);
    set_medication_name(medication_name);
    set_medication_category(medication_category);
    set_price(price);
    set_amount(amount);
    cout << endl;
}

int Medication::get_time_passed_since_produced() {
    auto today = floor<days>(system_clock::now());
    year_month_day current_date{ today };
    auto difference = sys_days(current_date) - sys_days(this->date_produced);
    return difference.count();
}
string Medication::get_medication_name() { return this->medication_name; }
string Medication::get_medication_category() { return this->medication_category; }
float Medication::get_price() { return this->price; }
size_t Medication::get_amount() { return this->amount; }
year_month_day Medication::get_date_produced() { return this->date_produced; }

void Medication::set_date_produced(unsigned day, unsigned month, int year) {
    this->date_produced = year_month_day{
        chrono::year{year},
        chrono::month{month},
        chrono::day{day}
    };
    if (!this->date_produced.ok()) {
        cout << "Дата для лекарства " << this->get_medication_name()
            << " не действительна, \nвыставленная дата производства - сегодняшняя"
            << endl << endl;
        set_date_produced(floor<days>(system_clock::now()));
    }
}
void Medication::set_date_produced(year_month_day date_produced) {
    this->date_produced = date_produced;
    if (!this->date_produced.ok()) {
        cout << "Дата для лекарства " << this->get_medication_name()
            << " не действительна, \nвыставленная дата производства - сегодняшняя"
            << endl << endl;
        set_date_produced(floor<days>(system_clock::now()));
    }
}
void Medication::set_medication_name(string name) { this->medication_name = name; }
void Medication::set_medication_category(string category) { this->medication_category = category; }
void Medication::set_price(float price) { 
    if (price < 0) {
        cout << "Цена не может быть минусовой - выставленное значение 0" << endl;
        this->price = 0;
    }
    else {
        this->price = price;
    }
}
void Medication::set_amount(size_t amount) {
    if (amount < 0) {
        cout << "Количество не может быть минусовым - выставленное значение 0" << endl;
        this->amount = 0;
    }
    else {
        this->amount = amount;
    }
}

// Реализация методов класса Pharmacy
Pharmacy::Pharmacy() {
    add_Medication(Medication("Ибупрофен_Прототип_1", "Обезболивающее", 150.0, 500, 20, 5, 2024));
    add_Medication(Medication("Ибупрофен_Прототип_2", "Обезболивающее", 200.0, 1500, 21, 5, 2024));
    add_Medication(Medication("Ибупрофен_Прототип_3", "Обезболивающее", 300.0, 3000, 22, 5, 2024));
    add_Medication(Medication("Називин", "От насморка", 120.5, 200, 30, 1, 2025));
    add_Medication(Medication("Лоратадин", "Антигистаминное", 85.9, 300, 25, 3, 2023));
    add_Medication(Medication("Парацетамол", "Жаропонижающее", 55.0, 1000, 14, 2, 2021));
    add_Medication(Medication("Аспирин", "Противовоспалительное", 90.0, 800, 0, 4, 2022));
    add_Medication(Medication("Стрепсилс", "От боли в горле", 180.0, 150, 20, 6, 2024));
    add_Medication(Medication("Амбробене", "От кашля", 220.0, 400, 5, 5, 2025));
    add_Medication(Medication("Нурофен", "Обезболивающее", 170.0, 600, 45, 1, 2025));
    add_Medication(Medication("Активированный уголь", "Сорбент", 30.0, 100, 10, 10, 2024));
    add_Medication(Medication("ТераФлю", "От простуды", 250.0, 300, 15, 11, 2024));
}
string date_to_string(const year_month_day& ymd) {
    return format("{}-{}-{}",
        static_cast<int>(ymd.year()),
        static_cast<unsigned>(ymd.month()),
        static_cast<unsigned>(ymd.day())
    );
}
void Pharmacy::add_Medication(Medication medication) {
    string category = medication.get_medication_category();
    bool uniqueMedicine = true;

    if (find(Categories.begin(), Categories.end(), category) == Categories.end()) {
        Categories.push_back(category);
    }

    for (auto med : Medications) {
        if (med.get_medication_name() == medication.get_medication_name()) {
            cout << "Лекарство с именем '" << medication.get_medication_name()
                << "' уже существует" << endl;
            uniqueMedicine = false;
            break;
        }
    }

    if (uniqueMedicine) {
        Medications.push_back(medication);
    }
}

void Pharmacy::add_Medication() {
    Medication medication;
    string category = medication.get_medication_category();
    bool uniqueMedicine = true;

    if (find(Categories.begin(), Categories.end(), category) == Categories.end()) {
        Categories.push_back(category);
    }

    for (auto &med : Medications) {
        if (med.get_medication_name() == medication.get_medication_name() 
            && med.get_date_produced() == medication.get_date_produced()
            && med.get_price() == medication.get_price()) {
            med.set_amount(med.get_amount() + medication.get_amount());
            uniqueMedicine = false;
            break;
        }
    }

    if (uniqueMedicine) {
        Medications.push_back(medication);
    }
}

float Pharmacy::money_in_total() {
    float money = 0;
    for (auto& medication : Medications) {
        money += medication.get_price() * medication.get_amount();
    }
    return money;
}

float Pharmacy::money_in_total(string medication_category) {
    float money = 0;
    for (auto& medication : Medications) {
        if (medication.get_medication_category() == medication_category) {
            money += medication.get_price() * medication.get_amount();
        }
    }
    return money;
}

void Pharmacy::coutTableInformation() {
    cout << endl << endl;
    cout << "Общая стоимость всех лекарств: " << this->money_in_total() << endl;
    for (auto categorie : Categories) {
        cout << "Категория: " << categorie << " общая стоимость: "
            << money_in_total(categorie) << "$" << endl;

        printf("|%-25s|%-25s|%-25s|",
            "Название лекарства",
            "Цена лекарства",
            "Количество на складе");
        cout << endl;

        for (auto medication : Medications) {
            if (medication.get_medication_category() == categorie) {
                printf("|%-25s|%-25.2f|%-25d|",
                    medication.get_medication_name().c_str(),   // string (%-25s)
                    medication.get_price(),                     // float/double (%-25.2f for 2 decimal places)
                    (int)medication.get_amount()                     // int (%-25d)
                );
                cout << endl;
            }
        }
        cout << endl;
    }

    cout << endl << "Товары с датой выпуска больше года назад" << endl;
    bool flag = true;

    printf("|%-25s|%-15s|%-15s|\n",
        "Название лекарства",
        "Дата выхода",
        "Дней прошло");

    for (auto medication : Medications) {
        if (medication.get_time_passed_since_produced() > 365) {
            printf("|%-25s|%-15s|%-15d|\n",
                medication.get_medication_name().c_str(),
                date_to_string(medication.get_date_produced()).c_str(),
                medication.get_time_passed_since_produced());
            flag = false;
        }
    }
    if (flag) {
        cout << "\tтаких товаров нет" << endl;
    }
    cout << endl;
}

void Pharmacy::coutTable() {
    printf("|%-25s|%-25s|%-25s|%-25s|%-18s|",
        "Название лекарства",
        "Категория лекарства",
        "Цена лекарства",
        "Количество на складе",
        "Дата производства");

    cout << endl;

    for (auto medication : Medications) {
        printf("|%-25s|%-25s|%-25.2f|%-25d|%-18s|",
            medication.get_medication_name().c_str(),               // string (%-25s)
            medication.get_medication_category().c_str(),           // string (%-25s)
            medication.get_price(),                                 // float/double (%-25.2f for 2 decimal places)
            (int)medication.get_amount(),                           // int (%-25d)
            date_to_string(medication.get_date_produced()).c_str()  // string (%-25s)
        );
        cout << endl;
    }
    cout << endl;
}

void Pharmacy::delete_Medicine()
{
    unsigned day, month; int year;
    string name; 

    cout << "Введите имя лекарства: "; cin >> name;
    cout << "День производства: "; cin >> day;
    cout << "Месяц производства: "; cin >> month;
    cout << "Год производства: "; cin >> year;

    auto date_produced = year_month_day{
       chrono::year{year},
       chrono::month{month},
       chrono::day{day}
    };

    for (size_t i = 0; i < Medications.size(); i++)
    {
        if (Medications[i].get_medication_name() == name && Medications[i].get_date_produced() == date_produced)
        {
            Medications.erase(Medications.begin() + i);
        }
    }
    cout << endl;
}

int main() {
    setlocale(LC_ALL, "Ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    Pharmacy test;
    cout << endl << endl;

    size_t option = 0;
    for (;;) {
        cout << "Выберете действие" << endl;
        cout << "\t0 - вывод всех лекарств" << endl;
        cout << "\t1 - вывод информации" << endl;
        cout << "\t2 - ввод лекарства" << endl;
        cout << "\t3 - удаление лекарства" << endl;
        cout << "\t4 - выход программы" << endl;
        cout << "- "; cin >> option;
        switch (option)
        {
        default:
            cout << "Опция недоступна, попробуйте ещё раз" << endl;
            break;
        case 0:
            test.coutTable();
            break;
        case 1:
            test.coutTableInformation();
            break;
        case 2:
            test.add_Medication();
            break;
        case 3:
            test.delete_Medicine();
            break; 
        case 4:
            return 0;
        }
    }
    return 0;
}