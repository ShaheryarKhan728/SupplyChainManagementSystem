using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class AccountUser
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string? Username { get; set; }

    public string? UserAddress { get; set; }

    public int? UserPhone1 { get; set; }

    public int? UserPhone2 { get; set; }

    public int? UserCnic { get; set; }

    public string? UserArea { get; set; }

    public string? UserCity { get; set; }

    public decimal? UserRating { get; set; }

    public decimal? UserReturnRate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Approve { get; set; }

    public bool? Status { get; set; }
}
