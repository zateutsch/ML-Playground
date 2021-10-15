using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;
using MLP.Core.Common;
using MLP.Core.Interfaces;


namespace MLP.Core.Services
{
    public class KMeansClusterService
    {
        // Services //
        private readonly IDataSetService _dataSetService;
        private readonly IMathHelper _mathHelper;

        // Model Parameters //
        public int K { get; set; }

        // Data Management //
        public DataSet Data { get; set; }
        public List<string> RegressionFeatureNames { get; set; }
        public string CurrentFeatureX { get; set; }
        public string CurrentFeatureY { get; set; }
        public List<double> CurrentDataX { get; set; }
        public List<double> CurrentDataY { get; set; }
        public int DataSize { get; set; }

        // Centroids, Clusters, and Max/Mins (K Means Specific) //
        public List<Tuple<double, double>> Centroids { get; set; }
        public List<List<double>> ClustersX { get; set; }
        public List<List<double>> ClustersY { get; set; }
        public double MaxX { get; set; } // Mins/Maxes for centroid randomization
        public double MaxY { get; set; }
        public double MinX { get; set; }
        public double MinY { get; set; }

        // Constructor //
        public KMeansClusterService(IDataSetService dataSetService, IMathHelper mathHelper)
        {
            this._dataSetService = dataSetService;
            this._mathHelper = mathHelper;
        }

        public void ConfigService(DataSet dataSet, int k = 2)
        {
            this.K = k;
            this._dataSetService.CurrentData = dataSet;
            this.RegressionFeatureNames = this._dataSetService.GetRegressionFeatureNames();
            this.Train(dataSet.DefaultFeatureX, dataSet.DefaultFeatureY);
        }

        public void Train(string featureX, string featureY)
        {
            this.CurrentFeatureX = featureX;
            this.CurrentFeatureY = featureY;

            this.CurrentDataX = _dataSetService.GetRegressionFeatureSeries(featureX);
            this.CurrentDataY = _dataSetService.GetRegressionFeatureSeries(featureY);
            this.MaxX = _mathHelper.Max(CurrentDataX);
            this.MaxY = _mathHelper.Max(CurrentDataY);
            this.MinX = _mathHelper.Min(CurrentDataX);
            this.MinY = _mathHelper.Min(CurrentDataY);

            this.Centroids = new List<Tuple<double, double>>();

            this.DataSize = this.CurrentDataX.Count;

        }

        public void RandomizeCentroids()
        {
            for(int i = 0; i < this.K; i++)
            {
                double centroidX = (this._mathHelper.RandomDouble() * this.MaxX) + this.MinX;
                double centroidY = (this._mathHelper.RandomDouble() * this.MaxY) + this.MinY;

                this.Centroids.Add(new Tuple<double, double>(centroidX, centroidY));
            }
        }
    }
}
