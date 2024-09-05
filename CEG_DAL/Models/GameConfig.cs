using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class GameConfig
{
    public int GameConfigId { get; set; }

    public string Title { get; set; } = null!;

    public int? Point { get; set; }

    public string? CorrectAnswer { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
}
