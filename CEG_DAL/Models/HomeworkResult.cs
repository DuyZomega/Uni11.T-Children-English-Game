using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class HomeworkResult
{
    public int HomeworkResultId { get; set; }

    public int HomeworkId { get; set; }

    public int StudentProgressId { get; set; }

    public int? TotalPoint { get; set; }

    public int? WordAmount { get; set; }

    public TimeOnly? Playtime { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual StudentProgress StudentProgress { get; set; } = null!;
}
