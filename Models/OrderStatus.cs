using System;
using System.Collections.Generic;

namespace FPT_Ebook.Models;

public partial class OrderStatus
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
