using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;

namespace MLP.Core.Services
{
    // Service class for storing and fetching required DataSets
    // Fancy dictionary of data sets
    // singleton
    // TODO: managing datasets based on what models use them
    public class DataManagerService
    {
        public Dictionary<string, DataSet> DataSets { get; set; }

        public DataSet FetchDataSet(string name)
        {
            return DataSets[name];
        }
    }
}