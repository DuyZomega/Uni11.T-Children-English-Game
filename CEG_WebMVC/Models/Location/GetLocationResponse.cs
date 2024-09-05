using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Location
{
    public class GetLocationResponse : DefaultResponseViewModel<LocationViewModel>
    {
        public GetLocationResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
