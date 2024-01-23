using SCMS.Models;

namespace SCMS.DTO
{
    public class OrderDto
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
        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}