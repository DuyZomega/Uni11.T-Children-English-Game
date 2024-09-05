using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Game
{
    public int GameId { get; set; }

    public int? GameConfigId { get; set; }

    public string? DownloadLink { get; set; }

    public string Title { get; set; } = null!;

    public int? Point { get; set; }

    public string? Status { get; set; }

    public string? Type { get; set; }

    public virtual GameConfig? GameConfig { get; set; }

    public virtual ICollection<GameLevel> GameLevels { get; set; } = new List<GameLevel>();
}
