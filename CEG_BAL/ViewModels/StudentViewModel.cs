namespace CEG_BAL.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Account = new AccountViewModel();
        }

        public string Description { get; set; } = null!;

        public int? CurLevel { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Image { get; set; }

        public AccountViewModel Account { get; set; } = null!;

        public List<EnrollViewModel> Enrolls { get; set; } = new List<EnrollViewModel>();

        public ParentViewModel Parent { get; set; } = null!;

        public List<StudentProgressViewModel> StudentProgresses { get; set; } = new List<StudentProgressViewModel>();
    }
}
