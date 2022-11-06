using System.Numerics;
using RasterisationAlgorithm;

public class Triangle {
    // точки
    public Vector2 A { get; private set; }
    public Vector2 B { get; private set; }
    public Vector2 C { get; private set; }

    // медианы
    public Vector2 m_AB { get; private set; }
    public Vector2 m_BC { get; private set; }
    public Vector2 m_CA { get; private set; }

    // точка пересечения медиан
    public Vector2 m_Cross { get; private set; }

    public Triangle(float Ax, float Ay, float Bx, float By, float Cx, float Cy) {
        A = new Vector2(Ax, Ay);
        B = new Vector2(Bx, By);
        C = new Vector2(Cx, Cy);

        m_AB = CalcMedian(A, B);
        m_BC = CalcMedian(B, C);
        m_CA = CalcMedian(C, A);

        m_Cross = CalcMedianCrossing(C, m_AB);
        //Console.WriteLine(m_Cross.X + "  " + m_Cross.Y);
    }

    public Triangle (float lenght, float height) : this(0, height, lenght / 2, 0, lenght, height) {

    }

    private Vector2 CalcMedian(Vector2 point1, Vector2 point2) {
        return new Vector2((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
    }

    private Vector2 CalcMedianCrossing(Vector2 point1, Vector2 point2) {
        float x = (point1.X + 2 * point2.X) / 3;
        float y = (point1.Y + 2 * point2.Y) / 3;
        return new Vector2(x, y);
    }

    public Triangle[] TrianglesArray() {
        return new Triangle[] {
        new Triangle(A.X, A.Y, m_AB.X, m_AB.Y, m_Cross.X, m_Cross.Y),    //1
        new Triangle(m_AB.X, m_AB.Y, B.X, B.Y, m_Cross.X, m_Cross.Y),    //2
        new Triangle(B.X, B.Y, m_BC.X, m_BC.Y, m_Cross.X, m_Cross.Y),    //3
        new Triangle(m_BC.X, m_BC.Y, C.X, C.Y, m_Cross.X, m_Cross.Y),    //4
        new Triangle(C.X, C.Y, m_CA.X, m_CA.Y, m_Cross.X, m_Cross.Y),    //5
        new Triangle(m_CA.X, m_CA.Y, A.X, A.Y, m_Cross.X, m_Cross.Y)     //6
      };
    }

    public Line[] TriangleSides() {
        return new Line[] {
        new Line(A, B),
        new Line(B, C),
        new Line(C, A)
      };
    }

    public Vector2[] MiniTriangleMedianPoints() {
        Vector2[] tmp = new Vector2[6];
        int i = 0;
        foreach (Triangle mini in this.TrianglesArray()) {
            tmp[i] = mini.m_Cross;
            i++;
        }
        return tmp;
    }

    public Line[] TriangleMEfianSides() {
        return new Line[] {
        new Line(A, m_BC),
        new Line(B, m_CA),
        new Line(C, m_AB)
      };
    }

    public Line[] CentralFigureSides() {
        Vector2[] tmp = this.MiniTriangleMedianPoints();
        return new Line[] {
        new Line(tmp[0], tmp[1]),
        new Line(tmp[1], tmp[2]),
        new Line(tmp[2], tmp[3]),
        new Line(tmp[3], tmp[4]),
        new Line(tmp[4], tmp[5]),
        new Line(tmp[5], tmp[0])
      };

    }
}