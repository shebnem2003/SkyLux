using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTelephone { get; set; }

    public string? ContactEmail { get; set; }
}
