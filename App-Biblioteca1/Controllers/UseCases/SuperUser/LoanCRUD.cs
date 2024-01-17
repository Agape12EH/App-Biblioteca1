using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Biblioteca1.Controllers.UseCases.SuperUser
{
    [ApiController]
    [Route("api/LoanCRUD")]
    public class LoanCRUD : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LoanCRUD(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        //GET:api/BooksCRUD/getAllBooks
        [HttpGet("/getLoanCRUD")]
        public async Task<IEnumerable<LoanDTO>> GetLoanCRUD()
        {
            var loans = await context.Books.ToListAsync();
            var loansDTO = mapper.Map<IEnumerable<LoanDTO>>(loans);
            return loansDTO;
        }


        //GET:api/BooksCRUD/getAllBooks/{guidBook}
        [HttpGet("/getOneLoanCRUD/{guidBook}")]
        public IActionResult GetOneLoanCRUD(Guid guidLoan)
        {
            var Loan = context.Loans.FirstOrDefault(b => b.Id == guidLoan);

            if (Loan is null)
            {
                return NotFound();
            }

            return Loan != null ? Ok(mapper.Map<LoanDTO>(Loan)) : NotFound();
        }

        //POST:api/BooksCRUD/addOne
        [HttpPost("/addOneLoanCRUD")]
        public async Task<ActionResult> AddOneLoanCRUD([FromBody] LoanDTO LoanDTO)
        {
            var Loan = mapper.Map<Loan>(LoanDTO);
            context.Add(Loan);
            await context.SaveChangesAsync();
            return Ok(Loan);
        }

        //POST:api/BooksCRUD/addVarious
        [HttpPost("/addVariousLoanCRUD")]
        public async Task<ActionResult> AddVariousLoanCRUD([FromBody] IEnumerable<LoanDTO> LoanDTO)
        {
            var Loan = mapper.Map<IEnumerable<Loan>>(LoanDTO);
            context.AddRange(Loan);
            await context.SaveChangesAsync();
            return Ok(Loan);
        }

        //PUT:api/BooksCRUD/updateBook/{guidBook}
        [HttpPut("/updateLoanCRUD/{guidLoan}")]
        public async Task<IActionResult> UpdateLoanCRUD(Guid guidLoan, [FromBody] LoanDTO LoanDTO)
        {
            var Loan = await context.Loans.FirstOrDefaultAsync(b => b.Id == guidLoan);
            return Loan != null ? Ok(mapper.Map<LoanDTO>(Loan)) : NotFound();
        }


        //GET:api/BooksCRUD/searchBy/{property}/{value}
        [HttpGet("/searchByLoanCRUD/{property}/{value}")]
        public IActionResult SearchByLoanCRUD(string property, string value)
        {
            var Loan = GetLoanByProperty(property, value);

            if (Loan is null)
            {
                return NotFound();
            }
            var LoanDTO = mapper.Map<BooksDTO>(Loan);
            return Ok(LoanDTO);

        }

        //DELETE:api/BooksCRUD/deleteBook/{guidBook}
        [HttpDelete("/deleteLoanCRUD/{guidLoan}")]
        public async Task<ActionResult> DeleteLoanCRUD(Guid guidLoan)
        {
            var Loan = await context.Loans.FirstOrDefaultAsync(b => b.Id == guidLoan);
            if (Loan is null)

            {
                return NotFound();
            }

            context.Remove(Loan);
            await context.SaveChangesAsync();
            return Ok();
        }


        //GetBookByProperty(property, value);
        private Loan GetLoanByProperty(string property, string value)
        {
            switch (property.ToLower())
            {
                case "LoanDate":
                    return context.Loans.FirstOrDefault(b => b.LoanDate == DateTime.Parse(value));
                case "ExpectedReturnDate":
                    return context.Loans.FirstOrDefault(b => b.ExpectedReturnDate == DateTime.Parse(value));
                case "CurrentReturnDate":
                    return context.Loans.FirstOrDefault(b => b.CurrentReturnDate == DateTime.Parse(value));
                case "LoanState":
                    return context.Loans.FirstOrDefault(b => b.LoanState.ToString() == value);
                case "books":
                    var LoansList = context.Loans
                        .Include(b => b.Books)
                        .Where(b => b.Books.Any())
                        .ToList();
                    var filteredLoan = LoansList.Select(b => new Loan
                    {
                        Id = b.Id,
                        ExpectedReturnDate = b.ExpectedReturnDate,
                        CurrentReturnDate = b.CurrentReturnDate,
                        LoanState = b.LoanState,
                        //for inventary of books
                        Books = b.Books.Where(book => book.Id == Guid.Parse(value)).ToHashSet(),
                    }).FirstOrDefault();

                    return filteredLoan;

                case "User":
                    var UserLoan = context.Loans
                        .Include(b => b.User)
                        .Where(b => b.User != null)
                        .ToList();
                    var filteredUser = UserLoan.FirstOrDefault(b => b.User.Id == Guid.Parse(value));
                    
                    return filteredUser;

                default:
                    return null;
            }
        }
    }
}
