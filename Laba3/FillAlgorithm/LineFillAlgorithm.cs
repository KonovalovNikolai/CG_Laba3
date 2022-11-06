using SFML.Graphics;

namespace FillAlgorithm;

public sealed class LineFillAlgorithm : IFillAlgorithm {
    public void Fill(Canvas canvas, Pixel startPixel, Color fillColor) {
        var stack = new Stack<Pixel>();
        stack.Push(startPixel);

        Color startColor = canvas.GetPixel(startPixel);

        while (stack.Count > 0) {
            var stackPixel = stack.Pop();
            uint y = stackPixel.Y;

            canvas.SetPixel(stackPixel, fillColor);

            uint rX = stackPixel.X + 1;
            var pixelColor = canvas.GetPixel(rX, y);
            while (pixelColor == startColor) {
                canvas.SetPixel(rX, y, fillColor);

                if (rX >= canvas.Image.Size.X)
                    break;

                rX += 1;
                pixelColor = canvas.GetPixel(rX, y);
            }

            uint lX = stackPixel.X - 1;
            pixelColor = canvas.GetPixel(lX, y);
            while (pixelColor == startColor) {
                canvas.SetPixel(lX, y, fillColor);

                if (lX == 0)
                    break;

                lX -= 1;
                pixelColor = canvas.GetPixel(lX, y);
            }

            if (stackPixel.Y < canvas.Image.Size.Y)
                Scan(canvas, stackPixel.Y + 1, lX + 1, rX - 1, stack, fillColor, startColor);

            if (stackPixel.Y > 0)
                Scan(canvas, stackPixel.Y - 1, lX + 1, rX - 1, stack, fillColor, startColor);
        }

    }

    private void Scan(Canvas canvas, uint y, uint lx, uint rx, Stack<Pixel> stack, Color fillColor, Color startColor) {
        for (uint x = lx; x <= rx; x++) {
            var color = canvas.GetPixel(x, y);
            if (color == fillColor)
                return;

            if (color != startColor) {
                continue;
            }

            stack.Push(new Pixel(x, y));
            return;
        }
    }
}
