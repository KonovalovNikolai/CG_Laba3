using SFML.Graphics;
using FillAlgorithm;

public sealed class RA4A : IFillAlgorithm
{
    public void Fill(Canvas canvas, Pixel startPixel, Color fillColor)
    {
        Color startColor = canvas.GetPixel(startPixel);

        var stack = new Stack<Pixel>();
        stack.Push(startPixel);

        while (stack.Count > 0) {
            startPixel = stack.Pop();

            var pixelColor = canvas.GetPixel(startPixel);
            if (pixelColor == fillColor || pixelColor != startColor)
                continue;

            canvas.SetPixel(startPixel, fillColor);

            if (startPixel.X + 1 < canvas.Image.Size.X) {
                Pixel nPixel = new(startPixel.X + 1, startPixel.Y);
                stack.Push(nPixel);
            }

            if (startPixel.X != 0) {
                Pixel nPixel = new(startPixel.X - 1, startPixel.Y);
                stack.Push(nPixel);
            }

            if (startPixel.Y < canvas.Image.Size.Y) {
                Pixel nPixel = new(startPixel.X, startPixel.Y + 1);
                stack.Push(nPixel);
            }

            if (startPixel.Y != 0) {
                Pixel nPixel = new(startPixel.X, startPixel.Y - 1);
                stack.Push(nPixel);
            }
        }
    }
}

