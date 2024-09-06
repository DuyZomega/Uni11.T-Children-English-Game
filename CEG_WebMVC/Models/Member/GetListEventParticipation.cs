using BAL.ViewModels.Event;

namespace CEG_WebMVC.Models.Member
{
    public class GetListEventParticipation : DefaultResponseViewModel<List<GetEventParticipation>>
    {
        public GetListEventParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
