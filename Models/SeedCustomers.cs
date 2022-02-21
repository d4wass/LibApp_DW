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
                    Id = 1,
                    Name = "Roman",
                    Birthdate = DateTime.Now.AddYears(-25),
                    HasNewsletterSubscribed = true,
                    MembershipTypeId = 3
                }
            );
            context.SaveChanges();
        }
    } 
    
}