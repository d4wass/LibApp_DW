using System.Collections.Generic;
using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Customer> GetCustomers()
    {
        return _context.Customers;
    }

    public IEnumerable<Customer> GetCustomersIncludeMembershipType()
    {
        return _context.Customers.Include(c => c.MembershipType);
    }

    public Customer GetCustomerById(int customerId)
    {
        return _context.Customers.Find(customerId);
    }

    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public void UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public void DeleteCustomer(Customer customer)
    {
        _context.Customers.Remove(customer);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}