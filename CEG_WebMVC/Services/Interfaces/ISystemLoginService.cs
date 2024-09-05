namespace CEG_WebMVC.Services.Interfaces
{
    public interface ISystemLoginService
    {
        Task<string?> GetTokenAsync();
    }
}
