using System;
using System.Collections.Generic;
using System.Text;
using MLP.MachineLearning.Models;

namespace MLP.MachineLearning.Services
{
    public class ClassificationKNNService
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
        public List<float> CurrentDataX { get; set; }
        public List<float> CurrentDataY { get; set; }
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
            this.CurrentDataX = _dataService.GetNumericFeatureSeries(this.Data, featureX);
            this.TargetData = _dataService.GetStringFeatureSeries(this.Data, targetFeature);

            this.DataSize = this.DataSize = this.TargetData.Count;

        }
        

        public string Classify()
        {

            return "temp";
        }

    }
}
