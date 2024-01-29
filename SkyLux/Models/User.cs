using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserSurname { get; set; }

    public string? UserPassword { get; set; }

    public string? UserBlock { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
}
