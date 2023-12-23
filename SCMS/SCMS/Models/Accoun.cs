using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class Accoun
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? Username { get; set; }

    public int? AccountType { get; set; }

    public DateTime? CreatedOn { get; set; }
}
