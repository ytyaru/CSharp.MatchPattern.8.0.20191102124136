using System;
//using System.Drawing;

namespace PatternMatch
{
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
    class X
    {
        public void Deconstruct() { }
        public void Deconstruct(out int a) => a = 0;
    }
    // 位置パターン
    class Code4
    {
        public void Run()
        {
            Console.WriteLine("===== Code4 =====");
            Console.WriteLine($"{Run0(new Point(1, 2))}"); // 0
            Console.WriteLine($"{Run0(new Point(1, 3))}"); // 1
            Console.WriteLine($"{Run0(new Point(0, 0))}"); // -1
            
            Console.WriteLine($"{Run1(new Point(1, 2))}"); // 0
            Console.WriteLine($"{Run2(new Point(1, 2))}"); // 0
            Run3();
            Console.WriteLine($"{Run4(1, 1)}"); // 0
            Console.WriteLine($"{Run4(1, 2)}"); // -1
            Console.WriteLine($"{Run4(2, 1)}"); // 1
            Console.WriteLine($"{Run4(null, null)}"); // -1
            Console.WriteLine($"{Run4(null, 1)}"); // -1
            Console.WriteLine($"{Run4(1, null)}"); // -1
            Run5(new X());
        }
        private int Run0(Point p) => p switch
        {
            (1, 2) => 0,
            (var x, _) when x > 0 => x,
            _ => -1
        };
        private int Run1(Point p) => p switch
        {
            Point(1, 2) => 0,
            Point(var x, _) when x > 0 => x,
            _ => -1
        };
        private int Run2(Point p) => p switch
        {
            (x: 1, y: 2) => 0,
            (x: var x, y: _) when x > 0 => x,
            _ => -1
        };
        private void Run3()
        {
            // 位置指定で構築できるんなら、位置指定でマッチングできるべき
            var p1 = new Point(1, 2);
            var r1 = p1 is Point (1, 2);
             
            // 名前指定で構築できるんなら、名前指定でマッチングできるべき
            var p2 = new Point(x: 1, y: 2);
            var r2 = p2 is Point (x: 1, y: 2);
             
            // 型推論が効く場合に new の後ろの型名は省略可能(になる予定)なら
            // 型が既知なら型名を省略してマッチングできるべき
//            Point p3 = new (1, 2); // error CS1031: 型が必要です。
            Point p3 = new Point(1, 2);
            var r3 = p3 is (1, 2);
             
            // 階層的に new できるんなら、階層的にマッチングできるべき
            var line = new Line(new Point(1, 2), new Point(3, 4));
            var r4 = line is ((1, 2), (3, 4));

            Console.WriteLine($"{r1}, {r2}, {r3}, {r4}");
        }
        private int Run4(int? a, int? b)
        {
            switch (a, b)
            {
                case (null, null): return 0;
                case (int _, null): return -1;
                case (null, int _): return -1;
                case (int a1, int b1): return a1.CompareTo(b1);
            }
        }
        public void Run5(X x)
        {
            // 0 引数の位置パターン。Deconstruct()必須
            if (x is ()) Console.WriteLine("Deconstruct()");
     
            // 1 引数の位置パターン。Deconstruct(out T)必須
            // キャストの () との区別が難しいらしく、素直に x is (int a) とは書けない。
            // 前後に余計な var や _ を付ける必要あり。
            if (x is (int a) _) Console.WriteLine($"Deconstruct({a})");
        }
    }
}
