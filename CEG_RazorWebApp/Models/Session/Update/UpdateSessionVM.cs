using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CEG_WebMVC.Models.ViewModels.Session.Update
{
    public class UpdateSessionVM
    {
        public UpdateSessionVM()
        {
            var lib = new ChildrenEnglishGameLibrary();
            Number = 1;
            Hours = Constants.SESSION_HOURS;
            Status = Constants.SESSION_STATUS_DRAFT;
            DefaultSessionStatusSelectList = lib.GetSessionStatusSelectableList(Status);
        }
        public int? SessionId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Number { get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
        public List<SelectListItem> DefaultSessionStatusSelectList { get; set; }
    }
}
