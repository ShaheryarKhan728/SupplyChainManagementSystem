using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductAtributeId { get; set; }

    public int? ProductBulkId { get; set; }

    public int? AccountId { get; set; }

    public int? Amount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
