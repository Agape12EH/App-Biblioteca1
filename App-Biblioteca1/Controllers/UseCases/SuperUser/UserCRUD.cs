using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.UseCases.SuperUser
{
    [ApiController]
    [Route("api/UserCRUD")]
    public class UserCRUD : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UserCRUD(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        //GET:api/BooksCRUD/getAllBooks
        [HttpGet("/getBooksCRUD")]
        public async Task<IEnumerable<BooksDTO>> GetCRUD()
        {
            var books = await context.Books.ToListAsync();
            var booksDTO = mapper.Map<IEnumerable<BooksDTO>>(books);
            return booksDTO;
        }


        //GET:api/BooksCRUD/getAllBooks/{guidBook}
        [HttpGet("/getOneBookCRUD/{guidBook}")]
        public IActionResult GetOneBookCRUD(Guid guidBook)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == guidBook);

            if (book is null)
            {
                return NotFound();
            }

            var bookDTO = mapper.Map<BooksDTO>(book);
            return book != null ? Ok(mapper.Map<BooksDTO>(book)) : NotFound();
        }

        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOneCRUD")]
        public async Task<ActionResult> AddOneCRUD([FromBody] BooksDTO bookDTO)
        {
            var book = mapper.Map<Books>(bookDTO);
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }

        //POST:api/BooksCRUD/addVarious
        [HttpPost("/addVariousCRUD")]
        public async Task<ActionResult> AddVariousCRUD([FromBody] IEnumerable<BooksDTO> booksDTO)
        {
            var books = mapper.Map<IEnumerable<Books>>(booksDTO);
            context.AddRange(books);
            await context.SaveChangesAsync();
            return Ok(books);
        }

        //PUT:api/BooksCRUD/updateBook/{guidBook}
        [HttpPut("/updateBookCRUD/{guidBook}")]
        public async Task<IActionResult> UpdateBookCRUD(Guid guidBook, [FromBody] BooksDTO booksDTO)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == guidBook);
            return book != null ? Ok(mapper.Map<BooksDTO>(book)) : NotFound();
        }


        //GET:api/BooksCRUD/searchBy/{property}/{value}
        [HttpGet("/searchByCRUD/{property}/{value}")]
        public IActionResult SearchByCRUD(string property, string value)
        {
            var book = GetBookByProperty(property, value);

            if (book is null)
            {
                return NotFound();
            }
            var bookDTO = mapper.Map<BooksDTO>(book);
            return Ok(bookDTO);

        }

        //DELETE:api/BooksCRUD/deleteBook/{guidBook}
        [HttpDelete("/deleteBookCRUD/{guidBook}")]
        public async Task<ActionResult> DeleteCRUD(Guid guidBook)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == guidBook);
            if (book is null)

            {
                return NotFound();
            }

            context.Remove(book);
            await context.SaveChangesAsync();
            return Ok();
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
                "inventory" => context.Books.FirstOrDefault(b => b.StoreId == int.Parse(value)),
                _ => null,
            };
        }
    }
}
