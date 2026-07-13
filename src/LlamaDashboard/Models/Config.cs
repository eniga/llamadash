namespace LlamaDashboard.Models;

public class Config
{
    public string ServerUrl { get; set; } = "http://localhost:8080/v1";
    public string ApiKey { get; set; } = string.Empty;
    public int RefreshInterval { get; set; } = 5000;
    public string DashboardName { get; set; } = "Llama Dashboard";
    public string Theme { get; set; } = "dark";
}
