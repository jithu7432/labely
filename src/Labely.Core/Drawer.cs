using SkiaSharp;
using SkiaSharp.QrCode;
namespace Labely.Core;


public static class Drawer {
    public static void DrawLabel(in LabelConfig lc) {
        var info = new SKImageInfo(lc.Canvas.Width, lc.Canvas.Height);
        using var surface = SKSurface.Create(info);

        // Set canvas background
        var parentC = surface.Canvas;
        parentC.Clear(SKColor.Parse(lc.Canvas.Color));

        // Draw margins
        if (lc.Canvas.BorderThickness > 0) {
            parentC.DrawRect(
                    new SKRect(
                        lc.Canvas.BorderOffset,
                        lc.Canvas.BorderOffset,
                        parentC.LocalClipBounds.Width - lc.Canvas.BorderOffset,
                        parentC.LocalClipBounds.Height - lc.Canvas.BorderOffset
                    ),
                    new SKPaint() {
                        Color = SKColor.Parse(lc.Canvas.BorderColor),
                        IsStroke = true,
                        StrokeWidth = lc.Canvas.BorderThickness,
                        StrokeCap = SKStrokeCap.Round
                    }
                );
        }

        using var bitmap = new SKBitmap(lc.Canvas.Width - 2 * lc.Canvas.BorderAll, lc.Canvas.Height - 2 * lc.Canvas.BorderAll);
        var canvas = new SKCanvas(bitmap);

        SKFont font;
        SKPaint paint;
        foreach (var element in lc.Elements) {
            switch (element.Kind) {
                case "qr":
                    using (var bb = new SKBitmap(element.Size, element.Size)) {
                        using (var cv = new SKCanvas(bb)) {
                            var qr = new QRCodeGenerator().CreateQrCode(element.Url, ECCLevel.H);
                            cv.Render(qr, element.Size, element.Size, SKColors.Transparent, SKColor.Parse(element.ForegroundColor), SKColor.Parse(element.BackgroundColor));
                        }
                        canvas.DrawBitmap(bb, element.X, element.Y);
                    }
                    break;
                case "text":
                    font = new SKFont(SKTypeface.FromFamilyName("Times New Roman"), element.Size);
                    paint = new() { IsStroke = false };
                    canvas.DrawText(element.Value, element.X, element.Y, SKTextAlign.Left, font, paint);
                    break;
            }
        }

        parentC.DrawBitmap(bitmap, lc.Canvas.BorderAll, lc.Canvas.BorderAll);


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
