using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int ParentId { get; set; }

    public int AccountId { get; set; }

    public int Highscore { get; set; }

    public string Description { get; set; } = null!;

    public int? Playtime { get; set; }

    public int? CurLevel { get; set; }

    public int? Points { get; set; }

    public int? Age { get; set; }

    public DateTime? Birthdate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Enroll> Enrolls { get; set; } = new List<Enroll>();

    public virtual Parent Parent { get; set; } = null!;

    public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();
}
