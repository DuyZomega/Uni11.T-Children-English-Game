using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Manager
{
    public class GetMemberStatusExpireResponse : DefaultResponseViewModel<MemberViewModel>
    {
        public GetMemberStatusExpireResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
