using System.Collections.Generic;

namespace Types {
  // 单态类型 MonoType
  public abstract class Monomorphic {
    public abstract Monomorphic applySub(Dictionary<Monomorphic, Monomorphic> m);
  }
}