using System.Collections.Generic;

namespace Types {
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

    public override bool Equals(object obj) => obj != null && GetType() == obj.GetType() && this.Name == ((Primitive)obj).Name;

    public override string ToString() => $"{Name}";
  }
}