namespace MyMvcApp.Models;

public class ProductSpecs
{
    public string Cpu { get; set; } = string.Empty;
    public string Gpu { get; set; } = string.Empty;
    public string Ram { get; set; } = string.Empty;
    public string Storage { get; set; } = string.Empty;
    public string? ScreenSize { get; set; }
    public string? FormFactor { get; set; }
    public string? OperatingSystem { get; set; }
}