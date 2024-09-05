using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class ParentViewModel
    {
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public virtual Account Account { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
