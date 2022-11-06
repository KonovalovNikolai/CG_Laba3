using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;
using FillAlgorithm;
using Iced.Intel;
using RasterisationAlgorithm;
using SFML.Graphics;
using SFML.Window;

//Demonstration program = new Demonstration();
//program.Run();

BenchmarkRunner.Run<LineRasterisationAlgorithm_StraightLineTest>();
//BenchmarkRunner.Run<LineRasterisationAlgorithm_LineAt60DegreesTest>();
//BenchmarkRunner.Run<LineRasterisationAlgorithm_TriangleTest>();

//BenchmarkRunner.Run<FillAlgorithm_SquareTest>();

//var canvasA = new Canvas(100, 100);
//var canvasB = new Canvas(100, 100);

//var fillAlgorithmA = new RA4A();
//fillAlgorithmA.Fill(canvasA, new(50, 50), Color.Black);

//var fillAlgorithmB = new LineFillAlgorithm();
//fillAlgorithmB.Fill(canvasB, new(50, 50), Color.Black);

Console.ReadLine();

public class LineRasterisationAlgorithm_StraightLineTest {
    private readonly Canvas _canvas = new Canvas(500, 500);

    private readonly SIA algorithmA = new SIA();
    private readonly DDA algorithmB = new DDA();

    private readonly Color _color = Color.Black;

    [Params(5, 10, 100)]
    public int lineLenght;
    public Line line;

    [GlobalSetup]
    public void SetUp() {
        line = new(new(0, 0), new(lineLenght, 0));
    }

    [Benchmark(Baseline = true)]
    public void SIA() {
        algorithmA.Draw(_canvas, line, _color);
    }

    [Benchmark]
    public void DDA() {
        algorithmB.Draw(_canvas, line, _color);
    }
}
public class LineRasterisationAlgorithm_LineAt60DegreesTest {
    private readonly Canvas _canvas = new Canvas(500, 500);

    private readonly SIA algorithmA = new SIA();
    private readonly DDA algorithmB = new DDA();

    private readonly Color _color = Color.Black;

    [Params(5, 10, 100)]
    public int lineLenght;
    public Line line;

    [GlobalSetup]
    public void SetUp() {
        float nx = (float)(lineLenght * Math.Cos(Math.PI / 3));
        float ny = (float)(lineLenght * Math.Sin(Math.PI / 3));
        line = new(new(0, 0), new(nx, ny));
    }

    [Benchmark(Baseline = true)]
    public void SIA() {
        algorithmA.Draw(_canvas, line, _color);
    }

    [Benchmark]
    public void DDA() {
        algorithmB.Draw(_canvas, line, _color);
    }
}
public class LineRasterisationAlgorithm_TriangleTest {
    private readonly SIA algorithmA = new SIA();
    private readonly DDA algorithmB = new DDA();

    private readonly Color _color = Color.Blue;

    private Canvas _canvas;

    private Line[] _triangleSides;
    private Line[] _centerSides;
    private Line[] _meSides;

    [Params(100, 200, 300)]
    public int triangle_height;

    [Params(100, 200, 300)]
    public int triangle_lenght;

    [GlobalSetup]
    public void SetUp() {
        _canvas = new Canvas(400, 400);
        var triangle = new Triangle(triangle_lenght, triangle_height);

        _triangleSides = triangle.TriangleSides();
        _meSides = triangle.TriangleMEfianSides();
        _centerSides = triangle.CentralFigureSides();
    }

    private void Draw(ILineRasterisationAlgorithm algorithm) {
        foreach (var line in _triangleSides) {
            algorithm.Draw(_canvas, line, _color);
        }
        foreach (var line in _meSides) {
            algorithm.Draw(_canvas, line, _color);
        }
        foreach (var line in _centerSides) {
            algorithm.Draw(_canvas, line, _color);
        }
    }

    [Benchmark(Baseline = true)]
    public void AlgorithmA() {
        Draw(algorithmA);
    }

    [Benchmark]
    public void AlgorithmB() {
        Draw(algorithmB);
    }
}

public class FillAlgorithm_SquareTest {
    private readonly RA4A algorithmA = new RA4A();
    private readonly LineFillAlgorithm algorithmB = new LineFillAlgorithm();

    private readonly Color _color = Color.Blue;

    private Canvas _canvas;

    //[Params(100, 200, 300)]
    [Params(100)]
    public int Side_Of_Square;

    private Pixel _startPoint;

    [GlobalSetup]
    public void SetUp() {
        _canvas = new Canvas((uint)Side_Of_Square, (uint)Side_Of_Square);
        _startPoint = new((uint)Side_Of_Square / 2, (uint)Side_Of_Square / 2);
    }

    [Benchmark(Baseline = true)]
    public void AlgorithmA() {
        algorithmA.Fill(_canvas, _startPoint, _color);
    }

    [Benchmark]
    public void AlgorithmB() {
        algorithmB.Fill(_canvas, _startPoint, _color);
    }
}
