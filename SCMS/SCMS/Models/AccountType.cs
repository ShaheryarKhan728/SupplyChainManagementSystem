using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class AccountType
{
    public int Id { get; set; }

    public string? AccountTypeName { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
