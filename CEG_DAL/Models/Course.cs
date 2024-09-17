using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Status { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<RegisteredClass> RegisteredClasses { get; set; } = new List<RegisteredClass>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
