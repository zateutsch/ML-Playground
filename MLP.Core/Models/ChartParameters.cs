using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Models
{
    public class ChartParameters
    {
        public string XFeatureName { get; set; }
        public string YFeatureName { get; set; }
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }

        public ChartParameters(string xf, string yf, double minX, double minY, double maxX, double maxY)
        {
            this.XFeatureName = xf;
            this.YFeatureName = yf;
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
        }
    }
}
