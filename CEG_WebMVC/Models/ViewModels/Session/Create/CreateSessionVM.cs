using CEG_WebMVC.Libraries;
using CEG_WebMVC.Models.ViewModels.Homework.Create;

namespace CEG_WebMVC.Models.ViewModels.Session.Create
{
    public class CreateSessionVM
    {
        public CreateSessionVM()
        {
            Status = Constants.SESSION_STATUS_DRAFT;
            Homeworks = new List<CreateHomeworkVM> { new CreateHomeworkVM() };
        }
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public int? Hours { get; set; }

        public string? Status { get; set; }

        public List<CreateHomeworkVM> Homeworks { get; set; } = new List<CreateHomeworkVM>();
    }
}
