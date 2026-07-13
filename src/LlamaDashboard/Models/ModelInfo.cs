namespace LlamaDashboard.Models;

public class ModelInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public bool IsLoaded { get; set; }
    public string? Format { get; set; }
    public int? VocabSize { get; set; }
    public int? Layers { get; set; }
    public int? ContextSize { get; set; }
}
