using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public int CourseId { get; set; } 

    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
