using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Certificate { get; set; }

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
