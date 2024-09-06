using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class GameViewModel
    {

        public string? DownloadLink { get; set; }

        public string Title { get; set; } = null!;

        public int? Point { get; set; }

        public string? Status { get; set; }

        public string? Type { get; set; }

        public virtual GameConfig? GameConfig { get; set; }

        public virtual ICollection<GameLevel> GameLevels { get; set; } = new List<GameLevel>();
    }
}
