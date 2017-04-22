using System;
using System.Collections.Generic;

namespace LogicPuzzle
{
    public class LogicGrid<T, U> : Dictionary<Tuple<T, U>, bool?>
    {
        public void SetFalse(T t, U u)
        {
            this[new Tuple<T, U>(t, u)] = false;
        }

        public bool? this[T t, U u]
        {
            get => this[new Tuple<T, U>(t, u)];
            set => this[new Tuple<T, U>(t, u)] = value;
        }

        public new bool? this[Tuple<T, U> x]
        {
            get { return base[x]; }
            set {
                base[x] = value;
                if (value == true){
                    foreach (T t in Enum.GetValues(typeof(T))) {
                        if (! t.Equals(x.Item1)) {
                            this[t, x.Item2] = false;    
                        }
                    }
                    foreach (U u in Enum.GetValues(typeof(U))) {
                        if (! u.Equals(x.Item2)) {
                            this[x.Item1, u] = false;    
                        }
                    }
                }
            }
        }
    }
}
