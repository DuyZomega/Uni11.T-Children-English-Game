using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class SessionViewModel
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public int? Hours { get; set; }

        public string? Status { get; set; }

        public virtual CourseViewModel Course { get; set; } = null!;
    }
}
