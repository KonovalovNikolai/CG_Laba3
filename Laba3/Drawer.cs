using FillAlgorithm;
using RasterisationAlgorithm;
using SFML.Graphics;

public sealed class Drawer {
    private IFillAlgorithm _fillAlgorithm;
    private ILineRasterisationAlgorithm _lineAlgorithm;
    private Color[] _fillColors;

    public Drawer(ILineRasterisationAlgorithm lineAlgorithm, IFillAlgorithm fillAlgorithm, Color[] fillColors) {
        _fillAlgorithm = fillAlgorithm;
        _lineAlgorithm = lineAlgorithm;
        _fillColors = fillColors;
    }

    public void Draw(Canvas canvas, Triangle triangle, Color borderColor) {
        foreach (var line in triangle.TriangleSides()) {
            _lineAlgorithm.Draw(canvas, line, borderColor);
        }
        foreach (var line in triangle.TriangleMEfianSides()) {
            _lineAlgorithm.Draw(canvas, line, borderColor);
        }

        int i = 0;
        foreach (var point in triangle.MiniTriangleMedianPoints()) {
            _fillAlgorithm.Fill(canvas, new((uint)point.X, (uint)point.Y), _fillColors[i++]);
        }

        foreach (var line in triangle.CentralFigureSides()) {
            _lineAlgorithm.Draw(canvas, line, borderColor);
        }
    }
}