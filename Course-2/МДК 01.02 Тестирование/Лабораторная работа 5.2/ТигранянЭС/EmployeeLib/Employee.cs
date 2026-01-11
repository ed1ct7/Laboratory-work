using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ТигранянЭС.Projects.EmployeeLib
{
    public enum Education { PRIMARY, SECONDARY, SPECIALIZED_SECONDARY, HIGHER };
    public enum CurrentPosition { DIRECTOR, MANAGER, JUNIOR, MIDDLE, SENIOR };
    public class Employee
    {
        public Employee(string name, DateTime birthday,
            char gender, decimal salary,
            Education education, CurrentPosition currentPosition)
        {  //("Вася", DateTime.Now.AddYears(-35), 'м', (decimal)salary, Education.SPECIALIZED_SECONDARY, currentPosition);

            this.education = education;
            this.position = currentPosition;
            this.name = name;
            this.birthday = birthday;
            this.gender = gender;
            this.salary = salary;
        }
        public string Name
        {
            get => name; set => name = value;
        }
        public string name;
        public DateTime birthday;
        public char gender;
        public decimal salary;
        public decimal percentMonthlyPremium;
        public Education education;
        public CurrentPosition position;
        public bool checkName()
        {
            if (name.Length < 2 || name.Length > 15)
                return false;

            if (char.IsDigit(name[0]))
                return false;

            int digitCount = 0;
            string allowedSymbols = " -";

            foreach (char c in name)
            {
                if (char.IsDigit(c))
                {
                    digitCount++;
                    if (digitCount > 1)
                        return false;
                }
                else
                {
                    bool isCyrillic = c >= 'А' && c <= 'я';
                    bool isLatin = c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z';
                    bool isAllowedSymbol = allowedSymbols.Contains(c);

                    if (!isCyrillic && !isLatin && !isAllowedSymbol)
                        return false;
                }
            }

            return true;
        }
        public int timeUntilRetirement()
        {
            int ageInDays = (int)(DateTime.Today - birthday).TotalDays;
            DateTime timeOfRetirnment;
            if (gender == 'м'){timeOfRetirnment = birthday.AddYears(65);}
            else if (gender == 'ж') { timeOfRetirnment = birthday.AddYears(60);}
            else {throw new Exception("Wrong gender");}
            int daysBeforeRetirnment = (int)(timeOfRetirnment - DateTime.Today).TotalDays > 0 ? (int)(timeOfRetirnment - DateTime.Today).TotalDays : 0;
            return daysBeforeRetirnment;
        }
        public decimal monthPayment()
        {
            double salaryOut;
            //(директор и сеньор – 15%, миддл – 10%, джуниор – 5%, менеджер – 3% от оклада)
            if (position == CurrentPosition.DIRECTOR || position == CurrentPosition.SENIOR)
            {
                salaryOut = (double)salary * 1.15;
                return (decimal)salaryOut;
            }
            else if (position == CurrentPosition.MIDDLE)
            {
                salaryOut = (double)salary * 1.1;
                return (decimal)salaryOut;
            }
            else if (position == CurrentPosition.JUNIOR)
            {
                salaryOut = (double)salary * 1.05;
                return (decimal)salaryOut;
            }
            else if (position == CurrentPosition.MANAGER)
            {
                salaryOut = (double)salary * 1.03;
                return (decimal)salaryOut;
            }
            else
            {
                return salary;
            }
        }
    }
}