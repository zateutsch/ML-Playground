using System;
using System.Collections.Generic;

namespace MLP.MachineLearning.Models
{

    // Basic data point class for use by all models
    // @author zateutsch

    public class DataPoint
    {
        public Dictionary<string, string> ClassificationFeatures { get; set; }
        public Dictionary<string, float> RegressionFeatures { get; set; }

    }
}
