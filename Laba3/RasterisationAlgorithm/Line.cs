using System.Numerics;

namespace RasterisationAlgorithm;

public readonly struct Line {
    public Line(Vector2 start, Vector2 end) {
        Start = start;
        End = end;
    }

    public Vector2 Start { get; init; }
    public Vector2 End { get; init; }
}
