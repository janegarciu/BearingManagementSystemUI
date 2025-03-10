using System.Security.Claims;
using BearingManagementUI.Models.Api.Requests;
using BearingManagementUI.Services.ApiContracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace BearingManagementUI.Services.AuthServices;

public class CustomAuthStateProvider : AuthenticationStateProvider, IAccountManagement
{
    private const string AuthenticationType = "CustomAuth";
    private const string UserStorageKey = "user";
    private readonly BrowserStorageService _browserStorageService;
    private readonly IAuthApi _authApi;
    public User CurrentUser { get; set; } = new();
    private static AuthenticationState EmptyAuthState => new (new ClaimsPrincipal());

    public CustomAuthStateProvider(BrowserStorageService browserStorageService, IAuthApi authApi)
    {
        _browserStorageService = browserStorageService;
        _authApi = authApi;
        AuthenticationStateChanged += CustomAuthStateProvider_AuthenticationStateChanged;
    }

    private async void CustomAuthStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
    {
        var authState = await task;
        if(authState is not null)
        {
            var idStr = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(
                !string.IsNullOrWhiteSpace(idStr) 
                && int.TryParse(idStr, out int id)
                && id > 0 
                )
            {
                CurrentUser = new User
                {
                    Id = id,
                    Name = authState.User.FindFirst(ClaimTypes.Name)!.Value,
                    Token = authState.User.FindFirst("Token")!.Value
                };
                return;
            }
        }
        CurrentUser = new User();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await _browserStorageService.GetFromStorage<User?>(UserStorageKey);
        if (user is null)
        {
            CurrentUser = new User();
            return EmptyAuthState;
        }
        CurrentUser = user;

        var authState = GenerateAuthState(user);
        return authState;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var userDetails = await _authApi.Login(new LoginRequest
        {
            Username = username,
            Password = password
        });
        if (userDetails is { IsSuccessStatusCode: true, Content: not null })
        {
            var user = new User
            {
                Id = userDetails.Content.Id,
                Name = userDetails.Content.Name,
                Token = userDetails.Content.Token
            };
            await _browserStorageService.SaveToStorage(UserStorageKey, user);

            var authState = GenerateAuthState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return true;
        }
        return false;
    }

    public async Task LogoutAsync()
    {
        await _browserStorageService.RemoveFromStorage(UserStorageKey);
        NotifyAuthenticationStateChanged(Task.FromResult(EmptyAuthState));
    }
    
    public async Task<bool> RegisterAsync(string username, string password)
        {
            try
            {
                var result = await _authApi.Register(new LoginRequest
                {
                    Username = username,
                    Password = password
                });

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

    private static AuthenticationState GenerateAuthState(User user)
    {
        Claim[] claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new("Token", user.Token ?? string.Empty),
        ];

        var identity = new ClaimsIdentity(claims, AuthenticationType);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(claimsPrincipal);
        return authState;
    }
}
public class User
{
    public int Id { get; init; }
    public string Name { get; init; } = String.Empty;
    public string? Token { get; init; } = String.Empty;
}