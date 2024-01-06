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
        public async Task<IEnumerable<BooksDTO>> Get()
        {
            var books = await context.Books.ToListAsync();
            var booksDTO = mapper.Map<IEnumerable<BooksDTO>>(books);
            return booksDTO;
        }

        //GET:api/BooksCRUD/getAllBooks/{guidBook}
        [HttpGet("/getOneBook/{guidBook}")]
        public IActionResult Get(Guid guidBook)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == guidBook);

            if (book is null)
            {
                return NotFound();
            }

            var bookDTO = mapper.Map<BooksDTO>(book);
            return Ok(bookDTO);
        }

        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOne")]
        public async Task<ActionResult> Post(BooksDTO bookDTO)
        {
            var book = mapper.Map<Books>(bookDTO);
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }

        //POST:api/BooksCRUD/addVarious
        [HttpPost("/addVarious")]
        public async Task<ActionResult> Post(IEnumerable<BooksDTO> booksDTO)
        {
            var books = mapper.Map<IEnumerable<Books>>(booksDTO);
            context.AddRange(books);
            await context.SaveChangesAsync();
            return Ok(books);
        }

        //PUT:api/BooksCRUD/updateBook/{guidBook}
        [HttpPut("/updateBook/{guidBook}")]
        public async Task<IActionResult> Update(Guid guidBook, [FromBody] BooksDTO booksDTO)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == guidBook);
            if (book is null)
            {
                return NotFound();
            }
            var bookDTO = mapper.Map<BooksDTO>(book);
            return Ok(bookDTO);
        }

        //DELETE:api/BooksCRUD/deleteBook/{guidBook}
        [HttpDelete("/deleteBook/{guidBook}")]
        public async Task<ActionResult> Delete(Guid guidBook)
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
    }
}
