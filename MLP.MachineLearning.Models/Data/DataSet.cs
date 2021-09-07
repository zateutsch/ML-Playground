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
        public int Size
        {
            get { return this.ClassificationData.Count != 0 ? this.ClassificationData.Count : this.RegressionData.Count; }
        }
        public Dictionary<string, List<string>> ClassificationData { get; set; }
        public Dictionary<string, List<float>> RegressionData { get; set; }


    }
}
