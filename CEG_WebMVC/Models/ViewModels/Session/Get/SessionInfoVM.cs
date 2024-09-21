namespace CEG_WebMVC.Models.ViewModels.Session.Get
{
    public class SessionInfoVM
    {
        public int? SessionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }

        //public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
