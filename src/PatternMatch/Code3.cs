using System;

namespace PatternMatch
{
    // 破棄パターン
    class Code3
    {
        public void Run()
        {
            Console.WriteLine("===== Code3 =====");
            Console.WriteLine($"{Run0(0)}");
            Console.WriteLine($"{Run0(1)}");
            Console.WriteLine($"{Run0("ABC")}");
            Console.WriteLine($"{Run0(null)}");
        }
        private int Run0(object x) => x switch
        {
            0 => 0,
            string s => s.Length,
            _ => -1
        };
    }
}
