using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public int CourseId { get; set; }
    public int? Number { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? Hours { get; set; }

    public int? Number { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<StudentProgress> StudentProgresses { get; set; } = new List<StudentProgress>();
}
