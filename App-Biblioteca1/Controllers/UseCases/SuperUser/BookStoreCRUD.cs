using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.UseCases.SuperUser
{
    [ApiController]
    [Route("api/BookStoreCRUD")]
    public class BookStoreCRUD : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BookStoreCRUD(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        //GET:api/BooksCRUD/getBooksStoreCRUD
        [HttpGet("/getBooksStoreCRUD")]
        public async Task<IEnumerable<InventoryDTO>> GetBooksStoreCRUD()
        {
            var BookStore = await context.BookStore.ToListAsync();
            var BookStoreDTO = mapper.Map<IEnumerable<InventoryDTO>>(BookStore);
            return BookStoreDTO;
        }


        //GET:api/BooksCRUD//getOneBookStoreCRUD/{id}
        [HttpGet("/getOneBookStoreCRUD/{id}")]
        public IActionResult GetOneBookStoreCRUD(int id)
        {
            var BookStore = context.BookStore.FirstOrDefault(b => b.Id == id);

            if (BookStore is null)
            {
                return NotFound();
            }

            return BookStore != null ? Ok(mapper.Map<InventoryDTO>(BookStore)) : NotFound();
        }

        //POST:api/BooksCRUD/addOneBookStoreCRUD
        [HttpPost("/addOneBookStoreCRUD")]
        public async Task<ActionResult> AddOneBookStoreCRUD([FromBody] InventoryDTO BookStoreDTO)
        {
            var BookStore = mapper.Map<BookStore>(BookStoreDTO);
            context.Add(BookStore);
            await context.SaveChangesAsync();
            return Ok(BookStore);
        }

        //POST:api/BooksCRUD/addVariousBookStoreCRUD
        [HttpPost("/addVariousBookStoreCRUD")]
        public async Task<ActionResult> addVariousBookStoreCRUD([FromBody] IEnumerable<InventoryDTO> BookStoreDTO)
        {
            var BookStore = mapper.Map<IEnumerable<BookStore>>(BookStoreDTO);
            context.AddRange(BookStore);
            await context.SaveChangesAsync();
            return Ok(BookStore);
        }

        //PUT:api/BooksCRUD/updateBookStoreCRUD/{id}
        [HttpPut("/updateBookStoreCRUD/{id}")]
        public async Task<IActionResult> updateBookStoreCRUD(int id, [FromBody] InventoryDTO BookStoreDTO)
        {
            var BookStore = await context.BookStore.FirstOrDefaultAsync(b => b.Id == id);
            return BookStore != null ? Ok(mapper.Map<InventoryDTO>(BookStore)) : NotFound();
        }


        //GET:api/BooksCRUD/searchBy/{property}/{value}
        [HttpGet("/searchBookStoreByCRUD/{property}/{value}")]
        public IActionResult searchBookStoreByCRUD(string property, string value)
        {
            var BookStore = GetBookStoreByProperty(property, value);

            if (BookStore is null)
            {
                return NotFound();
            }
            var BookStoreDTO = mapper.Map<InventoryDTO>(BookStore);
            return Ok(BookStoreDTO);

        }

        //DELETE:api/BooksCRUD/deleteBookStoreCRUD/{id}
        [HttpDelete("/deleteBookStoreCRUD/{id}")]
        public async Task<ActionResult> deleteBookStoreCRUD(int id)
        {
            var BookStore = await context.BookStore.FirstOrDefaultAsync(b => b.Id == id);
            if (BookStore is null)

            {
                return NotFound();
            }

            context.Remove(BookStore);
            await context.SaveChangesAsync();
            return Ok();
        }


        //GetBookByProperty(property, value);
        private BookStore GetBookStoreByProperty(string property, string value)
        {
            switch (property.ToLower())
            {
                case "datestored":
                    return context.BookStore.FirstOrDefault(b=>b.DateStored == DateTime.Parse(value));
                case "quantitytotal":
                    return context.BookStore.FirstOrDefault(b => b.QuantityTotal == int.Parse(value));
                case "books":
                    var bookList = context.BookStore
                        .Include(b => b.Books)
                        .Where(b => b.Books.Any())
                        .ToList();
                    var filteredBooks = bookList.Select(b => new BookStore
                    {
                        Id = b.Id,
                        DateStored = b.DateStored,
                        //for inventary of books
                        Books = b.Books.Where(book => book.Id == Guid.Parse(value)).ToHashSet(),
                    }).FirstOrDefault();

                    return filteredBooks;

                default:
                    return null;
            }
        }
    }
}