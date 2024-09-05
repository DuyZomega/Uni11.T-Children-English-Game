using BAL.ViewModels.Member;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Member
{
    public class GetMemberAvatarResponse : DefaultResponseViewModel<UpdateMemberAvatar>
    {
        public GetMemberAvatarResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
