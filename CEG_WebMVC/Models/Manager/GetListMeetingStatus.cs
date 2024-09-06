using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Manager
{
    public class GetListMeetingStatus : DefaultResponseViewModel<List<MeetingViewModel>>
    {
        public GetListMeetingStatus(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
