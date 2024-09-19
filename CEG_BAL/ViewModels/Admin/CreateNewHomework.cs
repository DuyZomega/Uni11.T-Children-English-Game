using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin
{
    public class CreateNewHomework
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalPoint { get; set; }
        public int WordAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SessionTitle { get; set; }
    }
}
