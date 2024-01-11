using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.CRUD
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

        //GET:api/BooksCRUD/getAllBooks
        //[HttpGet("/BookStore")]
        //public ActionResult<IEnumerable<BookStore>> GetBookStore()
        //{
        //    var bookStores = context.BookStore.ToList();

        //    var bookStoreDTO = bookStores.Select(bookStore => new BookStore
        //    {
        //        Id = bookStore.Id,
        //        DateStored = bookStore.DateStored,
        //        QuantityTotal = bookStore.QuantityTotal,
        //        isbnBook = bookStore.isbnBook,
        //        Books = bookStore.Books.Select(book => new Books 
        //        { 
        //            Id = book.Id,
        //            Title = book.Title,
        //            Author = book.Author,
        //            AgePublication = book.AgePublication,
        //        }).ToList()
            
        //    }).ToList();

        //    return Ok(bookStoreDTO);
        //}

    }


       
}