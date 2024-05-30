using System;
using System.Collections.Generic;

namespace SCs_Project_Master.Source.Models;

public partial class Invoice
{
    public string Number { get; set; } = null!;

    public DateOnly? CreationDate { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual Project Project { get; set; }
}
