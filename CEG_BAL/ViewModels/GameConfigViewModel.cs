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
        public string Title { get; set; } = null!;

        public int? Point { get; set; }

        public string? CorrectAnswer { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<Game> Games { get; set; } = new List<Game>();

        public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
