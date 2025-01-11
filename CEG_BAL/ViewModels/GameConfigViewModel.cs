using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class GameConfigViewModel
    {
        public int? GameConfigId { get; set; }
        public string Title { get; set; } = null!;

        public int? Point { get; set; }

        public string? CorrectAnswer { get; set; }

        public string? Status { get; set; }

        public List<GameViewModel> Games { get; set; } = new List<GameViewModel>();

        public List<HomeworkViewModel> Homeworks { get; set; } = new List<HomeworkViewModel>();
    }
}
