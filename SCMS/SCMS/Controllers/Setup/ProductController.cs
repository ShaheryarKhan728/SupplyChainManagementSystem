using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCMS.DTO;
using SCMS.Models;

namespace SCMS.Controllers.Setup
{
    [ApiController]
    [Route("setup")]
    public class ProductController : ControllerBase
    {
        ScmsContext _context;
        public ProductController(ScmsContext context)
        {
            _context = context;
        }

        [HttpGet("getallproduct")]
        public List<CategoryTypeDto> GetAllProduct()
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

        [HttpPost("saveproduct")]
        public SaveResponse SaveProduct(Product product)
        {
            if (product != null)
            {
                if (!product.ProductAttributes.IsNullOrEmpty() && !product.ProductBulks.IsNullOrEmpty())
                {
                    if (product.Id == 0)
                    {
                        //Product Save
                        int productId = (_context.Products.Max(c => (int?)c.Id) ?? 0) + 1;
                        product.Id = productId;
                        _context.Products.Add(product);

                        //Product Attribute Save
                        int productAttributesId = (_context.ProductAttributes.Max(c => (int?)c.Id) ?? 0);
                        foreach (var attribute in product.ProductAttributes)
                        {
                            attribute.ProductId = productId;
                            attribute.Id = productAttributesId + 1;
                        }
                        _context.ProductAttributes.AddRange(product.ProductAttributes);

                        //Product Bulk Save
                        int productBulksId = (_context.ProductBulks.Max(c => (int?)c.Id) ?? 0);
                        foreach (var bulk in product.ProductBulks)
                        {
                            bulk.ProductId = productId;
                            bulk.Id = productBulksId + 1;
                        }
                        _context.ProductBulks.AddRange(product.ProductBulks);

                        _context.SaveChanges();

                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "record saved successfully"
                        };
                    }
                    else
                    {
                        Product _product = _context.Products.Find(product.Id);
                        if (_product != null)
                        {
                            _context.ProductAttributes.RemoveRange(_context.ProductAttributes.Where(c => c.ProductId == _product.Id));
                            _context.ProductBulks.RemoveRange(_context.ProductBulks.Where(c => c.ProductId == _product.Id));
                            _product.ProductName = product.ProductName;
                            _product.ProductDescription = product.ProductDescription;
                            _product.CategoryId = product.CategoryId;
                            //_product.SubCategoryId = product.SubCategoryId;
                            _product.Warranty = product.Warranty;
                            _product.ModifiedBy = product.ModifiedBy;
                            _product.ModifiedOn = product.ModifiedOn;

                            _context.Products.Update(_product);

                            if (_context.ProductAttributes.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product attribute"
                                };
                            }
                            _context.ProductAttributes.AddRange(product.ProductAttributes);

                            if (_context.ProductBulks.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product bulk"
                                };
                            }
                            _context.ProductBulks.AddRange(product.ProductBulks);

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
                    Message = "invalid product attribute or product bulk"
                };
            }
            return new SaveResponse
            {
                StatusCode = "002",
                Message = "invalid data"
            };

        }

        [HttpPost("deleteproduct")]
        public SaveResponse DeleteProduct(int id)
        {
            if (id > 0)
            {
                Product product = _context.Products.Find(id);
                if (product != null)
                {
                    if (product.ProductAttributes.IsNullOrEmpty() || product.ProductBulks.IsNullOrEmpty())
                    {
                        return new SaveResponse
                        {
                            StatusCode = "002",
                            Message = "invalid product attribute or product bulk"
                        };
                    }
                    _context.ProductAttributes.RemoveRange(_context.ProductAttributes.Where(c => c.ProductId == product.Id));
                    _context.ProductBulks.RemoveRange(_context.ProductBulks.Where(c => c.ProductId == product.Id));
                    _context.Products.Remove(product);
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
    }
}
