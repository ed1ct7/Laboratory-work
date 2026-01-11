using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EmployeeLib;
using static EmployeeLib.Employee;

namespace EmployeeUnitTests
{
    [TestClass]
    public class EmployeeTests
    {       
        [TestMethod]
        [DataRow(1000.0d, 0, 1150.0d)]
        [DataRow(1000.0d, 1, 1030.0d)]
        [DataRow(1000.0d, 2, 1050.0d)]
        [DataRow(1000.0d, 3, 1100.0d)]
        [DataRow(1000.0d, 4, 1150.0d)]
        public void MonthPaymentTests(double salary, CurrentPosition currentPosition, double expected)
        {
            //arrange

            Employee employee = new Employee("Вася", DateTime.Now.AddYears(-35), 'м', (decimal) salary, Education.SPECIALIZED_SECONDARY, currentPosition);

            //act
            decimal actual = employee.monthPayment();

            //assert
            Assert.AreEqual((decimal) expected, actual);
        }


        [TestMethod]
        [DataRow("Федор Борисович", true)]
        [DataRow("Федор12", false)]
        [DataRow("Федор Борисович1", false)]
        [DataRow("Федор1", true)]
        [DataRow("Федор-Борисович", true)]
        [DataRow("Фz", true)]
        [DataRow("1Федор", false)]
        [DataRow("Федор@", false)]


        public void CheckNameTests(string name, bool expected)
        {
            //arrange
            Employee employee = new Employee(name, DateTime.Now.AddYears(-35), 'м', 1000.0M, Education.SPECIALIZED_SECONDARY, CurrentPosition.SENIOR);

            //act
            bool actual = employee.checkName();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow('м', 65, 0)]
        [DataRow('м', 60, 365*5+1)]
        [DataRow('ж', 58, 2*365)]
        public void TimeUntilRetirementTests(char gender, int years, int expected) {
            //arrange
            Employee employee = new Employee("Вася", DateTime.Now.AddYears(-years), gender, 1000.0M,Education.SPECIALIZED_SECONDARY, CurrentPosition.SENIOR);
            //act
            int actual = employee.timeUntilRetirement();
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
