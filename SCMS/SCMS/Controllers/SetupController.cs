using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCMS.DTO;
using SCMS.Models;
using System.Security.Principal;

namespace SCMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetupController : ControllerBase
    {
        ScmsContext _context;
        public SetupController(ScmsContext context)
        {
            _context = context;
        }

        

        
    }
}
