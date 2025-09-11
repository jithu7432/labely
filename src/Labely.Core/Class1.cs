using SkiaSharp;
namespace Labely.Core;

public static class Drawer {
    public static void Draw() {


        var info = new SKImageInfo(1200, 1800);
        using var surface = SKSurface.Create(info);

        SKPaint paint;

        var canvas = surface.Canvas;
        canvas.Clear(SKColors.AntiqueWhite);
        DrawBorder(canvas, 10);

        var point = new SKPoint(canvas.LocalClipBounds.Width/2, canvas.LocalClipBounds.Height/2);
        var font = new SKFont();
        DrawText(canvas, Guid.NewGuid().ToString(), point, new SKFont(), new SKPaint());

        // save the file
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite("output.png");
        data.SaveTo(stream);
    }

    private static void DrawText(SKCanvas canvas, string text, SKPoint point, SKFont font, SKPaint paint) {
        canvas.DrawText(text, point, SKTextAlign.Center, font, paint);
    }

    private static void DrawBorder(SKCanvas canvas, float offset) {
        canvas.Clear(SKColors.Black);
        var rect = new SKRect(offset, offset, canvas.LocalClipBounds.Width - offset, canvas.LocalClipBounds.Height - offset);
        var paint = new SKPaint() { Color = SKColors.White, IsStroke = false };
        canvas.DrawRect(rect, paint);
    }
}
