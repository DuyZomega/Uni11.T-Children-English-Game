using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Homework.Create
{
    public class CreateHomeworkVM
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public int? Hours { get; set; }

        public string? Status { get; set; }
    }
}
