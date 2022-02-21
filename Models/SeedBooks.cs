using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models;

public class SeedBooks
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Books.Any())
            {
                Console.WriteLine("Database already seeded");
                return;
            }
            context.Books.AddRange(
                new Book
                {
                    Name = "Harry Potter and Half-Blood Prince",
                    AuthorName = "J.K. Rowling",
                    DateAdded = DateTime.Now,
                    ReleaseDate = DateTime.Today.AddDays(-400),
                    GenreId = 6,
                    NumberAvailable = 10,
                    NumberInStock = 5
                },
                new Book
                {
                    Name = "The Lord of the Rings",
                    AuthorName = "J.R.R Tolkien",
                    DateAdded = DateTime.Now,
                    ReleaseDate = DateTime.Today.AddDays(-400),
                    GenreId = 6,
                    NumberAvailable = 15,
                    NumberInStock = 0
                },
                new Book
                {
                    Name = "Don Quixote",
                    AuthorName = "Miguel de Cervantes",
                    DateAdded = DateTime.Now,
                    ReleaseDate = DateTime.Today.AddDays(-400),
                    GenreId = 1,
                    NumberAvailable = 20,
                    NumberInStock = 15
                }
            );
            context.SaveChanges();
        }
    } 
}