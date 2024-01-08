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
        [HttpGet("/BookStore")]
        public async Task<IEnumerable<InventoryDTO>> GetAllBookStore() =>
             mapper.Map<IEnumerable<InventoryDTO>>(await context.BookStore.ToListAsync());

    }


       
}