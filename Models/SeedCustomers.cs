using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models;

public static class SeedCustomers
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Customers.Any())
            {
                Console.WriteLine("Database already seeded");
                return;
            }
            context.Customers.AddRange(
                new Customer
                {
                    Birthdate = DateTime.Now.AddYears(-25),
                    MembershipTypeId = 3,
                    HasNewsletterSubscribed = true,
                    Name = "Roman",
                },
                new Customer
                {
                    Birthdate = DateTime.Now.AddYears(-20),
                    MembershipTypeId = 1,
                    HasNewsletterSubscribed = false,
                    Name = "Kate",
                },
                new Customer
                {
                    Birthdate = DateTime.Now.AddYears(-18),
                    MembershipTypeId = 2,
                    HasNewsletterSubscribed = true,
                    Name = "Paul",
                }
            );
            context.SaveChanges();
        }
    } 
    
}