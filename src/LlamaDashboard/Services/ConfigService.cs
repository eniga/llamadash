using LlamaDashboard.Models;

namespace LlamaDashboard.Services;

public class ConfigService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ConfigService> _logger;
    private Config _currentConfig;

    public event EventHandler<Config>? ConfigChanged;

    public ConfigService(IConfiguration configuration, ILogger<ConfigService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _currentConfig = LoadConfig();
    }

    public Config GetCurrentConfig()
    {
        return _currentConfig;
    }

    public void UpdateConfig(Config newConfig)
    {
        _currentConfig = newConfig;
        
        // Update configuration
        _configuration["LlamaCpp:Url"] = newConfig.ServerUrl;
        _configuration["LlamaCpp:ApiKey"] = newConfig.ApiKey;
        _configuration["LlamaCpp:RefreshInterval"] = newConfig.RefreshInterval.ToString();
        _configuration["Dashboard:Name"] = newConfig.DashboardName;
        _configuration["Dashboard:Theme"] = newConfig.Theme;
        
        _logger.LogInformation("Configuration updated");
        ConfigChanged?.Invoke(this, newConfig);
    }

    private Config LoadConfig()
    {
        return new Config
        {
            ServerUrl = _configuration.GetValue<string>("LlamaCpp:Url") ?? "http://localhost:8080/v1",
            ApiKey = _configuration.GetValue<string>("LlamaCpp:ApiKey") ?? string.Empty,
            RefreshInterval = _configuration.GetValue<int>("LlamaCpp:RefreshInterval") ?? 5000,
            DashboardName = _configuration.GetValue<string>("Dashboard:Name") ?? "Llama Dashboard",
            Theme = _configuration.GetValue<string>("Dashboard:Theme") ?? "dark"
        };
    }
}
