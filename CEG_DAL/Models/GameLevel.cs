using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class GameLevel
{
    public int GameLevelId { get; set; }

    public int GameId { get; set; }

    public string Title { get; set; } = null!;

    public string? Status { get; set; }

    public virtual Game Game { get; set; } = null!;
}
