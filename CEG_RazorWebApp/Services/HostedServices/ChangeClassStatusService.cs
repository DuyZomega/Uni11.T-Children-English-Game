using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System;
using CEG_RazorWebApp.Services.Interfaces;
using CEG_BAL.Configurations;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Libraries.Models;

namespace CEG_RazorWebApp.Services.HostedServices
{
    public class ChangeClassStatusService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ChangeClassStatusService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private Timer _timer;
        private string DefaultAPI_URL = "";
        private readonly string updateClassStatusAPI_URL_TAIL = "Class/All/Date/Update/Status";
        private readonly MediaTypeWithQualityHeaderValue contentType = new("application/json");
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly CEG_RAZOR_Library methcall = new();
        public ChangeClassStatusService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<ChangeClassStatusService> logger, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
            DefaultAPI_URL = _config.GetSection(CEGConstants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Change Class Status Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Adjust the interval as needed

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _systemLoginService = scope.ServiceProvider.GetRequiredService<ISystemLoginService>();
                var today = DateTime.UtcNow;
                int claStatusUpdateExpired = 0;

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.BaseAddress = new Uri(_config.GetSection(CEGConstants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value);

                string? accToken = await _systemLoginService.GetTokenAsync();

                var claList = await methcall.CallMethodReturnObject<DefaultResponseModel<bool>>(
                                    _httpClient: client,
                                    options: jsonOptions,
                                    methodName: CEGConstants.GET_METHOD,
                                    url: DefaultAPI_URL + updateClassStatusAPI_URL_TAIL,
                                    _logger: _logger,
                                    accessToken: accToken);
                if (claList == null || !claList.Status)
                {
                    _logger.LogError(claList.ErrorMessage);
                    return;
                }
                _logger.LogInformation("Update classes response. {}", claList.Data);
                /*_logger.LogInformation("Succeed Retrieved list of {Count} members via API.", claList.Data.Count);
                
                _logger.LogInformation("Membership Expiry Service has updated {accStatusUpdateExpired} memberships to 'Expired' status.", claStatusUpdateExpired);*/
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Change Class Status Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
