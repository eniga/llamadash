namespace LlamaDashboard.Models;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long TotalMemory { get; set; }
    public long UsedMemory { get; set; }
    public long FreeMemory { get; set; }
    public int Utilization { get; set; }
    public string? CudaVersion { get; set; }
    public string? DriverVersion { get; set; }
}
