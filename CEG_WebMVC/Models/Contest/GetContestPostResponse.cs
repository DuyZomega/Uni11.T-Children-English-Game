using BAL.ViewModels;

namespace CEG_WebMVC.Models.Contest
{
    public class GetContestPostResponse : DefaultResponseViewModel<ContestViewModel>
    {
        public GetContestPostResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
        public GetContestPostResponse()
        {
        }
    }
}
