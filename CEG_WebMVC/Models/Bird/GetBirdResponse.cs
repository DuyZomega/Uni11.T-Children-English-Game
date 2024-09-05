using BAL.ViewModels;

namespace CEG_WebMVC.Models.Bird
{
    public class GetBirdResponse : DefaultResponseViewModel<BirdViewModel>
    {
        public GetBirdResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
