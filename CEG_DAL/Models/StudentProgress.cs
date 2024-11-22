using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class StudentProgress
{
    public int StudentProgressId { get; set; }

    public int StudentId { get; set; }

    public int? TotalPoint { get; set; }

    public TimeOnly? Playtime { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<StudentHomework> StudentHomeworks { get; set; } = new List<StudentHomework>();
}
