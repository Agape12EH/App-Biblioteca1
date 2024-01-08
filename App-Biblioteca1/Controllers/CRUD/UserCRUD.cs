using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App_Biblioteca1.Controllers.CRUD
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
    }
}
