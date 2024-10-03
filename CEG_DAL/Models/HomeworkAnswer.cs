using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class HomeworkAnswer
{
    public int HomeworkAnswerId { get; set; }

    public int HomeworkQuestionId { get; set; }

    public string? Answer { get; set; }

    public bool Type { get; set; }

    public virtual HomeworkQuestion HomeworkQuestion { get; set; } = null!;
}
