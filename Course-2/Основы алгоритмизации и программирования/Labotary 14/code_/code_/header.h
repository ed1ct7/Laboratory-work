#ifndef PHARMACY_SYSTEM_H
#define PHARMACY_SYSTEM_H

#include <string>
#include <chrono>
#include <vector>
#include <algorithm>
#include <iostream>

using namespace std;
using namespace chrono;

class Medication {
    string medication_name;
    string medication_category;
    float price;
    size_t amount;
    year_month_day date_produced;

public:
    Medication(string medication_name, string medication_category, float price, size_t amount,
               int day, int month, int year);
    Medication();
    
    int get_time_passed_since_produced();
    string get_medication_name();
    string get_medication_category();
    float get_price();
    size_t get_amount();
    year_month_day get_date_produced();

    void set_date_produced(unsigned day, unsigned month, int year);
    void set_date_produced(year_month_day date_produced);
    void set_medication_name(string name);
    void set_medication_category(string category);
    void set_price(float price);
    void set_amount(size_t amount);
};

class Pharmacy {
    vector<Medication> Medications;
    vector<string> Categories;

public:
    Pharmacy();
    
    void add_Medication(Medication medication);
    void add_Medication();
    float money_in_total();
    float money_in_total(string medication_category);
    void coutTableInformation();
    void coutTable();
    void delete_Medicine(string name);
    void delete_Medicine();
};

#endif // PHARMACY_SYSTEM_H