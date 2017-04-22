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
    }
}
