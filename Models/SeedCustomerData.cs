using System;
using System.Collections.Generic;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedCustomerData
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
                                Name = "Joe",
                                HasNewsletterSubscribed = false,
                                MembershipTypeId = 1,
                                Birthdate = DateTime.Today.AddYears(-20)
                            },
                            new Customer
                            {
                                Id = 2,
                                Name = "Mitchel",
                                HasNewsletterSubscribed = true,
                                MembershipTypeId = 2,
                                Birthdate = DateTime.Today.AddYears(-25)
                            }
                            );
                        context.SaveChanges();
                    }
                }
    }
};

