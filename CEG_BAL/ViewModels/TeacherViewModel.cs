using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class TeacherViewModel
    {
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public virtual AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
