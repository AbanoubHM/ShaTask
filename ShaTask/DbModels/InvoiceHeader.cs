using System;
using System.Collections.Generic;

namespace ShaTask.DbModels;

public partial class InvoiceHeader
{
    public long Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateTime Invoicedate { get; set; }

    public int? CashierId { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Cashier? Cashier { get; set; }

    public virtual List<InvoiceDetail> InvoiceDetails { get; set; } 
}
