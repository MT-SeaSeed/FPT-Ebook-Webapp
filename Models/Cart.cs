using System;
using System.Collections.Generic;

namespace FPT_Ebook.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public int? BookId { get; set; }

    public int? Quantity { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Customer? Customer { get; set; }

    internal object SingleOrDefault(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
