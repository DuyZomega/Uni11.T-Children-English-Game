using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Models.Homework.Get;

namespace CEG_RazorWebApp.Models.Session.Get
{
    public class SessionInfoVM
    {
        public SessionInfoVM()
        {
        }
        public int? SessionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Number {  get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
        public int? HomeworksAmount { get; set; }
    }
}
