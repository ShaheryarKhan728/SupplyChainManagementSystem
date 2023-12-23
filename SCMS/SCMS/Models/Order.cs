using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class Order
{
    public int Id { get; set; }

    public long? OrderNumber { get; set; }

    public int? AccountId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? OrderBy { get; set; }

    public decimal? OrderAmount { get; set; }

    public int? OrderQuantity { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public bool? OrdersStatus { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
