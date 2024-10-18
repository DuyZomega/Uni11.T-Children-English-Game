namespace CEG_RazorWebApp.Models.Student.Get
{
    public class StudentListVM
    {
        public string Username { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public int Highscore { get; set; }

        public int? Playtime { get; set; }

        public int? CurLevel { get; set; }

        public int? Points { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
