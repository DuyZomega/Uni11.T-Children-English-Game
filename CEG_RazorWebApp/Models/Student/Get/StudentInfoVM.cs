using CEG_DAL.Models;

namespace CEG_RazorWebApp.Models.Student.Get
{
    public class StudentInfoVM
    {
        public int StudentId { get; set; }

        public int ParentId { get; set; }

        public int AccountId { get; set; }

        public int Highscore { get; set; }

        public string Description { get; set; } = null!;

        public int? Playtime { get; set; }

        public int? CurLevel { get; set; }

        public int? Points { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Image { get; set; }
    }
}
