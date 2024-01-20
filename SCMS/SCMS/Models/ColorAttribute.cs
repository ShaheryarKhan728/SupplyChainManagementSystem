using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class ColorAttribute
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int LineItemId { get; set; }

    public string? ColorName { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedOn { get; set; }

    public DateTime? ModifedBy { get; set; }

    public virtual Product Product { get; set; } = null!;
}
