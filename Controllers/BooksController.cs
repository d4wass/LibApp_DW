using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Repositories;
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
            var books = _bookRepository.GetBooksIncludeGenres();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBooksIncludeGenres()
                .SingleOrDefault(b => b.Id == id);

            return View(book);
        }

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

        public IActionResult New()
        {
            var genres = _genresRepository.GetGenres().ToList();

            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View("BookForm", viewModel);
        }
        
    }
}
