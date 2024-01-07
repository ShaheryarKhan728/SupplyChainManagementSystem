using SCMS.Models;

namespace SCMS.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductDescription { get; set; }
        public int? Warranty { get; set; }
        public int? AccountId { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryTypeId { get; set; }
        public string? CategoryTypeName { get; set; }
        public bool? Status { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; } = new List<ProductAttribute>();

        public virtual ICollection<ProductBulk> ProductBulks { get; } = new List<ProductBulk>();
    }
}
