using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.FieldTrip
{
    public class GetFieldTripPostResponse : DefaultResponseViewModel<FieldTripViewModel>
    {
        public GetFieldTripPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
        public GetFieldTripPostResponse()
        {
        }
    }
}
