using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class ProductApproval
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? CategoryName { get; set; }

    public int? MinOrderValue { get; set; }

    public string? Username { get; set; }
}
