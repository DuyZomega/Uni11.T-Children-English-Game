/*using CEG_BAL.ViewModels.Authenticates;*/
using CEG_RazorWebApp.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using CEG_RazorWebApp.Models.Auth;
using CEG_RazorWebApp.Libraries;
using CEG_BAL.ViewModels.Authenticates;
using CEG_BAL.Configurations;
using CEG_RazorWebApp.Libraries.Models;

namespace CEG_RazorWebApp.Services.Implements
{
    public class SystemLoginService : ISystemLoginService
    {
        private readonly ILogger<SystemLoginService> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient client = null;
        private string AuthenAPI_URL = "";
        private readonly CEG_RAZOR_Library methcall = new();
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };

        public SystemLoginService(ILogger<SystemLoginService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.BaseAddress = new Uri(config.GetSection(CEGConstants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value);
            AuthenAPI_URL = _config.GetSection(CEGConstants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value + "Account";
        }

        public async Task<string?> GetTokenAsync()
        {
            AuthenAPI_URL += "/Login";

            AuthenRequest authenRequest = new()
            {
                Username = _config.GetSection(CEGConstants.SYSTEM_DEFAULT_ACCOUNT_USR_NAME).Value,
                Password = _config.GetSection(CEGConstants.SYSTEM_DEFAULT_ACCOUNT_USR_PASSWORD).Value
            };

            var authenResponse = await methcall.CallMethodReturnObject<DefaultResponseModel<AuthenResponse>>(
                _httpClient: client,
                options: jsonOptions,
                methodName: CEGConstants.POST_METHOD,
                url: AuthenAPI_URL,
                inputType: authenRequest,
                _logger: _logger);

            if (authenResponse == null || !authenResponse.Status || authenResponse.Data.AccessToken == null)
            {
                _logger.LogError("Error while system login to retrieve access token");
                return null;
            }
            var responseAuth = authenResponse.Data;

            return responseAuth.AccessToken;
        }
    }
}
