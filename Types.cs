/*
 * 

 */
using System.Collections.Generic;

namespace Types {
    public class Environment {

    }


    // 单态类型 MonoType
    public abstract class Monomorphic {
        public abstract Monomorphic applySub(Dictionary<Monomorphic, Monomorphic> m);
    }

    // 基础类型（或称原生类型？）
    public class Primitive : Monomorphic {
        public string Name;
        public object kind;
        public Primitive(string name, object kind) {
            Name = name;
        }

        public override Monomorphic applySub(Dictionary<Monomorphic, Monomorphic> m) {
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            return this.Name == ((Primitive)obj).Name;
        }

        public override string ToString() => $"{Name}";
    }

    // 类型变量
    public class Slot : Monomorphic {
        public string Name;
        public Slot (string name) {
            Name = name;
        }

        public override Monomorphic applySub(Dictionary<Monomorphic, Monomorphic> m) {
            if (!m.ContainsKey(this) || ReferenceEquals(m[this], this)) {
                return this;
            }
            var r = m[this];
            return r.applySub(m);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            return this.Name == ((Slot)obj).Name;
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

        public override Monomorphic applySub(Dictionary<Monomorphic, Monomorphic> m) {
            return new Composite(this.Ctor.applySub(m), this.Argument.applySub(m));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var t = (Composite)obj;
            // TODO: write your implementation of Equals() here
            return this.Ctor.Equals(t.Ctor) && this.Argument.Equals(t.Argument);
        }

        public override string ToString() {
            if (Argument.GetType() == typeof(Composite)) {
                return $"{Ctor.ToString()} ({Argument.ToString()})";
            } else {
                return $"{Ctor.ToString()} {Argument.ToString()}";
            }
        }
    }

    public class Polymorphic {

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