using BearingManagementUI.Models.Api.Requests;
using BearingManagementUI.Models.Api.Responses;
using Refit;

namespace BearingManagementUI.Services.ApiContracts;

[Headers("Content-Type: application/json; charset=UTF-8", "Accept: application/json")]
public interface IAuthApi
{
    [Post("/auth/login")]
    Task<IApiResponse<LoginResponse>> Login(LoginRequest request);
    
    [Post("/auth/register")]
    Task<IApiResponse<LoginResponse>> Register(LoginRequest request);
}