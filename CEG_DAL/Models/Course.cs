using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseType { get; set; } = null!;

    public string Description { get; set; } = null!;
    public int? TotalHours { get; set; }

    public string? Image { get; set; }

    public int? RequiredAge { get; set; }

    public string? Difficulty { get; set; }

    public string? Category { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<RegisteredClass> RegisteredClasses { get; set; } = new List<RegisteredClass>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
