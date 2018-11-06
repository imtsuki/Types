using System;
using static Types.Operations;

namespace Types
{
    class Program
    {
        static void Main(string[] args)
        {
            var type1 = Arrow(
                Product(
                    Arrow(slot("a1"), slot("a2")),
                    ct(pm("list"), slot("a3"))
                ),
                ct(pm("list"), slot("a2"))
            );
            var type2 = Arrow(
                Product(
                    Arrow(slot("a3"), slot("a4")),
                    ct(pm("list"), slot("a3"))
                ),
                slot("a5")
            );
            Console.WriteLine($"Type1 :: {type1.ToString()}");
            Console.WriteLine($"Type2 :: {type2.ToString()}");
        }
    }
}
