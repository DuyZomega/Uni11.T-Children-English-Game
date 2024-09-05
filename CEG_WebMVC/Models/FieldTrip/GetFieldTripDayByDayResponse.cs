using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.FieldTrip
{
    public class GetFieldTripDayByDayResponse : DefaultResponseViewModel<object>
    {
        public GetFieldTripDayByDayResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
