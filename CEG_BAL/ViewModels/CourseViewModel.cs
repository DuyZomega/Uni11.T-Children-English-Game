using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class CourseViewModel
    {

        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? NumberOfStudent { get; set; }

        public string? Status { get; set; }

        public virtual Class Class { get; set; } = null!;

        public virtual Teacher ClassNavigation { get; set; } = null!;

        public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
