using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MLP.Core.Common;
using MLP.Core.Models;

namespace MLP.Core.Interfaces
{

    // TODO
    public interface IClassificationKNN
    {
        List<string> RegressionFeatureNames { get; set; }
        string CurrentFeatureY { get; set; }
        string CurrentFeatureX { get; set; }
        string CurrentFeatureLabel { get; set; }
        void Train(string x_feature, string y_feature, string target_feature);
        string Classify(double x_data, double y_data);
        void ConfigService(DataSet dataSet, int k = 3);
        Dictionary<string, List<DataPoint<double>>> GetLabeledSeries();

    }
}
