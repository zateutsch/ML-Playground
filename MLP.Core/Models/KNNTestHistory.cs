using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Models
{
    public class KNNTest
    {
        public string XFeature { get; set; }
        public string YFeature { get; set; }
        public string LabelFeature { get; set; }
        public int K { get; set; }
        public double XValue { get; set; }
        public double YValue { get; set; }

        public KNNTest(string xFeature, string yFeature, string labelFeature, int k, double xVal, double yVal)
        {
            this.XFeature = xFeature;
            this.YFeature = yFeature;
            this.LabelFeature = labelFeature;
            this.K = k;
            this.XValue = xVal;
            this.YValue = yVal;
        }
    }
}
