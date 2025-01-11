namespace CEG_RazorWebApp.Models.Student.Get
{
    public class StudentListVM
    {
        public string Username { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int? CurLevel { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
