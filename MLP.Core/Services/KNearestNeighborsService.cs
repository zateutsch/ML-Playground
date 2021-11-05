using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;
using MLP.Core.Common;
using MLP.Core.Interfaces;

namespace MLP.Core.Services
{
    public class KNearestNeighborsService : IClassificationKNN
    {
        // Services
        private readonly IDataSetService _dataSetService;
        private readonly IMathHelper _mathHelper;

        private bool standardized;
        private double _stdX;
        private double _stdY;

        // Model parameters
        public int K { get; set; }

        // Data handling and feature management properties
        public DataSet Data { get; set; }
        public List<string> RegressionFeatureNames { get; set; }
        public string CurrentFeatureX { get; set; }
        public string CurrentFeatureY { get; set; }
        public string CurrentFeatureLabel { get; set; }
        public List<double> CurrentDataX { get; set; }
        public List<double> CurrentDataY { get; set; }
        public List<string> TargetData { get; set; }
        public Dictionary<string, int> Counts { get; set; }
        public int DataSize { get; set; }
        public double MinX
        {
            get => this._mathHelper.Min(this.CurrentDataX);
        }
        public double MaxX
        {
            get => this._mathHelper.Max(this.CurrentDataX);
        }
        public double MinY
        {
            get => this._mathHelper.Min(this.CurrentDataY);
        }
        public double MaxY
        {
            get => this._mathHelper.Max(this.CurrentDataY);
        }

        // Primary constructor
        public KNearestNeighborsService(IDataSetService dataSetService, IMathHelper mathHelper)
        {
            this._dataSetService = dataSetService;
            this._mathHelper = mathHelper;
        }

        public void ConfigService(DataSet dataSet, int k = 3)
        {
            this.K = k;
            this._dataSetService.CurrentData = dataSet;
            this.RegressionFeatureNames = this._dataSetService.GetRegressionFeatureNames();
            this.Train(dataSet.DefaultFeatureX, dataSet.DefaultFeatureY, dataSet.DefaultFeatureLabel, false);
        }

        public void Train(string featureX, string featureY, string targetFeature, bool isStandardized)
        {
            this.CurrentFeatureX = featureX;
            this.CurrentFeatureY = featureY;
            this.CurrentFeatureLabel = targetFeature;
            this.CurrentDataX = _dataSetService.GetRegressionFeatureSeries(featureX);
            this.CurrentDataY = _dataSetService.GetRegressionFeatureSeries(featureY);
            this.standardized = isStandardized;
            this.StandardizeCurrentData();
            this.TargetData = _dataSetService.GetClassificationFeatureSeries(targetFeature);

            this.DataSize = this.TargetData.Count;

        }

        public void StandardizeCurrentData()
        {
            this._stdX = this._mathHelper.StandardDeviation(this.CurrentDataX.ToArray());
            this._stdY = this._mathHelper.StandardDeviation(this.CurrentDataY.ToArray());
        }


        public string Classify(double x, double y)
        {

            ConstMinSortedDLL min_list = new ConstMinSortedDLL(this.K);
            double[] feature_arr;
            if (standardized)
            {
                feature_arr = new[] { x/this._stdX, y/this._stdY };
            }
            else
            {
                feature_arr = new[] { x, y };
            }

            for(int i = 0; i < this.TargetData.Count; i++)
            {
                double distance;
                if (standardized)
                {
                    distance = _mathHelper.EuclideanDistance(new[] { CurrentDataX[i]/this._stdX, CurrentDataY[i]/this._stdY }, feature_arr);
                }
                else
                {
                    distance = _mathHelper.EuclideanDistance(new[] { CurrentDataX[i], CurrentDataY[i] }, feature_arr);
                }
                    
                min_list.AddAndTrim(new Node(distance, i));
            }

            List<string> close_labels = this.GetLabelsFromDLL(min_list);

            return this.FindMostCommonLabel(close_labels);
            
        }

        public Tuple<string, Dictionary<int, double>> RobustClassify(double x, double y)
        {
            ConstMinSortedDLL min_list = new ConstMinSortedDLL(this.K);
            double[] feature_arr;
            if (standardized)
            {
                feature_arr = new[] { x / this._stdX, y / this._stdY };
            }
            else
            {
                feature_arr = new[] { x, y };
            }

            for (int i = 0; i < this.TargetData.Count; i++)
            {
                double distance;
                if (standardized)
                {
                    distance = _mathHelper.EuclideanDistance(new[] { CurrentDataX[i] / this._stdX, CurrentDataY[i] / this._stdY }, feature_arr);
                }
                else
                {
                    distance = _mathHelper.EuclideanDistance(new[] { CurrentDataX[i], CurrentDataY[i] }, feature_arr);
                }
                min_list.AddAndTrim(new Node(distance, i));
            }

            List<string> close_labels = this.GetLabelsFromDLL(min_list);

            return new Tuple<string, Dictionary<int, double>>(this.FindMostCommonLabel(close_labels), min_list.ReturnAsDictionary());
        }

        // Returns a dictionary where
        // each key is a unique label that appears in current classification problem and
        // each entry is a list of all data points that have that label

        public Dictionary<string, List<Point>> GetLabeledSeries()
        {
            Dictionary<string, List<Point>> labeledSeries = new Dictionary<string, List<Point>>();

            for(int i = 0; i < this.DataSize; i++)
            {
                string label = this.TargetData[i];

                if (!labeledSeries.ContainsKey(label))
                {
                    labeledSeries[label] = new List<Point>();  
                }

                labeledSeries[label].Add(new Point(this.CurrentDataX[i], this.CurrentDataY[i]));

            }

            return labeledSeries;
        }

        private List<string> GetLabelsFromDLL(ConstMinSortedDLL min_list)
        {
            List<int> keys = new List<int>(min_list.ReturnAsDictionary().Keys);
            List<string> labels = new List<string>(this.K);

            foreach(int idx in keys)
            {
                labels.Add(this.TargetData[idx]);
            }

            return labels;
        }

        private string FindMostCommonLabel(List<string> close_labels)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            int max_count = 0;
            string max_label = "";

            foreach(string label in close_labels)
            {
                if (!counts.ContainsKey(label))
                {
                    counts[label] = 1;
                }
                else
                {
                    counts[label] += 1;
                }

                if(counts[label] > max_count)
                {
                    max_count = counts[label];
                    max_label = label;
                }
            }

            this.Counts = counts;
            return max_label;

        }
    }
}
