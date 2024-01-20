using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class AccountApproval
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string? Username { get; set; }

    public string? UserArea { get; set; }

    public string? UserCity { get; set; }

    public int? UserCnic { get; set; }

    public string? AccountTypeName { get; set; }
}
