
using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace App_Biblioteca1.Controllers
{
    [ApiController]
    [Route("api/BookGet")]
    public class BookGets : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public  BookGets(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //GET:api/BooksCRUD/getAllBooks
        [HttpGet("/GetAllBooks")]
        public async Task<IEnumerable<BooksDTO>> GetAllBooks()
        {
            return await context.Books
                .ProjectTo<BooksDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        //GET:api/BooksCRUD/getAllBooks/{guidBook} SOLO BUSQUEDA CON CODIGO BARRA
        [HttpGet("/GetOneBook/{bookId}")]
        public async Task<ActionResult> GetBook(Guid bookId)
        {
            var book = await context.Books.FindAsync(bookId);

            if (book == null || book.Delete == 1)
            {
                return await HandleDeleteBook(book);
            }
            return Ok(book);
        }

        private async Task<ActionResult> HandleDeleteBook(Books book)
        {
            if (book == null)
            {
                return NotFound("No se encontro el Libro");
            }

            var stateBook = await context.StateBooks
                .Where(sb => sb.Books == book)
                .OrderBy(sb => sb.Registrationdate)
                .FirstOrDefaultAsync();

            if (stateBook != null)
            {
                return NotFound($"El libro está marcado como {stateBook.State}");
            }
            return NotFound("No se encontró el libro en la tabla StateBook");
        }

    }
}
