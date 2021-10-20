using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Models
{
    // General class wrapper for set of Data Points
    // @author zateutsch

    public class DataSet
    {
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string DefaultFeatureX { get; set; }
        public string DefaultFeatureY { get; set; }
        public string DefaultFeatureLabel { get; set; }

        public int FeatureSize
        {
            get { return this.ClassificationData.Count + this.RegressionData.Count; }
        }
        public Dictionary<string, List<string>> ClassificationData { get; set; }
        public Dictionary<string, List<double>> RegressionData { get; set; }
        public List<string> EnabledModels { get; set; }

    }
}
