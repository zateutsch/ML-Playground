using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Common
{
    public class Point : IComparable
    {
        public double X { set; get; }
        public double Y { set; get; }

        public string Label { get; set; }

        public Point(double x, double y, string label = null)
        {
            this.X = x;
            this.Y = y;
            this.Label = label;
        }


        // https://docs.microsoft.com/en-us/dotnet/api/system.icomparable.compareto?view=net-5.0
        // Comparable implemented for sorting in ConcaveHull alg
        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentException("CompareTo object is null.");

            Point point = obj as Point;
            if (point != null)
            {
                return (this.X == point.X) ? this.Y.CompareTo(point.Y) : this.X.CompareTo(point.X);
            }
            else throw new ArgumentException("CompareTo object is not a Point object.");

        }
    }
}
