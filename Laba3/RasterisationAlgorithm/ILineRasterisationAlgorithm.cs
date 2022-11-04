using SFML.Graphics;
using System.Drawing;
using Color = SFML.Graphics.Color;

namespace RasterisationAlgorithm;

public interface ILineRasterisationAlgorithm {
    void Draw(Canvas canvas, Line line, Color color) {}
}
