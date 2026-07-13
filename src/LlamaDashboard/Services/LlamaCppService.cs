using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using LlamaDashboard.Models;

namespace LlamaDashboard.Services;

public class LlamaCppService : ILlamaCppService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LlamaCppService> _logger;
    private readonly string _baseUrl;

    public LlamaCppService(HttpClient httpClient, ILogger<LlamaCppService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _baseUrl = configuration.GetValue<string>("LlamaCpp:Url") ?? "http://localhost:8080/v1";
    }

    public async Task<List<ModelInfo>> GetModelsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/models");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            
            var models = new List<ModelInfo>();
            if (doc.RootElement.TryGetProperty("data", out var dataArray))
            {
                foreach (var modelElement in dataArray.EnumerateArray())
                {
                    var model = new ModelInfo
                    {
                        Id = modelElement.GetProperty("id").GetString() ?? string.Empty,
                        Name = modelElement.GetProperty("id").GetString() ?? string.Empty,
                        IsLoaded = true
                    };
                    models.Add(model);
                }
            }
            
            return models;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching models");
            return new List<ModelInfo>();
        }
    }

    public async Task<bool> LoadModelAsync(string modelName)
    {
        try
        {
            var request = new { model = modelName };
            var json = JsonSerializer.Serialize(request);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/models/load", content);
            
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading model: {Model}", modelName);
            return false;
        }
    }

    public async Task<bool> UnloadModelAsync(string modelName)
    {
        try
        {
            var request = new { model = modelName };
            var json = JsonSerializer.Serialize(request);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/models/unload", content);
            
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unloading model: {Model}", modelName);
            return false;
        }
    }

    public async Task<Stats> GetStatsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/stats");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                
                var stats = new Stats
                {
                    TokensProcessed = 0,
                    TokensPerSecond = 0,
                    Uptime = 0,
                    LastRequest = DateTime.UtcNow
                };
                
                if (doc.RootElement.TryGetProperty("tokens_total", out var tokensTotal))
                    stats.TokensProcessed = tokensTotal.GetInt64();
                
                if (doc.RootElement.TryGetProperty("tokens_per_second", out var tps))
                    stats.TokensPerSecond = tps.GetDouble();
                
                if (doc.RootElement.TryGetProperty("uptime", out var uptime))
                    stats.Uptime = uptime.GetDouble();
                
                return stats;
            }
            
            // Return default stats if endpoint not available
            return new Stats
            {
                TokensProcessed = 0,
                TokensPerSecond = 0,
                Uptime = 0,
                LastRequest = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching stats");
            return new Stats
            {
                TokensProcessed = 0,
                TokensPerSecond = 0,
                Uptime = 0,
                LastRequest = DateTime.UtcNow
            };
        }
    }

    public async Task<List<Device>> GetDevicesAsync()
    {
        try
        {
            // Try to get GPU info from llama.cpp server
            var response = await _httpClient.GetAsync($"{_baseUrl}/devices");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Device>>(json) ?? new List<Device>();
            }
            
            // Return mock data for demonstration
            return new List<Device>
            {
                new()
                {
                    Id = 0,
                    Name = "GPU 0",
                    Type = "NVIDIA",
                    TotalMemory = 24 * 1024 * 1024 * 1024,
                    UsedMemory = 12 * 1024 * 1024 * 1024,
                    FreeMemory = 12 * 1024 * 1024 * 1024,
                    Utilization = 50,
                    CudaVersion = "12.1"
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching devices");
            return new List<Device>();
        }
    }

    public async Task<ChatCompletionResponse> ChatCompletionAsync(ChatCompletionRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{_baseUrl}/chat/completions", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ChatCompletionResponse>(responseJson) 
                ?? new ChatCompletionResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending chat completion request");
            throw;
        }
    }

    public async Task<bool> HealthCheckAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/health");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
