using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels
{
    public class StudentViewModel
    {
        public int Highscore { get; set; }

        public string Description { get; set; } = null!;

        public int? Playtime { get; set; }

        public int? CurLevel { get; set; }

        public int? Points { get; set; }

        public int? Age { get; set; }

        public DateTime? Birhthdate { get; set; }

        public virtual AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<Enroll> Enrolls { get; set; } = new List<Enroll>();

        public virtual Parent Parents { get; set; } = null!;

        public virtual ICollection<StudentProcess> StudentProcesses { get; set; } = new List<StudentProcess>();
    }
}
