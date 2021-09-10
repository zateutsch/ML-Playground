using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.MachineLearning.Models
{
    // General class wrapper for set of Data Points
    // @author zateutsch

    public class DataSet
    {
        public string Name { get; set; }
        public int FeatureSize
        {
            get { return this.ClassificationData.Count + this.RegressionData.Count; }
        }
        public Dictionary<string, List<string>> ClassificationData { get; set; }
        public Dictionary<string, List<double>> RegressionData { get; set; }

    }
}
