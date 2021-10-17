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
        public Dictionary<Tuple<double, double>, List<double>> ClustersX { get; set; }
        public Dictionary<Tuple<double, double>, List<double>> ClustersY { get; set; }
        public double MaxX { get; set; } // Mins/Maxes for centroid randomization
        public double MaxY { get; set; }
        public double MinX { get; set; }
        public double MinY { get; set; }

        public int Iteration { get; set; }

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

            this.ResetClusters();

            this.DataSize = this.CurrentDataX.Count;

        }

        public void RandomizeCentroids()
        {

            for (int i = 0; i < this.K; i++)
            {
                double centroidX = (this._mathHelper.RandomDouble() * this.MaxX) + this.MinX;
                double centroidY = (this._mathHelper.RandomDouble() * this.MaxY) + this.MinY;

                Tuple<double, double> centroid = new Tuple<double, double>(centroidX, centroidY);
                this.Centroids.Add(centroid);
                this.ClustersX[centroid] = new List<double>();
                this.ClustersY[centroid] = new List<double>();
            }
        }

        public void AssignToCluster()
        {
            for(int i = 0; i < this.DataSize; i++)
            {
                double minDist = double.MaxValue;
                Tuple<double, double> closestCentroid = null;
                double dataX = CurrentDataX[i];
                double dataY = CurrentDataY[i];

                foreach (Tuple<double,double> centroid in this.Centroids)
                {
                    double centroidX = centroid.Item1;
                    double centroidY = centroid.Item2;

                    double dist = this._mathHelper.EuclideanDistance(new[] { dataX, dataY }, new[] { centroidX, centroidY });
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closestCentroid = centroid;
                    }
                }
                this.ClustersX[closestCentroid].Add(dataX);
                this.ClustersY[closestCentroid].Add(dataY);
            }
        }

        public bool RecalculateCentroids()
        {
            bool centroidsUpdated = false;
            List<Tuple<double, double>> newCentroids = new List<Tuple<double, double>>();

            foreach (Tuple<double, double> centroid in this.Centroids)
            {
                double centroidNewX = this._mathHelper.Mean(this.ClustersX[centroid]);
                double centroidNewY = this._mathHelper.Mean(this.ClustersY[centroid]);

                if( (centroid.Item1 != centroidNewX) || (centroid.Item2 != centroidNewY) )
                {
                    centroidsUpdated = true;
                    List<double> valueX = this.ClustersX[centroid];
                    List<double> valueY = this.ClustersY[centroid];

                    Tuple<double, double> newCentroid = new Tuple<double, double>(centroidNewX, centroidNewY);
                    newCentroids.Add(newCentroid);
                    this.ClustersX.Remove(centroid);
                    this.ClustersY.Remove(centroid);
                    this.ClustersX[newCentroid] = valueX;
                    this.ClustersY[newCentroid] = valueY;
                }
                else
                {
                    newCentroids.Add(centroid);
                }
            }

            this.Centroids = newCentroids;

            return centroidsUpdated;
        }

        // Reeset clusters to empty data structures
        public void ResetClusters()
        {
            this.Centroids = new List<Tuple<double, double>>();
            this.ClustersX = new Dictionary<Tuple<double, double>, List<double>>();
            this.ClustersY = new Dictionary<Tuple<double, double>, List<double>>();
            this.Iteration = 0;
        }

        public bool Iterate()
        {
            if(this.Iteration == 0)
            {
                this.Iteration++;
                this.RandomizeCentroids();
                return false;
            }
            else
            {
                this.Iteration++;
                this.AssignToCluster();
                return !this.RecalculateCentroids();
            }
            
        }

        public bool Iterate(int numIterations)
        {
            for(int i = 0; i < numIterations; i++)
            {
                if (this.Iterate())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
