using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Home
{
    public class StudentLeaderboard
    {
        public string StudentName { get; set; } = string.Empty; // Student's full name
        public int Rank { get; set; } // Rank of the student
        public int Points { get; set; } // Total points of the student
    }
}
