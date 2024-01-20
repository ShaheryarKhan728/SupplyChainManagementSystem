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
                          where o.Status == true && o.Approve == 1
                          select new ProductDto
                          {
                              Id = o.Id,
                              ProductName = o.ProductName,
                              ProductDescription = o.ProductDescription?? "",
                              Warranty = o.Warranty,
                              MinOrderValue = o.MinOrderValue,
                              CategoryId = o.CategoryId,
                              ProductAtributeId = o.ProductAtributeId,
                              AccountId = o.AccountId,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              ColorAttributes = o.ColorAttributes,
                              SizeAttributes = o.SizeAttributes,
                              WeightAttributes = o.WeightAttributes,
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
                          where o.Status == true && o.Id == id && o.Approve == 1
                          select new ProductDto
                          {
                              Id = o.Id,
                              ProductName = o.ProductName,
                              ProductDescription = o.ProductDescription ?? "",
                              Warranty = o.Warranty,
                              MinOrderValue = o.MinOrderValue,
                              CategoryId = o.CategoryId,
                              ProductAtributeId = o.ProductAtributeId,
                              AccountId = o.AccountId,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              ColorAttributes = o.ColorAttributes,
                              SizeAttributes = o.SizeAttributes,
                              WeightAttributes = o.WeightAttributes,
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

                if (!productRequest.ColorAttributes.IsNullOrEmpty() && !productRequest.ProductBulks.IsNullOrEmpty() && !productRequest.SizeAttributes.IsNullOrEmpty() && !productRequest.WeightAttributes.IsNullOrEmpty())
                {
                    if (productRequest.Id == 0)
                    {
                        //Product Save
                        int productId = (_context.Products.Max(c => (int?)c.Id) ?? 0) + 1;
                        productRequest.Id = productId;
                        productRequest.Approve = 0;
                        //Product _product = product.getProductInstance(product);

                        // Access the returned values
                        var result = productRequest.getProductInstance(productRequest);

                        Product product = result.Item1;
                        //ICollection<ProductAttribute> productAttributes = result.Item2;
                        ICollection<ColorAttribute> colorAttributes = result.Item2;
                        ICollection<WeightAttribute> weightAttributes = result.Item3;
                        ICollection<SizeAttribute> sizeAttributes = result.Item4;
                        ICollection<ProductBulk> productBulks = result.Item5;

                        //productRequest.Dispose();

                        _context.Products.Add(product);

                        ////Product Attribute Save
                        //int productAttributesId = (_context.ProductAttributes.Max(c => (int?)c.Id) ?? 0);
                        //foreach (var attribute in productAttributes)
                        //{
                        //    attribute.ProductId = productId;
                        //    attribute.Id = productAttributesId + 1;
                        //}
                        //_context.ProductAttributes.AddRange(productAttributes);

                        //Color Attribute Save
                        int colorAttributesId = (_context.ColorAttributes.Max(c => (int?)c.Id) ?? 0);
                        foreach (var color in colorAttributes)
                        {
                            color.ProductId = productId;
                            color.Id = colorAttributesId + 1;
                        }
                        _context.ColorAttributes.AddRange(colorAttributes);

                        //Size Attribute Save
                        int sizeAttributesId = (_context.SizeAttributes.Max(c => (int?)c.Id) ?? 0);
                        foreach (var size in sizeAttributes)
                        {
                            size.ProductId = productId;
                            size.Id = sizeAttributesId + 1;
                        }
                        _context.SizeAttributes.AddRange(sizeAttributes);

                        //Weight Attribute Save
                        int weightAttributesId = (_context.WeightAttributes.Max(c => (int?)c.Id) ?? 0);
                        foreach (var weight in weightAttributes)
                        {
                            weight.ProductId = productId;
                            weight.Id = weightAttributesId + 1;
                        }
                        _context.WeightAttributes.AddRange(weightAttributes);

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
                            _product.MinOrderValue = productRequest.MinOrderValue;
                            _product.ModifiedBy = productRequest.ModifiedBy;
                            _product.ModifiedOn = productRequest.ModifiedOn;
                            _product.Approve = 0;

                            _context.Products.Update(_product);

                            if (_context.ColorAttributes.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product colors"
                                };
                            }
                            _context.ColorAttributes.AddRange(productRequest.ColorAttributes);

                            if (_context.SizeAttributes.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product sizes"
                                };
                            }
                            _context.SizeAttributes.AddRange(productRequest.SizeAttributes);

                            if (_context.WeightAttributes.IsNullOrEmpty())
                            {
                                return new SaveResponse
                                {
                                    StatusCode = "002",
                                    Message = "invalid product weights"
                                };
                            }
                            _context.WeightAttributes.AddRange(productRequest.WeightAttributes);

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
                    if (product.ColorAttributes.IsNullOrEmpty() || product.ProductBulks.IsNullOrEmpty() || product.SizeAttributes.IsNullOrEmpty() || product.WeightAttributes.IsNullOrEmpty())
                    {
                        return new SaveResponse
                        {
                            StatusCode = "002",
                            Message = "invalid product attributes or product bulk"
                        };
                    }
                    //_context.ProductAttributes.RemoveRange(_context.ProductAttributes.Where(c => c.ProductId == product.Id));
                    _context.ColorAttributes.RemoveRange(_context.ColorAttributes.Where(c => c.ProductId == product.Id));
                    _context.SizeAttributes.RemoveRange(_context.SizeAttributes.Where(c => c.ProductId == product.Id));
                    _context.WeightAttributes.RemoveRange(_context.WeightAttributes.Where(c => c.ProductId == product.Id));
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

        [HttpGet("getproductbyseller")]
        public List<ProductDto> GetProductBySeller(int id)
        {
            var result = (from o in _context.Products
                              //join a in _context.ProductAttributes on o.Id equals a.ProductId into ProductAttributes
                              //from a in ProductAttributes.DefaultIfEmpty()
                              //join b in _context.ProductBulks on o.Id equals b.ProductId into ProductBulks
                              //from b in ProductBulks.DefaultIfEmpty()
                          where o.Status == true && o.AccountId == id && o.Approve == 1
                          select new ProductDto
                          {
                              Id = o.Id,
                              ProductName = o.ProductName,
                              ProductDescription = o.ProductDescription ?? "",
                              Warranty = o.Warranty,
                              CategoryId = o.CategoryId,
                              MinOrderValue = o.MinOrderValue,
                              ProductAtributeId = o.ProductAtributeId,
                              AccountId = o.AccountId,
                              CreatedOn = o.CreatedOn,
                              CreatedBy = o.CreatedBy,
                              ColorAttributes = o.ColorAttributes,
                              SizeAttributes = o.SizeAttributes,
                              WeightAttributes = o.WeightAttributes,
                              ProductBulks = o.ProductBulks
                          })
            .ToList();

            return result;
        }
    }
}
