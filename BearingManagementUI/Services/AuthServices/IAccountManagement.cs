using Microsoft.AspNetCore.Components.Authorization;

namespace BearingManagementUI.Services;

    public interface IAccountManagement
    {
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();

        Task<bool> RegisterAsync(string username, string password);
        Task<AuthenticationState> GetAuthenticationStateAsync();
    }

