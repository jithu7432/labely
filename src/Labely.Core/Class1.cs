using SkiaSharp;
namespace Labely.Core;

public static class Drawer {
    public static void Draw() {
        var info = new SKImageInfo(1200, 1800);
        using var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;

        // make sure the canvas is blank
        canvas.Clear(SKColors.Bisque);

        var coord = new SKPoint(info.Width / 2, (info.Height) / 2);
        canvas.DrawText($"{Guid.NewGuid()}", coord, SKTextAlign.Center, new SKFont(), new SKPaint());


        var paint = new SKPaint() {
            Color = SKColors.Black, IsStroke = true
        };

        var rect = new SKRect(
            0, 1200, 1800, 0
        );
        canvas.DrawRect(rect, paint);

        // save the file
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite("output.png");
        data.SaveTo(stream);
    }
}
