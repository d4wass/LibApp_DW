using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

using LibApp.Interfaces;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        
        // GET api/books/
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooksIncludeGenres()
                                .ToList()
                                .Select(_mapper.Map<Book, BookDto>);

            return Ok(books);
        }

        //GET /api/books/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = _bookRepository.GetBooksIncludeGenres().SingleOrDefault(b => b.Id == id)!;
            await Task.Delay(2000);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(book));
        }
        
        //POST /api/books/{id}
        [HttpPost]

        public BookDto CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var book = _mapper.Map<Book>(bookDto);
            _bookRepository.AddBook(book);
            _bookRepository.Save();
            bookDto.Id = book.Id;

            return bookDto;
        }
        
        //PUT /api/books/{id}
        [HttpPut("{id:int}")]
        public void UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var bookInDb = _bookRepository.GetBookById(id);
            if (bookDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _mapper.Map(bookDto, bookInDb);
            _bookRepository.Save();
        }
        [Authorize(Roles="Owner, StoreManager")]
        [HttpDelete("{id:int}")]
        public void DeleteBook(int id)
        {
            var bookInDb = _bookRepository.GetBookById(id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            _bookRepository.DeleteBook(bookInDb);
            _bookRepository.Save();
        }
        
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
    }
}
