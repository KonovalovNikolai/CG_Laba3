using System.Numerics;
using FillAlgorithm;
using RasterisationAlgorithm;
using SFML.Graphics;
using SFML.Window;

var canvas = new Canvas(160, 160);

var triangle = new Line[] {
    new Line(new(75, 0), new(0, 150)),
    new Line(new(75, 0), new(150, 150)),
    new Line(new(0, 150), new(150, 150))
};

var drawer = new Drawer();
var drawAlgorithm = new DDA();
var fillAlgorithm = new LineFillAlgorithm();

drawer.Draw(canvas, triangle, Color.Black, Color.Cyan, drawAlgorithm, fillAlgorithm);
fillAlgorithm.Fill(canvas, new(75, 120), Color.Cyan, Color.Black);

var texture = new Texture(canvas.Image);
var sprite = new Sprite(texture);
sprite.Scale *= 3;

var window = new RenderWindow(new VideoMode(1200, 600), "Image");
window.Closed += (_, _) => window.Close();

while (window.IsOpen) {
    window.DispatchEvents();

    window.Clear(Color.White);
    window.Draw(sprite);
    window.Display();
}

public sealed class Drawer {
    public void Draw(Canvas canvas, Line[] lines, Color borderColor, Color fillColor, ILineRasterisationAlgorithm drawAlgorithm, IFillAlgorithm fillAlgorithm) {
        foreach (var line in lines) {
            drawAlgorithm.Draw(canvas, line, borderColor);
        }
    }
}

public sealed class Result {
    // Линии основного триугольника и медиан
    public Line[] trianglesLines;
    // Точки центров маленьких треугольников
    public Vector2[] fillPoints;
    // Линии центральной фигуры
    public Line[] centraleLines;
}