using System;
using System.Collections.Generic;

namespace DatabaseTest;

public partial class InvoiceItem
{
    public string InvoiceNumber { get; set; } = null!;

    public int Position { get; set; }

    public string? Name { get; set; }

    public decimal? Units { get; set; }

    public decimal? CostPerUnit { get; set; }

    public virtual Invoice InvoiceNumberNavigation { get; set; } = null!;
}
