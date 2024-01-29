using System;
using System.Collections.Generic;

namespace SkyLux.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public DateTime? TicketDepartureDate { get; set; }

    public string? TicketFrom { get; set; }

    public string? TicketDepartureTime { get; set; }

    public string? TicketArrivalTime { get; set; }

    public string? TicketTo { get; set; }

    public DateTime? TicketArrivalDate { get; set; }

    public string? TicketHour { get; set; }

    public string? TicketStatus { get; set; }

    public float? TicketPrice { get; set; }

    public int? TicketEconomyId { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual Economy? TicketEconomy { get; set; }
}
