using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class ProductAttribute
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int LineItemId { get; set; }

    public string? ColorName { get; set; }

    public string? SizeName { get; set; }

    public int? SizeUnitId { get; set; }

    public string? WeightName { get; set; }

    public int? WeightUnitId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Product Product { get; set; } = null!;
}
