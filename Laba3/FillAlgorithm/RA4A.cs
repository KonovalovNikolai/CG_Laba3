using SFML.Graphics;
using FillAlgorithm;

public sealed class RA4A : IFillAlgorithm
{
    public void Fill(Canvas canvas, Pixel startPixel, Color fillColor)
    {
        Color startColor = canvas.GetPixel(startPixel);
        Fill(canvas, startPixel, fillColor, startColor);
    }

    private void Fill(Canvas canvas, Pixel startPixel, Color fillColor, Color startColor) {
        var pixelColor = canvas.GetPixel(startPixel);
        if (pixelColor == fillColor || pixelColor != startColor)
            return;

        canvas.SetPixel(startPixel, fillColor);
        var nPixel = (Pixel)new(startPixel.X + 1, startPixel.Y);
        Fill(canvas, nPixel, fillColor, startColor);

        nPixel = (Pixel)new(startPixel.X - 1, startPixel.Y);
        Fill(canvas, nPixel, fillColor, startColor);

        nPixel = (Pixel)new(startPixel.X, startPixel.Y + 1);
        Fill(canvas, nPixel, fillColor, startColor);

        nPixel = (Pixel)new(startPixel.X, startPixel.Y - 1);
        Fill(canvas, nPixel, fillColor, startColor);
    }
}

