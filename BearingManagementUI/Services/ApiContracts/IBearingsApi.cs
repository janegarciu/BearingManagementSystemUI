using BearingManagementUI.Models;
using BearingManagementUI.Models.Api.Requests;
using BearingManagementUI.Models.Api.Responses;
using Refit;

namespace BearingManagementUI.Services.ApiContracts;

[Headers("Content-Type: application/json; charset=UTF-8", "Accept: application/json")]
public interface IBearingsApi
{
    [Get("/bearings")]
    Task<ApiResponse<IEnumerable<BearingResponse>>> Get([Authorize] string token);
    
    [Get("/bearings/{id}")]
    Task<ApiResponse<BearingResponse>> GetById([Authorize] string token, int id);
    
    [Post("/bearings")]
    Task<IApiResponse> Create([Authorize] string token, [Body] BearingRequest request);
    
    [Put("/bearings/{id}")]
    Task<IApiResponse> UpdateById([Authorize] string token, int id, [Body] BearingRequest request);
    
    [Delete("/bearings/{id}")]
    Task<IApiResponse> DeleteById([Authorize] string token, int id);
}