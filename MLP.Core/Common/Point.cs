using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Common
{
    public class Point
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
    }
}
