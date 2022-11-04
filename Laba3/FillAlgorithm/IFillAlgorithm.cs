using SFML.Graphics;

namespace FillAlgorithm;
public interface IFillAlgorithm {
    void Fill(Canvas canvas, Pixel startPixel, Color fillColor, Color borderColor);
}
