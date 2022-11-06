using RasterisationAlgorithm;
using SFML.Graphics;
using System.Numerics;

public sealed class DDA : ILineRasterisationAlgorithm {
    public void Draw(Canvas canvas, Line line, Color color) {
        if (line.Start.X < 0 && line.End.X < 0 && line.Start.Y < 0 && line.End.Y < 0) {
            throw new ArgumentOutOfRangeException();
        }

        int xStartPixel = (int)MathF.Round(line.Start.X);
        int yStartPixel = (int)MathF.Round(line.Start.Y);
        int xEndPixel = (int)MathF.Round(line.End.X);
        int yEndPixel = (int)MathF.Round(line.End.Y);

        int width = Math.Abs(xStartPixel - xEndPixel);
        int height = Math.Abs(yStartPixel - yEndPixel);

        int length = Math.Max(width, height);

        if (length == 0) {
            canvas.SetPixel((uint)xStartPixel, (uint)yStartPixel, color);
        }

        length += 1;

        double dX = (line.End.X - line.Start.X) / length;
        double dY = (line.End.Y - line.Start.Y) / length;

        double x = line.Start.X;
        double y = line.Start.Y;

        while(length > 0) {
            x += dX;
            y += dY;

            var  pixel = new Pixel((uint)Math.Round(x), (uint)Math.Round(y));

            //if (pixel.X >= canvas.Image.Size.X || pixel.Y >= canvas.Image.Size.Y)
            //    break;

            canvas.SetPixel(pixel, color);

            length--;
        } 
    }
}
