using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App_Biblioteca1.Controllers
{
    [ApiController]
    [Route("api/BookUpdate")]
    public class BookUpdate : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public BookUpdate(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //PUT Modificacion de un Libro 
        [HttpPut("ModifyBook/{bookId}")]
        public async Task<ActionResult> UpdateBook(Guid bookId, [FromBody] BooksDTO updatedBookDTO)
        {
            var book = context.Books.Find(bookId);
            if (book == null)
            {
                return NotFound($"No se encontro el libro con ese Id");
            }

            var originalBookDTO = mapper.Map<BooksDTO>(book);

            if (!string.IsNullOrEmpty(updatedBookDTO.Title))
            {
                book.Title = updatedBookDTO.Title;
            }
            if (!string.IsNullOrEmpty(updatedBookDTO.Author))
            {
                book.Author = updatedBookDTO.Author;
            }
            if (!string.IsNullOrEmpty(updatedBookDTO.Gender))
            {
                book.Gender = updatedBookDTO.Gender;
            }
            if (!string.IsNullOrEmpty(updatedBookDTO.ISBN))
            {
                book.ISBN = updatedBookDTO.ISBN;
            }
            if ((updatedBookDTO.AgePublication).HasValue)
            {
                book.AgePublication = updatedBookDTO.AgePublication;
            }
            await context.SaveChangesAsync();
            await UpdateStateBook(book, $"Libro creado: {book.Title} con ISBN {book.ISBN}  ha sido creado" +
                $"y cargado al inventario de la Biblioteca");
            return Ok(new { originalBook = originalBookDTO, UpdateBook = updatedBookDTO });
        }

        //metodo auxiliar 
        private async Task<ActionResult>
        UpdateStateBook(Books book, string message)
        {
            var stateBook = context.StateBooks.First(bs => bs.Books.Id == book.Id);


            if (stateBook == null)
            {
                return NotFound($"No se encontro el libro en el estado de los libros");
            }

            stateBook.TakenActions = message;


            await context.SaveChangesAsync();
            return Ok("Campo del libro actualizado en el estado de libros");
        }
    }
}
