using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class ProductAttribute
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? ColorId { get; set; }

    public int? SizeId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
