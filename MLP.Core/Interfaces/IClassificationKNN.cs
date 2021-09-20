using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Common;

namespace MLP.Core.Interfaces
{

    // TODO
    public interface IClassificationKNN
    {
        void Train(string x_feature, string y_feature, string target_feature);
        string Classify(double x_data, double y_data);
        Dictionary<string, List<DataPoint<double>>> GetLabeledSeries();
    }
}
