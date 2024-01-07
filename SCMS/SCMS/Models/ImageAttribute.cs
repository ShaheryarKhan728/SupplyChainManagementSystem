using System;
using System.Collections.Generic;

namespace SCMS.Models;

public partial class ImageAttribute
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int LineItemId { get; set; }

    public string? FileName { get; set; }

    public string? FileUrl { get; set; }
}
