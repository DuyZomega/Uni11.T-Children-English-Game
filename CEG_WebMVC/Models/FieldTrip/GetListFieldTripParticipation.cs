using BAL.ViewModels;

namespace CEG_WebMVC.Models.FieldTrip
{
    public class GetListFieldTripParticipation : DefaultResponseViewModel<List<FieldTripParticipantViewModel>>
    {
        public GetListFieldTripParticipation(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
