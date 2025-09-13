using System.Text.Json.Serialization;
using SkiaSharp;
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

    [JsonPropertyName("border_pixels")]
    public int BorderThickness { get; set; }

    [JsonPropertyName("border_color")]
    public string BorderColor { get; set; } = "#FFFFFFFF";

}

public record struct LabelConfig {
    [JsonPropertyName("canvas")]
    public required CanvasConfig Canvas { get; set; }
}

public static class Drawer {
    public static void DrawLabel(in LabelConfig lc) {
        var info = new SKImageInfo(lc.Canvas.Width, lc.Canvas.Height);
        using var surface = SKSurface.Create(info);

        // Set canvas background
        var canvas = surface.Canvas;
        canvas.Clear(SKColor.Parse(lc.Canvas.Color));

        // Draw margins
        if (lc.Canvas.BorderThickness > 0) {
            canvas.DrawRect(
                    new SKRect(
                        lc.Canvas.BorderOffset,
                        lc.Canvas.BorderOffset,
                        canvas.LocalClipBounds.Width - lc.Canvas.BorderOffset,
                        canvas.LocalClipBounds.Height - lc.Canvas.BorderOffset
                    ),
                    new SKPaint() {
                        Color = SKColor.Parse(lc.Canvas.BorderColor),
                        IsStroke = true,
                        StrokeWidth = lc.Canvas.BorderThickness,
                        StrokeCap = SKStrokeCap.Round
                    }
                );
        }

        //
        // var paint = new SKPaint() {
        //     Color = SKColors.DarkGreen, StrokeWidth = 10
        // };
        //
        // const int offset = 50;
        //
        //
        //
        // var point = new SKPoint(canvas.LocalClipBounds.Width / 2, canvas.LocalClipBounds.Height / 2);
        // var font = new SKFont(SKTypeface.FromFamilyName("Times New Roman"), 50);
        // canvas.DrawText(Guid.NewGuid().ToString(), point, SKTextAlign.Center, font, paint);
        //
        //
        // var a = new SKPoint(offset, canvas.LocalClipBounds.Height / 2 - 200);
        // var b = new SKPoint(canvas.LocalClipBounds.Width - offset, canvas.LocalClipBounds.Height / 2 - 200);
        // canvas.DrawLine(a, b, paint);
        //
        // a = new SKPoint(offset, canvas.LocalClipBounds.Height / 2 + 200);
        // b = new SKPoint(canvas.LocalClipBounds.Width - offset, canvas.LocalClipBounds.Height / 2 + 200);
        // canvas.DrawLine(a, b, paint);
        //
        //
        //
        // point.Offset(new SKPoint(-400, -400));
        // canvas.DrawText("PACKAGE DETAILS: ", point, SKTextAlign.Left, font, paint);
        //
        // point.Offset(new SKPoint(0, +400));
        // point.Offset(new SKPoint(0, +400));
        // canvas.DrawText("FROM: ", point, SKTextAlign.Left, font, paint);
        //
        // point.Offset(new SKPoint(0, +400));
        // canvas.DrawText("TO: ", point, SKTextAlign.Left, font, paint);

        // Save for DEBUG purposes
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 1);
        using var stream = File.OpenWrite("output.png");
        data.SaveTo(stream);
    }
}
