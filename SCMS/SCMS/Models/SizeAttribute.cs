﻿using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class SizeAttribute
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int LineItemId { get; set; }

    public string? SizeName { get; set; }

    public int? SizeUnitId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ModifiedOn { get; set; }

    public DateTime? ModifedBy { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SizeUnit? SizeUnit { get; set; }
}
