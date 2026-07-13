namespace LlamaDashboard.Models;

public class Stats
{
    public long TokensProcessed { get; set; }
    public double TokensPerSecond { get; set; }
    public double PromptEvalTokensPerSecond { get; set; }
    public double PredictedPerSecond { get; set; }
    public long TotalRequests { get; set; }
    public long ActiveRequests { get; set; }
    public double Uptime { get; set; }
    public DateTime LastRequest { get; set; }
    public int CacheHits { get; set; }
    public int CacheMisses { get; set; }
    public long PromptTokens { get; set; }
    public long CompletionTokens { get; set; }
    public long KvCacheSize { get; set; }
}
