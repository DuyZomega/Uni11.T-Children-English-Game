using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class StudentAnswer
{
    public int StudentAnswerId { get; set; }

    public int GameId { get; set; }

    public int StudentHomeworkId { get; set; }

    public string? Answer { get; set; }

    public string? Type { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual StudentHomework StudentHomework { get; set; } = null!;
}
