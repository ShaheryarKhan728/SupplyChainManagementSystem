﻿using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class SizeUnit
{
    public int SizeUnitId { get; set; }

    public string? SizeUnitName { get; set; }

    public virtual ICollection<SizeAttribute> SizeAttributes { get; } = new List<SizeAttribute>();
}
