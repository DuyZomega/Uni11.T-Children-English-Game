using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Meeting
{
    public class GetMeetingParticipantResponse : DefaultResponseViewModel<MeetingParticipantViewModel>
    {
        public GetMeetingParticipantResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
