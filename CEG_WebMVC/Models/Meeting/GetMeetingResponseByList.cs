using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Meeting
{
    public class GetMeetingResponseByList : DefaultResponseViewModel<List<MeetingViewModel>>
    {
        public GetMeetingResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
