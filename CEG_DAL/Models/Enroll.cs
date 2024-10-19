using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Enroll
{
    public int EnrollId { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public int TransactionId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime EnrolledDate { get; set; }

    public string? Status { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;
}
