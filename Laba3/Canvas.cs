using SFML.Graphics;

public sealed class Canvas {
    private Image _image;

    public Image Image => _image;

    public Canvas(uint width, uint height) {
        _image = new Image(width, height, Color.Transparent);
    }

    public Canvas(uint width, uint height, Color fillColor) {
        _image = new Image(width, height, fillColor);
    }

    public Color GetPixel(uint x, uint y) {
        return _image.GetPixel(x, y);
    }

    public Color GetPixel(Pixel pixel) {
        return GetPixel(pixel.X, pixel.Y);
    }

    public void SetPixel(uint x, uint y, Color color) {
        _image.SetPixel(x, y, color);
    }

    public void SetPixel(Pixel pixel, Color color) {
        SetPixel(pixel.X, pixel.Y, color);
    }

    public bool IsInBound(uint x, uint y) {
        return _image.Size.X <= x && _image.Size.Y <= y;
    }
}

public readonly struct Pixel {
    public Pixel(uint x, uint y) {
        X = x;
        Y = y;
    }

    public uint X { get; init; }
    public uint Y { get; init; }
}