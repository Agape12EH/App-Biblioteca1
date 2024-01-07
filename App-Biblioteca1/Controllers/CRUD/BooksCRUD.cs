using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.CRUD
{
    [ApiController]
    [Route("api/BooksCRUD")]
    public class BooksCRUD : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
       
        public BooksCRUD(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        //GET:api/BooksCRUD/getAllBooks
        [HttpGet("/getAllBooks")]
        public async Task<IEnumerable<BooksDTO>> GetAllBooks() =>
             mapper.Map<IEnumerable<BooksDTO>>(await context.Books.ToListAsync());

        //GET:api/BooksCRUD/getAllBooks/{guidBook}
        [HttpGet("/getOneBook/{guidBook}")]
        public IActionResult GetOneBook(Guid guidBook)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == guidBook);
            return book != null ? Ok(mapper.Map<BooksDTO>(book)) : NotFound();
        }
        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOne")]
        public async Task<ActionResult> AddOne([FromBody] BooksDTO bookDTO)
        {
            var book = mapper.Map<Books>(bookDTO);
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }
        //POST:api/BooksCRUD/addVarious
        [HttpPost("/addVarious")]
        public async Task<ActionResult> AddVarious([FromBody] IEnumerable<BooksDTO> booksDTO)
        {
            var books = mapper.Map<IEnumerable<Books>>(booksDTO);
            context.AddRange(books);
            await context.SaveChangesAsync();
            return Ok(books);
        }
        //PUT:api/BooksCRUD/updateBook/{guidBook}
        [HttpPut("/updateBook/{guidBook}")]
        public async Task<IActionResult> UpdateBook(Guid guidBook, [FromBody] BooksDTO booksDTO)//
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == guidBook);
            return book != null ? Ok(mapper.Map<BooksDTO>(book)) : NotFound();
        }
        //DELETE:api/BooksCRUD/deleteBook/{guidBook}
        [HttpDelete("/deleteBook/{guidBook}")]
        public async Task<ActionResult> DeleteBook(Guid guidBook)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == guidBook);
            if (book == null) return NotFound();

            context.Remove(book);
            await context.SaveChangesAsync();
            return Ok();
        }

        //GET:api/BooksCRUD/searchBy/{property}/{value}
        [HttpGet("/searchBy/{property}/{value}")]
        public IActionResult SearchBy(string property, string value)
        {
            var book = GetBookByProperty(property, value);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BooksDTO>(book));
        }

        //GET:api/BooksCRUD/searchStateBook/{GuidBook}
        [HttpGet("/searchStateBook/{GuidBook}")]
        public IActionResult GetForStateBook(Guid GuidBook)
        {
            var stateOfBook = context.StateBooks.FirstOrDefault(b => b.IdBook == GuidBook);

            if (stateOfBook is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StateBookDTO>(stateOfBook));
        }

        //GET:api/BooksCRUD/searchLoanBook/{GuidBook}
        [HttpGet("/searchLoanBook/{NameBook}")]
        public IActionResult GetForLoan(string NameBook)
        {
            var loan = context.Loans.FirstOrDefault(b => b.Books.Any(sb => sb.Title == NameBook));

            if (loan is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<LoanDTO>(loan));
        }

        //GetBookByProperty(property, value);
        private Books GetBookByProperty(string property, string value)
        {
            return property.ToLower() switch
            {
                "name" => context.Books.FirstOrDefault(b => b.Title.Contains(value)),
                "author" => context.Books.FirstOrDefault(b => b.Author.Contains(value)),
                "isbn" => context.Books.FirstOrDefault(b => b.ISBN.Contains(value)),
                "gender" => context.Books.FirstOrDefault(b => b.Gender.Contains(value)),
                "agepublication" => context.Books.FirstOrDefault(b => b.AgePublication == DateOnly.Parse(value)),
                //"inventory" => context.Books.FirstOrDefault(b => b.InventoryId == int.Parse(value)),
                _ => null,
            }; 
        }
    }
}
