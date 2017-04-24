using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicPuzzle
{
    public class LogicGrid<T, U> : Dictionary<Tuple<T, U>, bool?> where T: struct where U: struct
    {
        public LogicGrid(){
            foreach (T t in Enum.GetValues(typeof(T))) {
                foreach (U u in Enum.GetValues(typeof(U))) {
                    this[t, u] = null;
                }
            }
        }

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

        private void updateValues(){
            foreach (T t in Enum.GetValues(typeof(T))) {
                var values = ((IEnumerable<U>)Enum.GetValues(typeof(U)))
                    .Select(u => new Tuple<U, bool?>(u, this[t, u]));
                var allFalse = allFalseP(values);
                if (allFalse.Item1) {
                    this[t, (U)allFalse.Item2] = true;
                }
                var oneTrue = oneTrueP(values);
                if (oneTrue.Item1) {
                    foreach (U u in Enum.GetValues(typeof(U))) {
                        if (! u.Equals(oneTrue.Item2)) {
                            this[t, u] = false;
                        }                        
                    }
                }
            }  
        }

        private static Tuple<bool, V?> allFalseP<V>(IEnumerable<Tuple<V, bool?>> values) where V: struct {
            int count = values.Count();
            int countFalse = values.Count(x => x.Item2 == false);
            if (count == countFalse + 1) {
                return new Tuple<bool, V?>(true, values.First(x => x.Item2 != false).Item1);
            }
            return new Tuple<bool, V?>(false, null);
        }

        private static Tuple<bool, V?> oneTrueP<V>(IEnumerable<Tuple<V, bool?>> values) where V: struct {
            int countTrue = values.Count(x => x.Item2 == true);
            if (countTrue == 1) {
                return new Tuple<bool, V?>(true, values.First(x => x.Item2 == true).Item1);
            }
            return new Tuple<bool, V?>(false, null);
        }
    }
}
