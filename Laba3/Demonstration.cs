using FillAlgorithm;
using SFML.Graphics;
using SFML.Window;

class Demonstration {
    public void Run() {
        var canvasA = new Canvas(160, 160);
        var canvasB = new Canvas(160, 160);

        var drawAlgorithmA = new SIA();
        var fillAlgorithmA = new RA4A();

        var drawAlgorithmB = new DDA();
        var fillAlgorithmB = new LineFillAlgorithm();

        var fillColorsA = new Color[] {
            new Color(128, 0, 128, 128),
            new Color(0, 128, 128, 128),
            new Color(0, 0, 255, 128),
            new Color(14, 255, 75, 128),
            new Color(255, 0, 255, 128),
            new Color(0, 255, 255, 128),
        };

        var fillColorsB = new Color[] {
            new Color(128, 0, 128, 128),
            new Color(0, 128, 128, 128),
            new Color(63, 155, 54, 128),
            new Color(55, 22, 34, 128),
            new Color(14, 255, 75, 128),
            new Color(0, 255, 255, 128),
        };

        var borderColorA = new Color(255, 0, 0, 128);
        var borderColorB = new Color(0, 0, 255, 128);

        var drawerA = new Drawer(drawAlgorithmA, fillAlgorithmA, fillColorsA);
        var drawerB = new Drawer(drawAlgorithmB, fillAlgorithmB, fillColorsB);

        float ax = 0, ay = 150;
        float bx = 75, by = 0;
        float cx = 150, cy = 150;
        Triangle triangle = new Triangle(ax, ay, bx, by, cx, cy);

        drawerA.Draw(canvasA, triangle, borderColorA);
        drawerB.Draw(canvasB, triangle, borderColorB);

        var spriteA = CanvasToSprite(canvasA, 4);
        var spriteB = CanvasToSprite(canvasB, 4);

        DisplayMode mode = DisplayMode.A;

        var window = new RenderWindow(new VideoMode(1200, 800), "Image");
        window.Closed += (_, _) => window.Close();
        window.KeyPressed += (_, e) => {
            mode = e.Code switch {
                Keyboard.Key.Num1 => DisplayMode.A,
                Keyboard.Key.Num2 => DisplayMode.B,
                Keyboard.Key.Num3 => DisplayMode.Both,
                _ => mode,
            };
        };

        while (window.IsOpen) {
            window.DispatchEvents();

            window.Clear(Color.White);

            switch (mode) {
                case (DisplayMode.A):
                    window.Draw(spriteA);
                    break;
                case (DisplayMode.B):
                    window.Draw(spriteB);
                    break;
                case (DisplayMode.Both):
                    window.Draw(spriteA);
                    window.Draw(spriteB);
                    break;
            }

            window.Display();
        }


    }
    Sprite CanvasToSprite(Canvas canvas, int scale) {
        var texture = new Texture(canvas.Image);
        var sprite = new Sprite(texture);
        sprite.Scale *= scale;

        return sprite;
    }

    enum DisplayMode {
        A,
        B,
        Both
    }
}
