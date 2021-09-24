using System;
using System.Collections.Generic;
using System.Text;

using MLP.Core.Models;

namespace MLP.Core.Interfaces
{
    public interface IDataSetService
    {
        DataSet CurrentData { get; set; }
        List<string> GetFeatures();
        List<double> GetRegressionFeatureSeries(string featureName);
        List<string> GetClassificationFeatureSeries(string featureName);
        List<string> GetRegressionFeatureNames();
        List<string> GetClassificationFeatureNames();

    }
}
