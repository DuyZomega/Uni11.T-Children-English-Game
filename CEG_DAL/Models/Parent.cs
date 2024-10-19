using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Parent
{
    public int ParentId { get; set; }

    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
