using MLP.Core.Common;
using MLP.Core.Models;
using System;
using System.Collections.Generic;

namespace MLP.Core.Interfaces
{
    public interface IKMeans
    {
        int K { get; set; }

        List<string> RegressionFeatureNames { get; set; }
        string CurrentFeatureX { get; set; }
        string CurrentFeatureY { get; set; }
        List<double> CurrentDataX { get; set; }
        List<double> CurrentDataY { get; set; }
        double MaxX { get; set; }
        double MaxY { get; set; }
        double MinX { get; set; }
        double MinY { get; set; }
        List<Tuple<double, double>> Centroids { get; set; }
        Dictionary<Tuple<double, double>, List<double>> ClustersX { get; set; }
        Dictionary<Tuple<double, double>, List<double>> ClustersY { get; set; }

        int Iteration { get; set; }

        void ConfigService(DataSet dataSet, int k = 2);
        void Train(string featureX, string featureY, bool standardized);
        bool Iterate();
        bool Iterate(int numIterations);
        List<Point> GetBaseSeries();
        List<List<Point>> GetClusterSeries();
    }
}
