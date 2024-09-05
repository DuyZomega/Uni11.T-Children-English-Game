using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public string ClassName { get; set; } = null!;

    public int? NumberOfStudents { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();

    public virtual Teacher Teacher { get; set; } = null!;
}
