using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public string? VnpayId { get; set; }

    public int TransactionAmount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionStatus { get; set; } = null!;

    public string TransactionType { get; set; } = null!;

    public DateTime ConfirmDate { get; set; }

    public string? Description { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Enroll> Enrolls { get; set; } = new List<Enroll>();
}
