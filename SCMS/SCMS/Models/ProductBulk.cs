using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class ProductBulk
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public long? Quantity { get; set; }

    public long? Amount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedOn { get; set; }

    public DateTime? ModifedBy { get; set; }
}
