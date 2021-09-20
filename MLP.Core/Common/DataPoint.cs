using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Common
{
    public class DataPoint<T>
    {
        public T X { set; get; }
        public T Y { set; get; }

        public string Label { get; set; }

        public DataPoint(T x, T y, string label = null)
        {
            this.X = x;
            this.Y = y;
            this.Label = label;
        }
    }
}
