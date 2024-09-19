using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class HomeworkResultViewModel
    {
        public int? TotalPoint { get; set; }

        public int? WordAmount { get; set; }

        public TimeOnly? Playtime { get; set; }

        public virtual Homework Homework { get; set; } = null!;

        public virtual StudentProgress StudentProgress { get; set; } = null!;
    }
}
