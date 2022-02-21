using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LibApp.ViewModels;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace LibApp.Controllers
{
  
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenresRepository _genresRepository;

        public BooksController(IBookRepository bookRepository, IGenresRepository genresRepository)
        {
            _bookRepository = bookRepository;
            _genresRepository = genresRepository;
        }

        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBooksIncludeGenres()
                .SingleOrDefault(b => b.Id == id);

            return View(book);
        }
        [Authorize(Roles = "Owner, StoreManager")]
        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBooks().SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _genresRepository.GetGenres().ToList()
            };

            return View("BookForm", viewModel);
        }
        [Authorize(Roles = "Owner, StoreManager")]
        public IActionResult New()
        {
            var genres = _genresRepository.GetGenres().ToList();

            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View("BookForm", viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _bookRepository.AddBook(book);
            }
            else
            {
                var bookInDb = _bookRepository.GetBooks().Single(c => c.Id == book.Id);
                bookInDb.Name = book.Name;
                bookInDb.AuthorName = book.AuthorName;
                bookInDb.GenreId = book.GenreId;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.DateAdded = book.DateAdded;
                bookInDb.NumberInStock = book.NumberInStock;
            }

            try
            {
                _bookRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }

   
    }
}
