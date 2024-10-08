using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class HomeworkQuestion
{
    public int HomeworkQuestionId { get; set; }

    public int? HomeworkId { get; set; }

    public string? Question { get; set; }

    public virtual Homework? Homework { get; set; }

    public virtual ICollection<HomeworkAnswer> HomeworkAnswers { get; set; } = new List<HomeworkAnswer>();
}
