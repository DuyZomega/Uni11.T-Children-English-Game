/*using CEG_BAL.ViewModels.Authenticates;*/
using CEG_WebMVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using CEG_WebMVC.Library;
using CEG_WebMVC.Models.Auth;

namespace CEG_WebMVC.Services.Implements
{
    /*public class SystemLoginService : ISystemLoginService
    {
        private readonly ILogger<SystemLoginService> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient client = null;
        private string AuthenAPI_URL = "";
        private readonly BirdClubLibrary methcall = new();
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
            client.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            AuthenAPI_URL = "/api/User";
        }

        public async Task<string?> GetTokenAsync()
        {
            AuthenAPI_URL += "/Login";

            AuthenRequest authenRequest = new()
            {
                Username = _config.GetSection(Library.Constants.SYSTEM_DEFAULT_ACCOUNT_USR_NAME).Value,
                Password = _config.GetSection(Library.Constants.SYSTEM_DEFAULT_ACCOUNT_USR_PASSWORD).Value
            };

            var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: client,
                options: jsonOptions,
                methodName: Library.Constants.POST_METHOD,
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
    }*/
}
