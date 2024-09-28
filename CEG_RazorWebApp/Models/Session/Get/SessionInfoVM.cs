using CEG_BAL.ViewModels;
using CEG_WebMVC.Models.Homework.Get;

namespace CEG_WebMVC.Models.Session.Get
{
    public class SessionInfoVM
    {
        public SessionInfoVM()
        {
            HomeworksAmount = Homeworks.Count;
        }
        public int? SessionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Number {  get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
        public int? HomeworksAmount { get; set; }

        public List<HomeworkInfoVM> Homeworks { get; set; } = new List<HomeworkInfoVM>();
    }
}
