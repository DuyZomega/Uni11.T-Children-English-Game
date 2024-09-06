using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.FieldTrip
{
    public class GetFieldTripGettingThereResponse : DefaultResponseViewModel<FieldtripGettingThereViewModel>
    {
        public GetFieldTripGettingThereResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
