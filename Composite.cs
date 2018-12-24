using System.Collections.Generic;

namespace Types
{
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
}