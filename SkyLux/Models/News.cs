using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? NewsImage { get; set; }

    public string? NewsTitle { get; set; }

    public string? NewsText { get; set; }

    public string? NewsDate { get; set; }
}
