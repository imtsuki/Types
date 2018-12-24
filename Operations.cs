using System.Collections.Generic;

namespace Types {
  public static class Operations {
    static Dictionary<string, Slot> SlotTable = new Dictionary<string, Slot>();

    public static Slot slot(string name) {
      if (SlotTable.ContainsKey(name)) return SlotTable[name];
      var t = new Slot(name);
      SlotTable.Add(name, t);
      return t;
    }

    static Dictionary<string, Primitive> PrimitiveTable = new Dictionary<string, Primitive>();
    public static Primitive pm(string name, string kind = "") {
      if (PrimitiveTable.ContainsKey(name)) return PrimitiveTable[name];
      var t = new Primitive(name, kind);
      PrimitiveTable.Add(name, t);
      return t;
    }
    public static Composite ct(Monomorphic ctor, Monomorphic argument) {
      return new Composite(ctor, argument);
    }
    public static Composite Arrow(Monomorphic p, Monomorphic q) {
      return ct(ct(pm("[->]"), p), q);
    }
    public static Composite Product(Monomorphic p, Monomorphic q) {
      return ct(ct(pm("[*]"), p), q);
    }

    public static bool unify(Dictionary<Monomorphic, Monomorphic> m, Monomorphic s, Monomorphic t) {
      if (s is Slot && t is Slot && s.applySub(m).Equals(t.applySub(m))) {
        return true;
      } else if (s is Primitive && t is Primitive && ((Primitive)s).Name == ((Primitive)t).Name) {
        return true;
      } else if (s is Composite && t is Composite) {
        var s_ = (Composite)s;
        var t_ = (Composite)t;
        return unify(m, s_.Ctor, t_.Ctor) && unify(m, s_.Argument, t_.Argument);
      } else if (s is Slot) {
        m.Add(s, t);
        return true;
      } else if (t is Slot) {
        m.Add(t, s);
        return true;
      } else {
        return false;
      }
    }
  }
}