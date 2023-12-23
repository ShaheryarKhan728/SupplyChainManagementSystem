using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public decimal? CategoryMargin { get; set; }

    public int? CategoryHeaderId { get; set; }

    public int? CategoryTypeId { get; set; }

    public bool? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
