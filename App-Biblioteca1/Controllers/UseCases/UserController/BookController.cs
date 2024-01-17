using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models.Enums;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.UseCases.UserController
{
    [ApiController]
    [Route("api/UserController")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public BookController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOneBook")]
        public async Task<ActionResult> AddBook(BooksDTO bookDTO)
        {
            if (string.IsNullOrEmpty(bookDTO.Title)
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
                newBook.Delete = 0;

            }
            else
            {
                newBook.StoreId = existingStore.Id;
                newBook.Delete = 0;


            }

            CreateStateBook(newBook, $"Libro creado: {newBook.Title} con ISBN {newBook.ISBN} creado" +
                $"y cargado al inventario de la Biblioteca");
            UpdateQuantity(newBook.ISBN);

            context.Books.Add(newBook);
            await context.SaveChangesAsync();

            return Ok();

        }

        //metodo auxiliar 1
        private void CreateStateBook(Books book, string takenAction)
        {
            var stateBook = new StateBook
            {
                State = Models.Enums.StatesOfBooks.Disponible,
                Registrationdate = DateTime.UtcNow,
                TakenActions = takenAction,
                Books = book,
            };

            context.StateBooks.Add(stateBook);
            context.SaveChanges();
        }

        //metodo auxiliar 2
        private void UpdateQuantity(string IsbnBook)
        {
            var bookStore = context.BookStore.FirstOrDefault(bs => bs.isbnBook == IsbnBook);
            bookStore.QuantityTotal = bookStore.QuantityTotal + 1;
            context.SaveChanges();
        }


        //PUT Gestionar estado del libro
        [HttpPut("/softDelete{bookId}")]
        public async Task<ActionResult> SoftDeleteBook(Guid bookId, StatesOfBooks states, string message)
        {
            if (bookId == Guid.Empty)
            {
                return BadRequest("El Id del libro no es valido");
            }

            var book = await context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound($"No se encontro el libro con Id {bookId}");
            }

            book.Delete = 1;
            await context.SaveChangesAsync();

            await UpdateStoreBookQuantity(book);
            await CreateStateBookRecord(book, states, $"El Libro {book.Title} con ISBN {book.ISBN}, Cambio de estado a {states}: " + message);

            return Ok($"Estado del Libro Ha Cambiado a {states}");
        }

        private async Task UpdateStoreBookQuantity(Books book)
        {
            var storeId = book.StoreId;

            var storeBook = await context.BookStore
                    .FirstOrDefaultAsync(sb => sb.Id == storeId);

            if (storeBook != null)
            {
                storeBook.QuantityTotal -= 1;
                await context.SaveChangesAsync();
            }
        }
        private async Task CreateStateBookRecord(Books book, StatesOfBooks state, string message)
        {
            var stateBook = new StateBook
            {
                State = state,
                Registrationdate = DateTime.UtcNow,
                TakenActions = message,
                Books = book,
            };
            context.StateBooks.Add(stateBook);
            await context.SaveChangesAsync();
        }
    }
}
