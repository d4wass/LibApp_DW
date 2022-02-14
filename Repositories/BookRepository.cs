using System.Collections.Generic;
using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public void AddBook(Book book)
    {
        _context.Books.Add(book);
    }

    public void DeleteBook(Book book)
    {
        _context.Books.Remove(book);
    }

    public Book GetBookById(int bookId)
    {
        return _context.Books.Find(bookId);
    }

    public IEnumerable<Book> GetBooksIncludeGenres()
    {
        return _context.Books.Include(b => b.Genre);
    }

    public IEnumerable<Book> GetBooks()
    {
        return _context.Books;
    }

    public void UpdateBook(Book book)
    {
        _context.Books.Update(book);
    }
    public void Save()
    {
        _context.SaveChanges();
    }

}