using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.UseCases.SuperUser
{
    [ApiController]
    [Route("api/StateBookCRUD")]
    public class StateBookCRUD : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public StateBookCRUD(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        //GET:api/BooksCRUD/getAllBooks
        [HttpGet("/getStateBooksCRUD")]
        public async Task<IEnumerable<StateBookDTO>> GetStateBookCRUD()
        {
            var StateBook = await context.StateBooks.ToListAsync();
            var StateBookDTO = mapper.Map<IEnumerable<StateBookDTO>>(StateBook);
            return StateBookDTO;
        }


        //GET:api/BooksCRUD/getAllBooks/{guidBook}
        [HttpGet("/getOneStateBookCRUD/{id}")]
        public IActionResult GetOneStateBookCRUD(int id)
        {
            var StateBook = context.StateBooks.FirstOrDefault(b => b.Id == id);

            if (StateBook is null)
            {
                return NotFound();
            }
            return StateBook != null ? Ok(mapper.Map<StateBookDTO>(StateBook)) : NotFound();
        }

        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOneStateBookCRUD")]
        public async Task<ActionResult> AddOneStateBookCRUD([FromBody] StateBookDTO StateBookDTO)
        {
            var StateBook = mapper.Map<StateBook>(StateBookDTO);
            context.Add(StateBook);
            await context.SaveChangesAsync();
            return Ok(StateBook);
        }

        //POST:api/BooksCRUD/addVarious
        [HttpPost("/addVariousStateBookCRUD")]
        public async Task<ActionResult> AddVariousStateBookCRUD([FromBody] IEnumerable<StateBookDTO> StateBookDTO)
        {
            var StateBook = mapper.Map<IEnumerable<StateBook>>(StateBookDTO);
            context.AddRange(StateBook);
            await context.SaveChangesAsync();
            return Ok(StateBook);
        }

        //PUT:api/BooksCRUD/updateBook/{guidBook}
        [HttpPut("/updateStateBookCRUD/{id}")]
        public async Task<IActionResult> UpdateStateBookCRUD(int id, [FromBody] StateBookDTO StateBookDTO)
        {
            var StateBook = await context.StateBooks.FirstOrDefaultAsync(b => b.Id == id);
            return StateBook != null ? Ok(mapper.Map<StateBookDTO>(StateBook)) : NotFound();
        }


        //GET:api/BooksCRUD/searchBy/{property}/{value}
        [HttpGet("/searchByStateBookCRUD/{property}/{value}")]
        public IActionResult SearchByStateBookCRUD(string property, string value)
        {
            var StateBook = GetByStateBookProperty(property, value);

            if (StateBook is null)
            {
                return NotFound();
            }
            var StateBookDTO = mapper.Map<StateBookDTO>(StateBook);
            return Ok(StateBookDTO);

        }

        //DELETE:api/BooksCRUD/deleteBook/{guidBook}
        [HttpDelete("/deleteStateBookCRUD/{id}")]
        public async Task<ActionResult> DeleteStateBookCRUD(int id)
        {
            var StateBook = await context.StateBooks.FirstOrDefaultAsync(b => b.Id == id);
            if (StateBook is null)

            {
                return NotFound();
            }
            context.Remove(StateBook);
            await context.SaveChangesAsync();
            return Ok();
        }


        //GetBookByProperty(property, value);
        private StateBook GetByStateBookProperty(string property, string value)
        {
            switch (property.ToLower())
            {
                case "State":
                    return context.StateBooks.FirstOrDefault(b => b.State.ToString() == value);
                case "Registrationdate":
                    return context.StateBooks.FirstOrDefault(b => b.Registrationdate == DateTime.Parse(value));
                case "TakenActions":
                    return context.StateBooks.FirstOrDefault(b => b.TakenActions == value);
   
                case "User":
                    var UserStateBook = context.StateBooks
                        .Include(b => b.User)
                        .Where(b => b.User != null)
                        .ToList();
                    var filteredUserStateBook = UserStateBook.FirstOrDefault(b => b.User.Id == Guid.Parse(value));

                    return filteredUserStateBook;

                case "Books":
                    var BooksState = context.StateBooks
                        .Include(b => b.Books)
                        .Where(b => b.Books != null)
                        .ToList();
                    var filteredBook = BooksState.FirstOrDefault(b => b.Books.Id == Guid.Parse(value));

                    return filteredBook;

                default:
                    return null;
            }
        }
    }
}
