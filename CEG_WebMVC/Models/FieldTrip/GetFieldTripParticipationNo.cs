using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.FieldTrip
{
    public class GetFieldTripParticipationNo : DefaultResponseViewModel<object>
    {
        public GetFieldTripParticipationNo(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
