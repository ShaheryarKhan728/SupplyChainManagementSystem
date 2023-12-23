using Microsoft.AspNetCore.Mvc;
using SCMS.Models;

namespace SCMS.Controllers
{
    [ApiController]
    [Route("[setup]")]
    public class SetupController : ControllerBase
    {
        ScmsContext _context;
        public SetupController(ScmsContext context)
        {
            _context = context;
        }
        [HttpGet(Name = "getallcategory")]
        public List<Category> Get()
        {
            return (from o in _context.Categories select o
            ).ToList();
        }
    }
}
