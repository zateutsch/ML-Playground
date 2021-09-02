using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.MachineLearning.Models
{
    // General class wrapper for set of Data Points
    // @author zateutsch

    public class DataSet
    {
        string Name { get; set; }
        public List<DataPoint> Data { get; set; }
    }
}
