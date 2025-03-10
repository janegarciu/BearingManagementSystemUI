namespace BearingManagementUI.Models.Api.Requests;

public class BearingRequest
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string? Size { get; set; }
}