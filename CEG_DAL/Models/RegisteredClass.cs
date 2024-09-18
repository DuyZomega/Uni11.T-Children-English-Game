using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class RegisteredClass
{
    public int RegisteredClassId { get; set; }

    public int ClassId { get; set; }

    public int PaymentId { get; set; }

    public bool? PaymentStatus { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public virtual Course Class { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
