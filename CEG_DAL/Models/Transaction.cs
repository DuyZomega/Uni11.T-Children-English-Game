using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int ParentId { get; set; }

    public string VnpayId { get; set; } = null!;

    public int TransactionAmount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionStatus { get; set; } = null!;

    public string TransactionType { get; set; } = null!;

    public DateTime ConfirmDate { get; set; }

    public virtual ICollection<Enroll> Enrolls { get; set; } = new List<Enroll>();

    public virtual Parent Parent { get; set; } = null!;
}
