using System.Numerics;
using FillAlgorithm;
using RasterisationAlgorithm;
using SFML.Graphics;
using SFML.Window;

var canvas = new Canvas(160, 160);

float ax = 0, ay = 150;
float bx = 75, by = 0;
float cx = 150, cy = 150;

Triangle test = new Triangle(ax, ay, bx, by, cx, cy);

var drawer = new Drawer();
var drawAlgorithm = new DDA();
var fillAlgorithm = new RA4A();

drawer.Draw(canvas, test, Color.Black, Color.Cyan, drawAlgorithm, fillAlgorithm);

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
    private static Color[] Colors = {
        Color.Yellow,
        Color.Blue,
        Color.Green,
        Color.Cyan,
        Color.Magenta,
        Color.Red
    };

    public void Draw(Canvas canvas, Triangle triangle, Color borderColor, Color fillColor, ILineRasterisationAlgorithm drawAlgorithm, IFillAlgorithm fillAlgorithm) {
        foreach(var line in triangle.TriangleSides()) {
            drawAlgorithm.Draw(canvas, line, borderColor);
        }
        foreach (var line in triangle.TriangleMEfianSides()) {
            drawAlgorithm.Draw(canvas, line, borderColor);
        }

        int i = 0;
        foreach (var point in triangle.MiniTriangleMedianPoints()) {
            fillAlgorithm.Fill(canvas, new((uint)point.X, (uint)point.Y), Colors[i++], borderColor);
        }

        foreach (var line in triangle.CentralFigureSides()) {
            drawAlgorithm.Draw(canvas, line, borderColor);
        }
    }
}