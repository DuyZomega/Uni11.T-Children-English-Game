using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.Homework.Get
{
    public class HomeworkInfoVM
    {
        public int? HomeworkId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
    }
}
