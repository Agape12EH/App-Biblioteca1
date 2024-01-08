using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App_Biblioteca1.Controllers.CRUD
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
    }
}
