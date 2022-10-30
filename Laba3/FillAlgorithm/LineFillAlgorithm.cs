using SFML.Graphics;

namespace FillAlgorithm;

public sealed class LineFillAlgorithm : IFillAlgorithm {
    public void Fill(Canvas canvas, Pixel startPixel, Color fillColor, Color borderColor) {
        var stack = new Stack<Pixel>();
        stack.Push(startPixel);

        while (stack.Count > 0) {
            var stackPixel = stack.Pop();

            canvas.SetPixel(stackPixel, fillColor);

            var rPixel = RightPixel(stackPixel);
            var pixelColor = canvas.GetPixel(rPixel);
            while (pixelColor != borderColor) {
                canvas.SetPixel(rPixel, fillColor);

                rPixel = RightPixel(rPixel);

                if (rPixel.X >= canvas.Image.Size.X)
                    break;

                pixelColor = canvas.GetPixel(rPixel);
            }

            var lPixel = LeftPixel(stackPixel);
            pixelColor = canvas.GetPixel(lPixel);
            while (pixelColor != borderColor) {
                canvas.SetPixel(lPixel, fillColor);

                if (lPixel.X == 0)
                    break;

                lPixel = LeftPixel(lPixel);
                pixelColor = canvas.GetPixel(lPixel);
            }

            if (stackPixel.Y < canvas.Image.Size.Y)
                Scan(canvas, stackPixel.Y + 1, lPixel.X + 1, rPixel.X - 1, stack, fillColor, borderColor);

            if (stackPixel.Y > 0)
                Scan(canvas, stackPixel.Y - 1, lPixel.X + 1, rPixel.X - 1, stack, fillColor, borderColor);
        }

    }

    private void Scan(Canvas canvas, uint y, uint lx, uint rx, Stack<Pixel> stack, Color fillColor, Color borderColor) {
        for (uint x = lx; x <= rx; x++) {
            var color = canvas.GetPixel(x, y);
            if (color == fillColor)
                return;

            if (color == borderColor) {
                continue;
            }

            stack.Push(new Pixel(x, y));
            return;
        }
    }

    Pixel RightPixel(Pixel pixel) {
        return new(pixel.X + 1, pixel.Y);
    }

    Pixel LeftPixel(Pixel pixel) {
        return new(pixel.X - 1, pixel.Y);
    }
}
