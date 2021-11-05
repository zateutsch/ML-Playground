using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;
using MLP.Core.Common;
using MLP.Core.Interfaces;
using System.Diagnostics;

namespace MLP.Core.Services
{
    public class KMeansClusterService : IKMeans
    {
        // Services //
        private readonly IDataSetService _dataSetService;
        private readonly IMathHelper _mathHelper;

        private double _stdX;
        private double _stdY;
        private bool standardized;
        // Model Parameters //
        public int K { get; set; }

        // Data Management //
        public DataSet Data { get; set; }
        public List<string> RegressionFeatureNames { get; set; }
        public string CurrentFeatureX { get; set; }
        public string CurrentFeatureY { get; set; }
        public List<double> CurrentDataX { get; set; }
        public List<double> CurrentDataY { get; set; }
        public int DataSize { get; set; } = 0;

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
            this.Train(dataSet.DefaultFeatureX, dataSet.DefaultFeatureY, true);
        }

        public void Train(string featureX, string featureY, bool isStandardized)
        {
            this.CurrentFeatureX = featureX;
            this.CurrentFeatureY = featureY;

            this.CurrentDataX = _dataSetService.GetRegressionFeatureSeries(featureX);
            this.CurrentDataY = _dataSetService.GetRegressionFeatureSeries(featureY);
            this.standardized = isStandardized;
            this.StandardizeCurrentData();
            this.MaxX = _mathHelper.Max(CurrentDataX);
            this.MaxY = _mathHelper.Max(CurrentDataY);
            this.MinX = _mathHelper.Min(CurrentDataX);
            this.MinY = _mathHelper.Min(CurrentDataY);

            this.ResetClusters();

            this.DataSize = this.CurrentDataX.Count;

        }

        public void StandardizeCurrentData()
        {
            this._stdX = this._mathHelper.StandardDeviation(this.CurrentDataX.ToArray());
            this._stdY = this._mathHelper.StandardDeviation(this.CurrentDataY.ToArray());
        }

        public void RandomizeCentroids()
        {
            for (int i = 0; i < this.K; i++)
            {
                double centroidX = (this._mathHelper.RandomDouble() * (this.MaxX - this.MinX)) + this.MinX;
                double centroidY = (this._mathHelper.RandomDouble() * (this.MaxY - this.MinY)) + this.MinY;

                Tuple<double, double> centroid = new Tuple<double, double>(centroidX, centroidY);
                this.Centroids.Add(centroid);
                this.ClustersX[centroid] = new List<double>();
                this.ClustersY[centroid] = new List<double>();
            }
        }

        public void AssignToCluster()
        {
            this.EmptyClustersForReassign();

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
                    double dist;

                    if (standardized)
                    {
                        dist = this._mathHelper.EuclideanDistance(new[] { dataX/this._stdX, dataY/this._stdY }, new[] { centroidX/this._stdX, centroidY/this._stdY });
                    }
                    else
                    {
                        dist = this._mathHelper.EuclideanDistance(new[] { dataX, dataY }, new[] { centroidX, centroidY });
                    }

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
                if(Double.IsNaN(centroidNewX) || Double.IsNaN(centroidNewY))
                {
                    newCentroids.Add(centroid);
                }
                else if((centroid.Item1 != centroidNewX)  || (centroid.Item2 != centroidNewY) )
                {
                    centroidsUpdated = true;
                    List<double> valueX = this.ClustersX[centroid];
                    List<double> valueY = this.ClustersY[centroid];

                    Tuple<double, double> newCentroid = new Tuple<double, double>(centroidNewX, centroidNewY);
                    newCentroids.Add(newCentroid);
                }
                else
                {
                    newCentroids.Add(centroid);
                }
            }

            this.Centroids = newCentroids;

            return centroidsUpdated;
        }

        // Reset clusters to empty data structures
        public void ResetClusters()
        {
            this.Centroids = new List<Tuple<double, double>>();
            this.ClustersX = new Dictionary<Tuple<double, double>, List<double>>();
            this.ClustersY = new Dictionary<Tuple<double, double>, List<double>>();
            this.Iteration = 0;
        }

        public void EmptyClustersForReassign()
        {
            foreach (Tuple<double, double> centroid in this.Centroids)
            {
                this.ClustersX[centroid] = new List<double>();
                this.ClustersY[centroid] = new List<double>();
            }
        }

        public bool Iterate()
        {
            if(this.Iteration == 0)
            {
                this.RandomizeCentroids();
                this.AssignToCluster();
                this.Iteration++;
                return false;
            }
            else
            {
                this.Iteration++;
                bool result = this.RecalculateCentroids();
                this.AssignToCluster();
                return !result;
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

        public List<Point> GetBaseSeries()
        {
            List<Point> baseSeries = new List<Point>();
            for(int i = 0; i < this.DataSize; i++)
            {
                baseSeries.Add(new Point(this.CurrentDataX[i], this.CurrentDataY[i]));
            }

            return baseSeries;
        }

        public List<List<Point>> GetClusterSeries()
        {
            List<List<Point>> clusters = new List<List<Point>>();
            foreach (Tuple<double, double> centroid in this.Centroids)
            {
                List<Point> cluster = new List<Point>();
                for (int i = 0; i < this.ClustersX[centroid].Count; i++)
                {
                    cluster.Add(new Point(this.ClustersX[centroid][i], this.ClustersY[centroid][i]));
                }

                clusters.Add(cluster);
            }

            return clusters;
        }
    }
}
