using System.Collections.Generic;

namespace Types {
    // 单态类型 MonoType
    public class Monomorphic {

    }

    // 基础类型（或称原生类型？）
    public class Primitive : Monomorphic {
        public string Name;
        public object kind;
        public Primitive(string name, object kind) {
            Name = name;
        }

        public override string ToString() => $"{Name}";
    }

    // 类型变量
    public class Slot : Monomorphic {
        public string Name;
        public Slot (string name) {
            Name = name;
        }
        public override string ToString() => $"#{Name}";
    }

    public class Composite : Monomorphic {
        public Monomorphic Ctor;
        public Monomorphic Argument;
        public Composite(Monomorphic ctor, Monomorphic argument) {
            Ctor = ctor;
            Argument = argument;
        }
        public override string ToString() {
            if (Argument.GetType() == typeof(Composite)) {
                return $"{Ctor.ToString()} ({Argument.ToString()})";
            } else {
                return $"{Ctor.ToString()} {Argument.ToString()}";
            }
        }
    }

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
    }
}