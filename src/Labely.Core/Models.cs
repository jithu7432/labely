using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Labely.Core;

public record CanvasConfig {
    [JsonPropertyName("width")]
    public required int Width { get; set; }

    [JsonPropertyName("height")]
    public required int Height { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = "#00000000";

    [JsonPropertyName("border_offset")]
    public int BorderOffset { get; set; }

    public int BorderAll => BorderOffset + BorderThickness;

    [JsonPropertyName("border_pixels")]
    public int BorderThickness { get; set; }

    [JsonPropertyName("border_color")]
    public string BorderColor { get; set; } = "#FFFFFFFF";

}

public record ElementConfig {
    [JsonPropertyName("kind")]
    public required string Kind { get; set; }

    [JsonPropertyName("x")]
    public int X { get; set; } = 0;

    [JsonPropertyName("y")]
    public int Y { get; set; } = 0;

    [JsonPropertyName("width")]
    public int Width { get; set; } = 0;

    [JsonPropertyName("height")]
    public int Height  { get; set; } = 0;

    [JsonPropertyName("size")]
    public int Size { get; set; } = 0;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("bg")]
    public string BackgroundColor { get; set; } = "#FFFFFF";

    [JsonPropertyName("fg")]
    public string ForegroundColor { get; set; } = "#000000";
}

public record struct LabelConfig {
    [JsonPropertyName("canvas")]
    public required CanvasConfig Canvas { get; set; }

    [JsonPropertyName("elements")]
    public ElementConfig[] Elements { get; set; } = [];

    public LabelConfig() { }

    public static LabelConfig FromFile([StringSyntax(StringSyntaxAttribute.Uri)] string path) {
        using var fs = File.OpenRead(path);
        return JsonSerializer.Deserialize<LabelConfig>(fs);
    }
}
