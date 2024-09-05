using BAL.ViewModels.Authenticates;

namespace CEG_WebMVC.Models.Auth
{
    public class GetGGAuthenResponse : DefaultResponseViewModel<GGAuthenResponse>
    {
        public GetGGAuthenResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
