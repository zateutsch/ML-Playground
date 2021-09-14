using System;
using System.Collections.Generic;
using System.Text;

using MLP.Core.Models;

namespace MLP.Core.Interfaces
{
    public interface IDataService
    {
        List<string> GetFeatures(DataSet dataSet);
        List<double> GetNumericFeatureSeries(DataSet dataSet, string featureName);
        List<string> GetStringFeatureSeries(DataSet dataSet, string featureName);
    }
}
