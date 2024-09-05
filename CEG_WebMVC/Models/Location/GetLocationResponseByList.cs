using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Location
{
    public class GetLocationResponseByList : DefaultResponseViewModel<List<LocationViewModel>>
    {
        public GetLocationResponseByList(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
