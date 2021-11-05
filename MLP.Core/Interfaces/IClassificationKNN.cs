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
        int K { get; set; }
        string CurrentFeatureY { get; set; }
        string CurrentFeatureX { get; set; }
        string CurrentFeatureLabel { get; set; }
        List<double> CurrentDataX { get; set; }
        List<double> CurrentDataY { get; set; }
        double MaxX { get; }
        double MaxY { get; }
        double MinX { get; }
        double MinY { get; }
        void Train(string x_feature, string y_feature, string target_feature, bool isStandardized);
        string Classify(double x_data, double y_data);
        Tuple<string, Dictionary<int, double>> RobustClassify(double x, double y);
        void ConfigService(DataSet dataSet, int k = 3);
        Dictionary<string, int> Counts { get; set; }
        Dictionary<string, List<Point>> GetLabeledSeries();
    }
}
