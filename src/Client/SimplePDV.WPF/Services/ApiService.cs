using System.Net.Http;
using System.Net.Http.Json;

namespace SimplePDV.WPF.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5000/api";

    public ApiService()
    {
        _httpClient = new HttpClient 
        { 
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public async Task<bool> IsOnlineAsync()
    {
        try
        {
            var fullUrl = $"{BaseUrl}/produtos";
            Console.WriteLine($"[ApiService] Testando conexão com: {fullUrl}");
            
            var response = await _httpClient.GetAsync(fullUrl);
            Console.WriteLine($"[ApiService] Status: {response.StatusCode}");
            
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ApiService] Erro: {ex.GetType().Name} - {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"[ApiService] InnerException: {ex.InnerException.Message}");
            return false;
        }
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var url = $"{BaseUrl}/{endpoint.TrimStart('/')}";
            Console.WriteLine($"[ApiService] GET {url}");
            return await _httpClient.GetFromJsonAsync<T>(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ApiService] Erro em GET: {ex.Message}");
            return default;
        }
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        try
        {
            var url = $"{BaseUrl}/{endpoint.TrimStart('/')}";
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch
        {
            return default;
        }
    }

    public async Task<bool> PutAsync<T>(string endpoint, T data)
    {
        try
        {
            var url = $"{BaseUrl}/{endpoint.TrimStart('/')}";
            var response = await _httpClient.PutAsJsonAsync(url, data);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> PatchAsync(string endpoint)
    {
        try
        {
            var url = $"{BaseUrl}/{endpoint.TrimStart('/')}";
            var response = await _httpClient.PatchAsync(url, null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        try
        {
            var url = $"{BaseUrl}/{endpoint.TrimStart('/')}";
            Console.WriteLine($"[ApiService] DELETE {url}");
            var response = await _httpClient.DeleteAsync(url);
            Console.WriteLine($"[ApiService] DELETE Status: {response.StatusCode}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ApiService] Erro em DELETE: {ex.Message}");
            return false;
        }
    }
}
