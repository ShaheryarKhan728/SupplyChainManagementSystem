using SCMS.Models;

namespace SCMS.Requests
{
    public class OrderRequest : IDisposable
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public long? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? OrderBy { get; set; }

        public decimal? OrderAmount { get; set; }

        public int? OrderQuantity { get; set; }

        public int? Approve { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public int? OrdersStatus { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        //public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

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
        ~OrderRequest()
        {
            Dispose(false);
        }

        public (Order, ICollection<OrderDetail>) getOrderInstance(OrderRequest orderRequest) {
            Order order = new Order() {
                Id = orderRequest.Id,
                AccountId = orderRequest.AccountId,
                OrderNumber = orderRequest.OrderNumber,
                OrderDate = orderRequest.OrderDate,
                OrderBy = orderRequest.OrderBy,
                OrderAmount = orderRequest.OrderAmount,
                OrderQuantity = orderRequest.OrderQuantity,
                Approve = orderRequest.Approve,
                ApprovedOn = orderRequest.ApprovedOn,
                OrdersStatus = orderRequest.OrdersStatus,
                CreatedOn = orderRequest.CreatedOn,
                ModifiedBy = orderRequest.ModifiedBy,
                ModifiedOn = orderRequest.ModifiedOn,
            };
            ICollection<OrderDetail> orderDetails = orderRequest.OrderDetails;
            return (order, orderDetails);
        }
    }
}
