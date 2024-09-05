using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ParentsId { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool PaymentStatus { get; set; }

    public string PaymentType { get; set; } = null!;

    public DateTime ConfirmDate { get; set; }

    public virtual Parent Parents { get; set; } = null!;

    public virtual ICollection<RegisteredCourse> RegisteredCourses { get; set; } = new List<RegisteredCourse>();
}
