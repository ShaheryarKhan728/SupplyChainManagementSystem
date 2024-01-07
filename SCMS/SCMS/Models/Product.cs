using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int? CategoryId { get; set; }

    public bool? Status { get; set; }

    public string? Warranty { get; set; }

    public int? ProductAtributeId { get; set; }

    public int? AccountId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ProductAttribute> ProductAttributes { get; } = new List<ProductAttribute>();

    public virtual ICollection<ProductBulk> ProductBulks { get; } = new List<ProductBulk>();
}
