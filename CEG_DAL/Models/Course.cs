using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public int ClassId { get; set; }

    public int SessionId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? NumberOfStudent { get; set; }

    public string? Status { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Teacher ClassNavigation { get; set; } = null!;

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<RegisteredCourse> RegisteredCourses { get; set; } = new List<RegisteredCourse>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
