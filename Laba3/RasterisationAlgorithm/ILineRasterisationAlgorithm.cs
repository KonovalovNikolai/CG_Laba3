using SFML.Graphics;

namespace RasterisationAlgorithm;

public interface ILineRasterisationAlgorithm {
    void Draw(Canvas canvas, Line line, Color color) {

        if (line.Start.X < 0 && line.End.X < 0 && line.Start.Y < 0 && line.End.Y < 0) {
            throw new ArgumentOutOfRangeException();
        }

        int xStartPixel = (int)MathF.Round(line.Start.X);
        int yStartPixel = (int)MathF.Round(line.Start.Y);
        int xEndPixel = (int)MathF.Round(line.End.X);
        int yEndPixel = (int)MathF.Round(line.End.Y);

        double k = (yEndPixel - yStartPixel) / (xEndPixel - xStartPixel);
        double b = yStartPixel - k * xStartPixel;
        for (int x = xStartPixel; x <= xEndPixel; x++)
            canvas.SetPixel((uint)(x), (uint)Math.Round(k * x + b), color);
    }
}
