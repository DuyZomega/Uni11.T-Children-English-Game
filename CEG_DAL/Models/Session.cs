using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual Course Course { get; set; } = null!;
}
