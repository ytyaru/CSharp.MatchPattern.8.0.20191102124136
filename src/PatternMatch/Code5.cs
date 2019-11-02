using System;

namespace PatternMatch
{
    /*
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x = 0, int y = 0) => (X, Y) = (x, y);
        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
    public class Line
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Line(Point p1, Point p2) => (P1, P2) = (p1, p2);
        public void Deconstruct(out Point p1, out Point p2) => (p1, p2) = (P1, P2);
    }
    */
    class X1
    {
        public int GetOnly { get; private set; }
        public int GetSet { get; set; }
        public int Field;
        public int SetOnly { set => GetOnly = value; }
    }
    // プロパティパターン
    class Code5
    {
        public void Run()
        {
            Console.WriteLine("===== Code6 =====");
            Console.WriteLine($"{Run0(new Point(1, 2))}"); // 0
            Console.WriteLine($"{Run0(new Point(1, 3))}"); // 0
            Console.WriteLine($"{Run0(new Point(0, 2))}"); // 0
            Console.WriteLine($"{Run1(new Point(1, 2))}"); // 0
            Run2();
            Run3();
            Run4("ABC");
            Run4(null);
        }
        private int Run0(Point p) => p switch
        {
            { X: 1, Y: 2 } => 0,
            { X: var x, Y: _ } when x > 0 => x,
            _ => -1
        };
        private int Run1(Point p) => p switch
        {
            Point { X: 1, Y: 2 } => 0,
            Point { X: var x, Y: _ } when x > 0 => x,
            _ => -1
        };
        private void Run2()
        {
            // オブジェクト初期化子
            var x = new X1 { GetSet = 1, Field = 2, SetOnly = 3 };
            // プロパティパターン
            Console.WriteLine(x is { GetOnly: 3, GetSet: 1, Field: 2 });
        }
        private void Run3()
        {
            // 初期化子でプロパティ指定できるなら、プロパティ指定でマッチングできるべき
            var p1 = new Point { X = 1, Y = 2 };
            var r1 = p1 is { X: 1, Y: 2 };
             
            // 混在で構築できるなら、混在でマッチングできるべき
            var p2 = new Point(x: 1) { Y = 2 };
            var r2 = p2 is (1, _) { Y: 2 };
        }
        private void Run4(object x)
        {
            if (x is { } nonNull) { Console.WriteLine("非nullだよ。"); }
            else { Console.WriteLine("nullだよ。"); }
        }
    }
}
