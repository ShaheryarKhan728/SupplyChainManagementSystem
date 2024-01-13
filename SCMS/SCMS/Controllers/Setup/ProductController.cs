using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SCMS.DTO;
using SCMS.Models;
using SCMS.Requests;

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
        public List<ProductDto> GetAllProduct()
        {
            var result = (from o in _context.Products
                          //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                          //from a in ProductAttributes.DefaultIfEmpty()
                          //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                          //from b in ProductBulks.DefaultIfEmpty()
                          where o.Status == true
                          select new ProductDto
                          {
                              Id = o.Id,
                              ProductName = o.ProductName,
                              ProductDescription = o.ProductDescription?? "",
                              Warranty = o.Warranty,
                              CategoryId = o.CategoryId,
                              ProductAtributeId = o.ProductAtributeId,
                              AccountId = o.AccountId,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              ProductAttributes = o.ProductAttributes,
                              ProductBulks = o.ProductBulks
                          })
        .ToList();

            return result;
        }

        [HttpGet("getproductbyid")]
        public List<ProductDto> GetProductById(int id)
        {
            var result = (from o in _context.Products
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.Status == true && o.Id == id
                          select new ProductDto
                          {
                              Id = o.Id,
                              ProductName = o.ProductName,
                              ProductDescription = o.ProductDescription ?? "",
                              Warranty = o.Warranty,
                              CategoryId = o.CategoryId,
                              ProductAtributeId = o.ProductAtributeId,
                              AccountId = o.AccountId,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              ProductAttributes = o.ProductAttributes,
                              ProductBulks = o.ProductBulks
                          })
        .ToList();

            return result;
        }

        [HttpPost("saveproduct")]
        public SaveResponse SaveProduct(ProductRequest productRequest)
        {
            if (productRequest != null)
            {

                if (!productRequest.ProductAttributes.IsNullOrEmpty() && !productRequest.ProductBulks.IsNullOrEmpty())
                {
                    if (productRequest.Id == 0)
                    {
                        //Product Save
                        int productId = (_context.Products.Max(c => (int?)c.Id) ?? 0) + 1;
                        productRequest.Id = productId;
                        //Product _product = product.getProductInstance(product);

                        // Access the returned values
                        var result = productRequest.getProductInstance(productRequest);

                        Product product = result.Item1;
                        ICollection<ProductAttribute> productAttributes = result.Item2;
                        ICollection<ProductBulk> productBulks = result.Item3;

                        //productRequest.Dispose();

                        _context.Products.Add(product);

                        //Product Attribute Save
                        int productAttributesId = (_context.ProductAttributes.Max(c => (int?)c.Id) ?? 0);
                        foreach (var attribute in productAttributes)
                        {
                            attribute.ProductId = productId;
                            attribute.Id = productAttributesId + 1;
                        }
                        _context.ProductAttributes.AddRange(productAttributes);

                        //Product Bulk Save
                        int productBulksId = (_context.ProductBulks.Max(c => (int?)c.Id) ?? 0);
                        foreach (var bulk in productBulks)
                        {
                            bulk.ProductId = productId;
                            bulk.Id = productBulksId + 1;
                        }
                        _context.ProductBulks.AddRange(productBulks);

                        _context.SaveChanges();

                        return new SaveResponse
                        {
                            StatusCode = "000",
                            Message = "record saved successfully"
                        };
                    }
                    else
                    {
                        Product _product = _context.Products.Find(productRequest.Id);
                        if (_product != null)
                        {
                            _context.ProductAttributes.RemoveRange(_context.ProductAttributes.Where(c => c.ProductId == _product.Id));
                            _context.ProductBulks.RemoveRange(_context.ProductBulks.Where(c => c.ProductId == _product.Id));
                            _product.ProductName = productRequest.ProductName;
                            _product.ProductDescription = productRequest.ProductDescription;
                            _product.CategoryId = productRequest.CategoryId;
                            //_product.SubCategoryId = product.SubCategoryId;
                            _product.Warranty = productRequest.Warranty;
                            _product.ModifiedBy = productRequest.ModifiedBy;
                            _product.ModifiedOn = productRequest.ModifiedOn;

                            _context.Products.Update(_product);

                            if (_context.ProductAttributes.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product attribute"
                                };
                            }
                            _context.ProductAttributes.AddRange(productRequest.ProductAttributes);

                            if (_context.ProductBulks.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product bulk"
                                };
                            }
                            _context.ProductBulks.AddRange(productRequest.ProductBulks);

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
