using SFML.Graphics;
using FillAlgorithm;

public sealed class RA4A : IFillAlgorithm
{
    public void Fill(Canvas canvas, Pixel startPixel, Color fillColor, Color borderColor)
    {
        var pixelColor = canvas.GetPixel(startPixel);
        if (pixelColor == fillColor || pixelColor == borderColor) return;

        canvas.SetPixel(startPixel, fillColor);
        var nPixel = (Pixel)new(startPixel.X + 1, startPixel.Y);
        Fill(canvas, nPixel, fillColor, borderColor);

        nPixel = (Pixel)new(startPixel.X - 1, startPixel.Y);
        Fill(canvas, nPixel, fillColor, borderColor);

        nPixel = (Pixel)new(startPixel.X, startPixel.Y + 1);
        Fill(canvas, nPixel, fillColor, borderColor);

        nPixel = (Pixel)new(startPixel.X, startPixel.Y - 1);
        Fill(canvas, nPixel, fillColor, borderColor);
    }
}

