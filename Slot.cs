using System.Collections.Generic;

namespace Types
{
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
            return obj != null && GetType() == obj.GetType() && this.Name == ((Slot)obj).Name;
        }

        public override string ToString() => $"#{Name}";
    }
}