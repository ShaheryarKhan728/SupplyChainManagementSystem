using SCMS.Models;

namespace SCMS.Requests
{
    public class ProductRequest : IDisposable
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public int? CategoryId { get; set; }

        public bool? Status { get; set; }

        public string? Warranty { get; set; }

        public int? MinOrderValue { get; set; }

        public int? ProductAtributeId { get; set; }

        public int? AccountId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? Approve { get; set; }

        //public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();

        public virtual ICollection<ColorAttribute> ColorAttributes { get; set; } = new List<ColorAttribute>();

        public virtual ICollection<SizeAttribute> SizeAttributes { get; set; } = new List<SizeAttribute>();

        public virtual ICollection<WeightAttribute> WeightAttributes { get; set; } = new List<WeightAttribute>();

        public virtual ICollection<ProductBulk> ProductBulks { get; set; } = new List<ProductBulk>();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    //ProductAttributes?.Clear();
                    //ProductBulks?.Clear();
                }

                // Dispose unmanaged resources

                disposed = true;
            }
        }
        ~ProductRequest()
        {
            Dispose(false);
        }

        public (Product, ICollection<ColorAttribute>, ICollection<WeightAttribute>, ICollection<SizeAttribute>, ICollection<ProductBulk>) getProductInstance(ProductRequest productRequest) {
            Product product = new Product() { 
                AccountId = productRequest.AccountId,
                ProductName = productRequest.ProductName,
                ProductDescription = productRequest.ProductDescription,
                CategoryId = productRequest.CategoryId,
                Status = productRequest.Status,
                CreatedBy = productRequest.CreatedBy,
                CreatedOn = productRequest.CreatedOn,
                ModifiedBy = productRequest.ModifiedBy,
                ModifiedOn = productRequest.ModifiedOn,
                Id = productRequest.Id,
                ProductAtributeId = productRequest.ProductAtributeId,
                Warranty = productRequest.Warranty,
                MinOrderValue = productRequest.MinOrderValue,
                Approve = productRequest.Approve
            };
            ICollection<ColorAttribute> colorAttributes = productRequest.ColorAttributes;
            ICollection<SizeAttribute> sizeAttributes = productRequest.SizeAttributes;
            ICollection<WeightAttribute> weightAttributes = productRequest.WeightAttributes;
            ICollection<ProductBulk> ProductBulks = productRequest.ProductBulks;
            foreach (var attribute in colorAttributes)
            {
                attribute.Product = null;
            }
            foreach (var size in sizeAttributes)
            {
                size.Product = null;
            }
            foreach (var weight in weightAttributes)
            {
                weight.Product = null;
            }
            foreach (var bulk in ProductBulks)
            {
                bulk.Product = null;
            }
            return (product, colorAttributes, weightAttributes, sizeAttributes, ProductBulks);
        }
    }
}
