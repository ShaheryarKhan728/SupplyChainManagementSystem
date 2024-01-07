using Microsoft.AspNetCore.Mvc;
using SCMS.DTO;
using SCMS.Models;

namespace SCMS.Controllers.Setup
{
    [ApiController]
    [Route("setup")]
    public class CategoryController : ControllerBase
    {
        ScmsContext _context;
        public CategoryController(ScmsContext context)
        {
            _context = context;
        }

        #region Category
        [HttpGet("getallcategory")]
        public List<CategoryDto> GetAllCategory()
        {
            var result = (from o in _context.Categories
                          join subcategory in _context.Categories on o.Id equals subcategory.CategoryHeaderId into subcategories
                          from subcategory in subcategories.DefaultIfEmpty()
                          join subcategorytype in _context.CategoryTypes on o.CategoryTypeId equals subcategorytype.Id into subcategoryTypes
                          from subcategorytype in subcategoryTypes.DefaultIfEmpty()
                          select new CategoryDto
                          {
                              Id = o.Id,
                              CategoryName = o.CategoryName,
                              CategoryMargin = o.CategoryMargin,
                              CategoryHeaderId = o.CategoryHeaderId,
                              CategoryTypeId = o.CategoryTypeId,
                              CategoryHeaderName = subcategory.CategoryName,
                              CategoryTypeName = subcategorytype.CategoryTypeName,
                              Status = o.Status
                          }).ToList();

            return result;
        }

        [HttpGet("getcategorybyid")]
        public List<CategoryDto> GetCategorybyid(int id)
        {
            var result = (from o in _context.Categories
                          join subcategory in _context.Categories on o.Id equals subcategory.CategoryHeaderId into subcategories
                          from subcategory in subcategories.DefaultIfEmpty()
                          join subcategorytype in _context.CategoryTypes on o.CategoryTypeId equals subcategorytype.Id into subcategoryTypes
                          from subcategorytype in subcategoryTypes.DefaultIfEmpty()
                          where o.Id == id
                          select new CategoryDto
                          {
                              Id = o.Id,
                              CategoryName = o.CategoryName,
                              CategoryMargin = o.CategoryMargin,
                              CategoryHeaderId = o.CategoryHeaderId,
                              CategoryTypeId = o.CategoryTypeId,
                              CategoryHeaderName = subcategory != null ? subcategory.CategoryName : null,
                              CategoryTypeName = subcategorytype != null ? subcategorytype.CategoryTypeName : null,
                              Status = (subcategorytype != null ? subcategorytype.Status : false)
                          }).ToList();

            return result;
        }

        [HttpPost("savecategory")]
        public SaveResponse SaveCategory(Category category)
        {
            if (category != null)
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
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deletecategory")]
        public SaveResponse DeleteCategory(int id)
        {
            if (id > 0)
            {
                Category category = _context.Categories.Find(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record deleted successfully"
                    };
                }
                else
                {
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid id"
            };

        }

        #endregion


        #region Category Type

        [HttpGet("getallcategorytype")]
        public List<CategoryTypeDto> GetAllCategoryType()
        {
            var result = (from o in _context.CategoryTypes
                          join a in _context.CategoryTypes on o.Id equals a.CategoryTypeHeaderId into joinedData
                          from a in joinedData.DefaultIfEmpty()
                          select new CategoryTypeDto
                          {
                              Id = o.Id,
                              CategoryTypeName = o.CategoryTypeName,
                              CategoryTypeHeaderId = o.CategoryTypeHeaderId,
                              CategoryTypeHeaderName = a.CategoryTypeName ?? null,
                          })
        .ToList();

            return result;
        }

        [HttpPost("savecategorytype")]
        public SaveResponse SaveCategoryType(CategoryType categoryType)
        {
            if (categoryType != null)
            {
                if (categoryType.Id == 0)
                {
                    int id = (_context.CategoryTypes.Max(c => (int?)c.Id) ?? 0) + 1;
                    categoryType.Id = id;
                    _context.CategoryTypes.Add(categoryType);
                    _context.SaveChanges();

                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record saved successfully"
                    };
                }
                else
                {
                    CategoryType _categoryType = _context.CategoryTypes.Find(categoryType.Id);
                    if (_categoryType != null)
                    {
                        _categoryType.Id = categoryType.Id;
                        _categoryType.CategoryTypeName = categoryType.CategoryTypeName;
                        _categoryType.CategoryTypeHeaderId = categoryType.CategoryTypeHeaderId;
                        _categoryType.ModifiedBy = categoryType.ModifiedBy;
                        _categoryType.ModifiedOn = categoryType.ModifiedOn;

                        _context.CategoryTypes.Update(_categoryType);
                        _context.SaveChanges();
                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "Record updated successfully"
                        };
                    }
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deletecategorytype")]
        public SaveResponse DeleteCategoryType(int id)
        {
            if (id > 0)
            {
                CategoryType categoryType = _context.CategoryTypes.Find(id);
                if (categoryType != null)
                {
                    _context.CategoryTypes.Remove(categoryType);
                    _context.SaveChanges();
                    return new SaveResponse
                    {
                        StatusCode = "000",
                        Message = "record deleted successfully"
                    };
                }
                else
                {
                    return new SaveResponse
                    {
                        StatusCode = "001",
                        Message = "record not found"
                    };
                }
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid id"
            };

        }

        #endregion Category Type
    }
}
