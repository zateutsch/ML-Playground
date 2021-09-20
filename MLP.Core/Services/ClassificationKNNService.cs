using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;
using MLP.Core.Common;
using MLP.Core.Interfaces;

namespace MLP.Core.Services
{
    public class ClassificationKNNService : IClassificationKNN
    {
        // Services
        private readonly IDataService _dataService;
        private readonly IMathHelper _mathHelper;

        // Model parameters
        public int Kparam { get; set; }

        // Data handling and feature management properties
        public DataSet Data { get; set; }
        public List<string> Features { get; set; }
        public string CurrentFeatureX { get; set; }
        public string CurrentFeatureY { get; set; }
        public string TargetFeature { get; set; }
        public List<double> CurrentDataX { get; set; }
        public List<double> CurrentDataY { get; set; }
        public List<string> TargetData { get; set; }
        public int DataSize { get; set; }

        // Primary constructor
        public ClassificationKNNService(
            DataSet data, 
            IDataService dataService,
            IMathHelper mathHelper,
            int k = 3)
        {

            this._dataService = dataService;
            this._mathHelper = mathHelper;

            this.Data = data;
            this.Kparam = k;
            
            this.Features = this._dataService.GetFeatures(this.Data);

        }

        public void Train(string featureX, string featureY, string targetFeature)
        {
            this.CurrentFeatureX = featureX;
            this.CurrentFeatureY = featureY;
            this.TargetFeature = targetFeature;

            this.CurrentDataX = _dataService.GetNumericFeatureSeries(this.Data, featureX);
            this.CurrentDataY = _dataService.GetNumericFeatureSeries(this.Data, featureY);
            this.TargetData = _dataService.GetStringFeatureSeries(this.Data, targetFeature);

            this.DataSize = this.DataSize = this.TargetData.Count;

        }


        public string Classify(double x, double y)
        {

            ConstMinSortedDLL min_list = new ConstMinSortedDLL(this.Kparam);
            double[] feature_arr = new[] { x, y };

            for(int i = 0; i < this.TargetData.Count; i++)
            {
                double distance = _mathHelper.EuclideanDistance(new[] { CurrentDataX[i], CurrentDataY[i] }, feature_arr);
                min_list.AddAndTrim(new Node(distance, i));
            }

            List<string> close_labels = this.GetLabelsFromDLL(min_list);

            return this.FindMostCommonLabel(close_labels);
            
        }

        // Returns a dictionary where
        // each key is a unique label that appears in current classification problem and
        // each entry is a list of all data points that have that label

        public Dictionary<string, List<DataPoint<double>>> GetLabeledSeries()
        {
            Dictionary<string, List<DataPoint<double>>> labeledSeries = new Dictionary<string, List<DataPoint<double>>>();

            for(int i = 0; i < this.DataSize; i++)
            {
                string label = this.TargetData[i];

                if (!labeledSeries.ContainsKey(label))
                {
                    labeledSeries[label] = new List<DataPoint<double>>();  
                }

                labeledSeries[label].Add(new DataPoint<double>(this.CurrentDataX[i], this.CurrentDataY[i]));

            }

            return labeledSeries;
        }

        private List<string> GetLabelsFromDLL(ConstMinSortedDLL min_list)
        {
            List<int> keys = new List<int>(min_list.ReturnAsDictionary().Keys);
            List<string> labels = new List<string>(this.Kparam);

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

            return max_label;

        }
    }
}
