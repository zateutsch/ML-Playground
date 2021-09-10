using System;
using System.Collections.Generic;
using MLP.MachineLearning.Models;

namespace MLP.MachineLearning.Services
{

    // DataService
    // Service class for manipulating DataSet objects independent of ML model
    // @author zateutsch

    public class DataService : IDataService
    {
        public List<string> GetFeatures(DataSet dataSet)
        {
            List<string> features = new List<string>(dataSet.RegressionData.Keys);
            features.AddRange(new List<string>(dataSet.ClassificationData.Keys));

            return features;
        }

        public List<double> GetNumericFeatureSeries(DataSet dataSet, string featureName)
        {
            return new List<double>(dataSet.RegressionData[featureName]);
        }

        public List<string> GetStringFeatureSeries(DataSet dataSet, string featureName)
        {
            return new List<string>(dataSet.ClassificationData[featureName]);
        }
    }
}
