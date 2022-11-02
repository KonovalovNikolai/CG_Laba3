using RasterisationAlgorithm;
using SFML.Graphics;
using System.Numerics;

public sealed class SIA : ILineRasterisationAlgorithm
{
    public void Draw(Canvas canvas, Line line, Color color)
    {
        if (line.Start.X < 0 && line.End.X < 0 && line.Start.Y < 0 && line.End.Y < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        int xStartPixel = (int)MathF.Round(line.Start.X);
        int yStartPixel = (int)MathF.Round(line.Start.Y);
        int xEndPixel = (int)MathF.Round(line.End.X);
        int yEndPixel = (int)MathF.Round(line.End.Y);

        int width = Math.Abs(xStartPixel - xEndPixel);
        int height = Math.Abs(yStartPixel - yEndPixel);

        int length = Math.Max(width, height);

        if (length == 0)
        {
            canvas.SetPixel((uint)xStartPixel, (uint)yStartPixel, color);
        }
        else
        {
            double k = 1.0 * (yEndPixel - yStartPixel) / (xEndPixel - xStartPixel);
            double b = yStartPixel - k * xStartPixel;
            int flag;
            
            if (width > height)
            {
                int x = xStartPixel;
                if (xStartPixel > xEndPixel) flag = -1;
                else flag = 1;

                for (int i = 0; i < length; i++)
                {
                    canvas.SetPixel((uint)(x), (uint)Math.Round(k * x + b), color);
                    x += flag;
                }
            }
            else
            {
                int y = yStartPixel;
                if (yStartPixel > yEndPixel) flag = -1;
                else flag = 1;

                for (int i = 0; i < length; i++)
                {
                    canvas.SetPixel((uint)Math.Round((y - b) / k), (uint)(y), color);
                    y += flag;
                }
            }
        }
        
    }
}