using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class RegisteredClass
{
    public int RegisteredCourseId { get; set; }

    public int ClassId { get; set; }

    public int PaymentId { get; set; }

    public bool? PaymentStatus { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;

    public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();
}
