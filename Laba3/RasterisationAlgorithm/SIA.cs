using RasterisationAlgorithm;
using SFML.Graphics;
using System.Numerics;

public sealed class SIA : ILineRasterisationAlgorithm
{
    public void Draw(Canvas canvas, Line line, Color color)
    {

        double tg = Math.Abs((double)(line.End.Y - line.Start.Y) / (line.End.X - line.Start.X));

        if (line.Start.X > line.End.X && tg < 1 || line.End.Y < line.Start.Y && tg >= 1) {
            line = new Line(new(line.End.X, line.End.Y), new(line.Start.X, line.Start.Y));
        }

        float A = line.End.Y - line.Start.Y;
        float B = -line.End.X + line.Start.X;

        double kx = (double)A / (-B);
        double ky = (double)B / (-A);

        if (tg < 1 || kx == 0) {
            double y = line.Start.Y;

            for (double x = line.Start.X; x <= line.End.X; x++) {
                canvas.SetPixel((uint)x, (uint)y, color);
                y += kx;
            }
        }
        else {
            double x = line.Start.X;

            for (double y = line.Start.Y; y <= line.End.Y; y++) {
                canvas.SetPixel((uint)x, (uint)y, color);
                x += ky;
            }
        }
    }
}