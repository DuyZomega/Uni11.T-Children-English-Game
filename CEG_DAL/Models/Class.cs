using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public int CourseId { get; set; }

    public string ClassName { get; set; } = null!;

    public int? MinimumStudents { get; set; }

    public int? MaximumStudents { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();

    public virtual Teacher Teacher { get; set; } = null!;
}
