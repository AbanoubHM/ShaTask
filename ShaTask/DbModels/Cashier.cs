﻿using System;
using System.Collections.Generic;

namespace ShaTask.DbModels;

public partial class Cashier
{
    public int Id { get; set; }

    public string CashierName { get; set; } = null!;

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; } = new List<InvoiceHeader>();
}
