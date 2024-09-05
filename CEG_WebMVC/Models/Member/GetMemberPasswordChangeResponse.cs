using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Member
{
    public class GetMemberPasswordChangeResponse : DefaultResponseViewModel<object>
    {
        public GetMemberPasswordChangeResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }

    }
}
