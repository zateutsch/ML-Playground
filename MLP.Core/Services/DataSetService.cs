using System;
using System.Collections.Generic;
using MLP.Core.Interfaces;
using MLP.Core.Models;

namespace MLP.Core.Services
{

    // DataService
    // Service class for manipulating DataSet objects independent of ML model
    // @author zateutsch

    public class DataSetService : IDataSetService
    {
        public DataSet CurrentData { get; set; }

        public List<string> GetFeatures()
        {
            List<string> features = new List<string>(this.CurrentData.RegressionData.Keys);
            features.AddRange(new List<string>(this.CurrentData.ClassificationData.Keys));

            return features;
        }

        public List<double> GetRegressionFeatureSeries(string featureName)
        {
            return new List<double>(this.CurrentData.RegressionData[featureName]);
        }

        public List<string> GetClassificationFeatureSeries(string featureName)
        {
            return new List<string>(this.CurrentData.ClassificationData[featureName]);
        }
       
    }
}
