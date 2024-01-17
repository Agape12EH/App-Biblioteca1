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
        [HttpGet("/getUserCRUD")]
        public async Task<IEnumerable<UserDTO>> GetCRUD()
        {
            var Users = await context.Users.ToListAsync();
            var UserDTO = mapper.Map<IEnumerable<UserDTO>>(Users);
            return UserDTO;
        }


        //GET:api/BooksCRUD/getAllBooks/{guidBook}
        [HttpGet("/getOneUserCRUD/{guidUser}")]
        public IActionResult GetOneUserCRUD(Guid guidUser)
        {
            var User = context.Users.FirstOrDefault(b => b.Id == guidUser);

            if (User is null)
            {
                return NotFound();
            }
            return User != null ? Ok(mapper.Map<UserDTO>(User)) : NotFound();
        }

        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOneUserCRUD")]
        public async Task<ActionResult> AddOneUserCRUD([FromBody] UserDTO UserDTO)
        {
            var User = mapper.Map<User>(UserDTO);
            context.Add(User);
            await context.SaveChangesAsync();
            return Ok(User);
        }

        //POST:api/BooksCRUD/addVarious
        [HttpPost("/addVariousUserCRUD")]
        public async Task<ActionResult> AddVariousUserCRUD([FromBody] IEnumerable<UserDTO> UserDTO)
        {
            var User = mapper.Map<IEnumerable<User>>(UserDTO);
            context.AddRange(User);
            await context.SaveChangesAsync();
            return Ok(User);
        }

        //PUT:api/BooksCRUD/updateBook/{guidBook}
        [HttpPut("/updateUserCRUD/{guidUser}")]
        public async Task<IActionResult> UpdateUserCRUD(Guid guidUser, [FromBody] UserDTO UserDTO)
        {
            var User = await context.Users.FirstOrDefaultAsync(b => b.Id == guidUser);
            return User != null ? Ok(mapper.Map<UserDTO>(User)) : NotFound();
        }


        //GET:api/BooksCRUD/searchBy/{property}/{value}
        [HttpGet("/searchByUserCRUD/{property}/{value}")]
        public IActionResult SearchByUserCRUD(string property, string value)
        {
            var User = GetUserByProperty(property, value);

            if (User is null)
            {
                return NotFound();
            }
            var UserDTO = mapper.Map<UserDTO>(User);
            return Ok(UserDTO);

        }

        //DELETE:api/BooksCRUD/deleteBook/{guidBook}
        [HttpDelete("/deleteUserCRUD/{guidUser}")]
        public async Task<ActionResult> DeleteUserCRUD(Guid guidUser)
        {
            var User = await context.Users.FirstOrDefaultAsync(b => b.Id == guidUser);
            if (User is null)

            {
                return NotFound();
            }

            context.Remove(User);
            await context.SaveChangesAsync();
            return Ok();
        }


        //GetBookByProperty(property, value);
        private User GetUserByProperty(string property, string value)
        {
            switch (property.ToLower())
            {
                case "Name":
                    return context.Users.FirstOrDefault(b => b.Name == value);
                case "Lastname":
                    return context.Users.FirstOrDefault(b => b.Lastname == value); 
                case "Email":
                    return context.Users.FirstOrDefault(b => b.Email == value);
                case "Loans":
                    var LoansList = context.Users
                        .Include(b => b.Loans)
                        .Where(b => b.Loans.Any())
                        .ToList();
                    var filteredLoans = LoansList.Select(b => new User
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Lastname = b.Lastname,
                        Email = b.Email,
                        //for inventary of books
                        Loans = b.Loans.Where(loan => loan.Id == Guid.Parse(value)).ToHashSet(),
                    }).FirstOrDefault();

                    return filteredLoans;

                case "StateBooks":
                    var StateBookList = context.Users
                        .Include(b => b.StateBooks)
                        .Where(b => b.StateBooks.Any())
                        .ToList();
                    var filteredStateBook = StateBookList.Select(b => new User
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Lastname = b.Lastname,
                        Email = b.Email,
                        //for inventary of books
                        StateBooks = (HashSet<StateBook>)b.StateBooks.Where(statebook => statebook.State.ToString() == value),
                    }).FirstOrDefault();

                    return filteredStateBook;

                default:
                    return null;
            }
        }
    }
}
