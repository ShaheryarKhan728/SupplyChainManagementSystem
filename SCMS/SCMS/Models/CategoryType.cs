using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class CategoryType
{
    public int Id { get; set; }

    public string? CategoryTypeName { get; set; }

    public int? CategoryTypeHeaderId { get; set; }

    public bool? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }
}
