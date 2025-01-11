using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class HomeworkResult
{
    public int HomeworkResultId { get; set; }

    public int? TotalPoint { get; set; }

    public int? TotalCorrectAnswers { get; set; }

    public TimeSpan Playtime { get; set; }

    public virtual ICollection<StudentHomework> StudentHomeworks { get; set; } = new List<StudentHomework>();
}
