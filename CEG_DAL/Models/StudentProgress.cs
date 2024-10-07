using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class StudentProgress
{
    public int StudentProgressId { get; set; }

    public int StudentId { get; set; }

    public int SessionId { get; set; }

    public int ClassId { get; set; }

    public int? TotalPoint { get; set; }

    public int? Playtimes { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<HomeworkResult> HomeworkResults { get; set; } = new List<HomeworkResult>();

    public virtual Session Session { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<StudentHomework> StudentHomeworks { get; set; } = new List<StudentHomework>();
}
