using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewGameLevel
    {
        public string Title { get; set; } = null!;

        public string? Status { get; set; }

        public int? GameId { get; set; }
    }
}
