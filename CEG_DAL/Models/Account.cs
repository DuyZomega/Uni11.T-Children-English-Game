using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string Gender { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
