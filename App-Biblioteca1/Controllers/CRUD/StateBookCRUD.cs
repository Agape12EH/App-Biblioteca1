using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App_Biblioteca1.Controllers.CRUD
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
    }
}
