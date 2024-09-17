using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ParentId { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool PaymentStatus { get; set; }

    public string PaymentType { get; set; } = null!;

    public DateTime ConfirmDate { get; set; }

    public virtual Parent Parent { get; set; } = null!;

    public virtual ICollection<RegisteredClass> RegisteredClasses { get; set; } = new List<RegisteredClass>();
}
