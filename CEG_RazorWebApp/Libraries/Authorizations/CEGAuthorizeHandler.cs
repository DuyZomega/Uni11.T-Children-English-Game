using CEG_DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CEG_RazorWebApp.Libraries.Authorizations
{
    public class CEGAuthorizeHandler : IAuthorizationHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly ISession _session;

        public CEGAuthorizeHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var session = _contextAccessor.HttpContext?.Session;
            var model = context.Resource as DefaultHttpContext;
            if (session == null) return;

            var requirement = context.Requirements.FirstOrDefault(r => r is CEGAuthorizeRequirement) as CEGAuthorizeRequirement;
            if (requirement == null) return;

            var isAuthenticated = session.GetString(requirement.Role);

            if (isAuthenticated == null || isAuthenticated == Constants.GUEST)
            {
                //context.Resource = "/Login";
                //_contextAccessor.HttpContext.Session.SetString(Constants.ROLE_NAME, Constants.GUEST);
                /*await _contextAccessor.HttpContext.ForbidAsync();*/
                context.Fail();
            } 
            else
            {
                //var authenticationScheme = _contextAccessor.HttpContext.RequestServices.GetService<IAuthenticationSchemeProvider>()?.GetDefaultChallengeSchemeAsync().Result;
                context.Succeed(requirement);
            }
        }
    }
}
