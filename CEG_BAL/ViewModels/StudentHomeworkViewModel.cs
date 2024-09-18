using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class StudentHomeworkViewModel
    {
        public int TotalPoint { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }

        public virtual Homework Homework { get; set; } = null!;

        public virtual StudentProgress StudentProcess { get; set; } = null!;
    }
}
