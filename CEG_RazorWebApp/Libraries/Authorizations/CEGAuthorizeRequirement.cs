using Microsoft.AspNetCore.Authorization;

namespace CEG_RazorWebApp.Libraries.Authorizations
{
    public class CEGAuthorizeRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; } = Constants.ROLE_NAME;
    }
}
