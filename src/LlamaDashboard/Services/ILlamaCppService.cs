using LlamaDashboard.Models;

namespace LlamaDashboard.Services;

public interface ILlamaCppService
{
    Task<List<ModelInfo>> GetModelsAsync();
    Task<bool> LoadModelAsync(string modelName);
    Task<bool> UnloadModelAsync(string modelName);
    Task<Stats> GetStatsAsync();
    Task<List<Device>> GetDevicesAsync();
    Task<ChatCompletionResponse> ChatCompletionAsync(ChatCompletionRequest request);
    Task<bool> HealthCheckAsync();
}
