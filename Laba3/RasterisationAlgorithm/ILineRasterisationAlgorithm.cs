using SFML.Graphics;

namespace RasterisationAlgorithm;

public interface ILineRasterisationAlgorithm {
    void Draw(Canvas canvas, Line line, Color color);
}
