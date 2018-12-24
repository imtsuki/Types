using System;
using System.Collections.Generic;
using static Types.Operations;

namespace Types {
  class Program {
    static void Main(string[] args) {
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
      var m = new Dictionary<Monomorphic, Monomorphic>();
      foreach(var key in m.Keys) {
        Console.Write($"{key}=>{m[key]}, ");
      }
      Console.WriteLine();
      Console.WriteLine(unify(m, type1, type2));
      foreach(var key in m.Keys) {
        Console.Write($"{key}=>{m[key]}, ");
      }
      Console.WriteLine();
      Console.WriteLine(type1.applySub(m).ToString());
      Console.WriteLine(type2.applySub(m).ToString());
    }
  }
}
