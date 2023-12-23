using Microsoft.AspNetCore.Mvc;
using SCMS.DTO;
using SCMS.Models;

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

        #region Category
        [HttpGet("getallcategory")]
        public List<CategoryDto> GetAllCategory()
        {
            var result = (from o in _context.Categories
                    join a in _context.Categories on o.Id equals a.CategoryHeaderId
                    join b in _context.CategoryTypes on o.CategoryTypeId equals b.Id
                    select new CategoryDto
                    {
            Id = o.Id,
            CategoryName = o.CategoryName,
            CategoryMargin = o.CategoryMargin,
            CategoryHeaderId = o.CategoryHeaderId,
            CategoryTypeId = o.CategoryTypeId,
            CategoryHeaderName = a.CategoryName,
            CategoryTypeName = b.CategoryTypeName
        })
        .ToList();

            return result;
        }

        [HttpPost("savecategory")]
        public SaveResponse SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                int id = (_context.Categories.Max(c => (int?)c.Id) ?? 0) + 1;
                category.Id = id;
                _context.Categories.Add(category);
                _context.SaveChanges();

                return new SaveResponse
                {
                    Message = "record saved successfully"
                };
            }
            else
            {
                Category _category = _context.Categories.Find(category.Id);
                if (_category != null)
                {
                    _category.Id = category.Id;
                    _category.CategoryName = category.CategoryName;
                    _category.CategoryMargin = category.CategoryMargin;
                    _category.CategoryHeaderId = category.CategoryHeaderId;
                    _category.CategoryTypeId = category.CategoryTypeId;
                    _category.Status = category.Status;
                    _category.ModifiedBy = category.ModifiedBy;
                    _category.ModifiedOn = category.ModifiedOn;

                    _context.Categories.Update(_category);
                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        Message = "Record updated successfully"
                    };
                }
                return new SaveResponse
                {
                    Message = "Unable to update record"
                };
            }

            
        }

        #endregion Category
    }
}
