using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string? PositionImage { get; set; }

    public string? PositionTo { get; set; }

    public string? PositionText { get; set; }

    public string? PositionPrice { get; set; }

    public string? PositionCity { get; set; }
}
