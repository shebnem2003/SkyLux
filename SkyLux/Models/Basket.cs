using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class Basket
{
    public int BasketId { get; set; }

    public int? BasketTicketId { get; set; }

    public int? BasketUserId { get; set; }

    public virtual Ticket? BasketTicket { get; set; }

    public virtual User? BasketUser { get; set; }
}
