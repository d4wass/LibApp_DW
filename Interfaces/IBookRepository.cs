using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces;

public interface IBookRepository
{
    IEnumerable<Book> GetBooks();
    IEnumerable<Book> GetBooksIncludeGenres();
    Book GetBookById(int bookId);
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
    void Save();
}