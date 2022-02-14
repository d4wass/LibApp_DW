using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetCustomers();
    IEnumerable<Customer> GetCustomersIncludeMembershipType();
    Customer GetCustomerById(int customerId);
    void AddCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    void Save();
}