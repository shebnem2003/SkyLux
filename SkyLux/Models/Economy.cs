using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class Economy
{
    public int EconomyId { get; set; }

    public string? EconomyName { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
