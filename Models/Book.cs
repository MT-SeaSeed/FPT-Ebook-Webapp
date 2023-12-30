using System;
using System.Collections.Generic;

namespace FPT_Ebook.Models;

public partial class Book
{
    public int BookId { get; set; }
    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? QuantityInStock { get; set; }

    public string? ImagePath { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
