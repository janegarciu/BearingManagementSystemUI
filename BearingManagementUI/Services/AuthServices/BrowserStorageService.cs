using System.Text.Json;
using Blazored.LocalStorage;

namespace BearingManagementUI.Services;

public class BrowserStorageService(ILocalStorageService localStorage)
{
    public async Task SaveToStorage<TData>(string key, TData value)
    {
        var serializedData = Serialize(value);
        await localStorage.SetItemAsync(key, serializedData);
    }

    public async Task<TData?> GetFromStorage<TData>(string key)
    {
        var serializedData = await localStorage.GetItemAsync<string?>(key);
        return Deserialize<TData?>(serializedData);
    }

    public async Task RemoveFromStorage(string key)
    {
        await localStorage.RemoveItemAsync(key);
    }

    private static readonly JsonSerializerOptions JsonSerializerOptions = new();

    private static string Serialize<TData>(TData data) =>
        JsonSerializer.Serialize(data, JsonSerializerOptions);

    private static TData? Deserialize<TData>(string? jsonData)
    {
        if(!string.IsNullOrWhiteSpace(jsonData))
        {
            return JsonSerializer.Deserialize<TData>(jsonData, JsonSerializerOptions);
        }
        return default;
    }
}