using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Contest
{
    public class GetContestEndedUpdateResponse : DefaultResponseViewModel<object>
    {
        public GetContestEndedUpdateResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
