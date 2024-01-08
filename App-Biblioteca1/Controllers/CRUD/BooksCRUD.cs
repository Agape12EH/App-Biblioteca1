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

        //GET:api/BooksCRUD/getAllBooks/{guidBook} SOLO BUSQUEDA CON CODIGO BARRA
        [HttpGet("/getOneBook/{guidBook}")]
        public IActionResult GetOneBook(Guid guidBook)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == guidBook);
            return book != null ? Ok(mapper.Map<BooksDTO>(book)) : NotFound();
        }
        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOne")]
        public async Task<ActionResult> AddBook(BooksDTO bookDTO)
        {
            if(string.IsNullOrEmpty(bookDTO.Title)
                && string.IsNullOrEmpty(bookDTO.Author)
                && string.IsNullOrEmpty(bookDTO.ISBN)
                && string.IsNullOrEmpty(bookDTO.Gender)
                && (bookDTO.AgePublication).HasValue) 
            {
                return BadRequest("Se requiere el ingreso de todos los campos");
            }

            var newBook = mapper.Map<Books>(bookDTO);

            //Buscar en tabla BookStore un registro Isbn 
            var existingStore = context.BookStore.FirstOrDefault(bs => bs.isbnBook == bookDTO.ISBN);

            if (existingStore == null)
            {
                //nuevo Almacen de ese libro
                var newStore = new BookStore
                {
                    DateStored = DateTime.UtcNow,
                    isbnBook = bookDTO.ISBN,
                };

                //agregar registro a db
                context.BookStore.Add(newStore);
                await context.SaveChangesAsync();

                newBook.StoreId = newStore.Id;

                
            }
            else
            {
                newBook.StoreId = existingStore.Id;

            }

            context.Books.Add(newBook);
            await context.SaveChangesAsync();

            CreateStateBook(newBook.Id, $"Libro creado: {newBook.Title} con ISBN {newBook.ISBN} creado" +
                $"y cargado al inventario de la Biblioteca");
            UpdateQuantity(newBook.ISBN);

            return Ok();
            
        }

        //metodo auxiliar
        private void CreateStateBook(Guid bookId, string takenAction)
        {
            var stateBook = new StateBook
            {
                IdBook = bookId,
                State = Models.Enums.StatesOfBooks.Disponible,
                Registrationdate = DateTime.UtcNow,
                TakenActions = takenAction
            };

            context.StateBooks.Add(stateBook);
            context.SaveChanges();
        }

        //metodo auxiliar 
        private void UpdateQuantity(string IsbnBook)
        {
            var bookStore = context.BookStore.FirstOrDefault(bs => bs.isbnBook == IsbnBook);
            bookStore.QuantityTotal = bookStore.QuantityTotal + 1;
            context.SaveChanges();
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
