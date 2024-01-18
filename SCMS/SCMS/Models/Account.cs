using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPassword { get; set; }

    public int? AccountType { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool? Status { get; set; }

    public virtual AccountType? AccountTypeNavigation { get; set; }
}
