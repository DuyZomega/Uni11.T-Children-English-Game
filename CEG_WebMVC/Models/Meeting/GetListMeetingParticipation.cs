using BAL.ViewModels;

namespace CEG_WebMVC.Models.Meeting
{
    public class GetListMeetingParticipation : DefaultResponseViewModel<List<MeetingParticipantViewModel>>
    {
        public GetListMeetingParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
