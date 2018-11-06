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

        public override string ToString() => Name;
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

    }

    public static class Operations {
        static Dictionary<string, Primitive> PrimitiveTable = new Dictionary<string, Primitive>();
        public static Primitive pm(string name, string kind) {
            if (PrimitiveTable.ContainsKey(name)) return PrimitiveTable[name];
            var t = new Primitive(name, kind);
            PrimitiveTable.Add(name, t);
            return t;
        }
        public static Composite CompositeType(string name, string kind) {
            return new Composite();
        }

        public static void Arrow() {

        }

        public static void Product() {

        }
    }
}