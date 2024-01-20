using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class WeightUnit
{
    public int WeightUnitId { get; set; }

    public string? WeightUnitName { get; set; }

    public virtual ICollection<WeightAttribute> WeightAttributes { get; } = new List<WeightAttribute>();
}
