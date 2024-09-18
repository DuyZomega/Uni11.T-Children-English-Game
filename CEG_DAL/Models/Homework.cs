using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Homework
{
    public int HomeworkId { get; set; }

    public int SessionId { get; set; }

    public int GameConfigId { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int? TotalPoint { get; set; }

    public int? WordAmount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual GameConfig GameConfig { get; set; } = null!;

    public virtual ICollection<HomeworkResult> HomeworkResults { get; set; } = new List<HomeworkResult>();

    public virtual Session Session { get; set; } = null!;

    public virtual ICollection<StudentHomework> StudentHomeworks { get; set; } = new List<StudentHomework>();
}
