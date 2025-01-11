using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class StudentHomework
{
    public int StudentHomeworkId { get; set; }

    public int HomeworkId { get; set; }

    public int StudentProgressId { get; set; }

    public int HomeworkResultId { get; set; }

    public int? Point { get; set; }

    public TimeSpan Playtime { get; set; }

    public string? Status { get; set; }

    public int? CorrectAnswers { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual HomeworkResult HomeworkResult { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual StudentProgress StudentProgress { get; set; } = null!;
}
