using BAL.ViewModels;

namespace CEG_WebMVC.Models.Bird
{
    public class GetListBirdByMemberResponse : DefaultResponseViewModel<List<BirdViewModel>>
    {
        public GetListBirdByMemberResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
