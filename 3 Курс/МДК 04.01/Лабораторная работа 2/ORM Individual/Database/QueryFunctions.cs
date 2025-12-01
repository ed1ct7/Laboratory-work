using Microsoft.EntityFrameworkCore;
using ORM_Individual.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ORM_Individual.Models.Database
{
    public static class QueryFunctions
    {
        public record OrderCustomerInfo(int OrderId, string CustomerName, string? EmployeeName, decimal? TotalAmount, string? OrderDate);

        public static IReadOnlyList<OrderCustomerInfo> JoinOrdersWithCustomersAndEmployees()
        {
            var context = DatabaseContext.GetContext();

            var joinedQuery = from order in context.Orders
                              join customer in context.Customers on order.CustomerId equals customer.Id
                              join employee in context.Employees on order.EmployeeId equals employee.Id into employees
                              from employee in employees.DefaultIfEmpty()
                              select new OrderCustomerInfo(
                                  order.Id,
                                  customer.FullName ?? string.Empty,
                                  employee?.FullName,
                                  order.TotalAmount,
                                  order.OrderDate);

            return joinedQuery.AsNoTracking().ToList();
        }

        public static IReadOnlyList<Customer> GetCustomersRegisteredBetween(DateTime from, DateTime to)
        {
            var context = DatabaseContext.GetContext();

            return context.Customers
                          .AsNoTracking()
                          .Where(c => c.RegistrationDate >= from && c.RegistrationDate <= to)
                          .OrderBy(c => c.RegistrationDate)
                          .ToList();
        }

        public static IReadOnlyList<Customer> GetRecentCustomers(int daysBack)
        {
            var cutoffDate = DateTime.Today.AddDays(-daysBack);
            var context = DatabaseContext.GetContext();

            return context.Customers
                          .AsNoTracking()
                          .Where(c => c.RegistrationDate >= cutoffDate)
                          .OrderByDescending(c => c.RegistrationDate)
                          .ToList();
        }
    }
}
