namespace CEG_BAL.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Account = new AccountViewModel();
        }
        public int Highscore { get; set; }

        public string Description { get; set; } = null!;

        public int? Playtime { get; set; }

        public int? CurLevel { get; set; }

        public int? Points { get; set; }

        public DateTime? Birhthdate { get; set; }

        public virtual AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<EnrollViewModel> Enrolls { get; set; } = new List<EnrollViewModel>();

        public virtual ParentViewModel Parents { get; set; } = null!;

        public virtual ICollection<StudentProgressViewModel> StudentProcesses { get; set; } = new List<StudentProgressViewModel>();
    }
}
