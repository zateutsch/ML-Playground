using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;
using MLP.Core.Interfaces;

namespace MLP.Core.Services
{
    // Service class for storing and fetching required DataSets
    // Fancy dictionary of data sets
    // singleton
    // TODO: managing datasets based on what models use them
    public class DataManagerService : IDataManagerService
    {
        public Dictionary<string, DataSet> DataSets { get; set; }
        public Dictionary<string, List<string>> AvailableDataModelMappings { get; set; }
        public Dictionary<string, string> CurrentDataModelMappings { get; set; }


        public DataSet FetchDataSet(string name)
        {
            return DataSets[name];
        }

        public void InitDataModelMappings()
        {
            this.AvailableDataModelMappings = new Dictionary<string, List<string>>();
            this.CurrentDataModelMappings = new Dictionary<string, string>();
            foreach (DataSet dataSet in this.DataSets.Values)
            {
                foreach (string modelKey in dataSet.EnabledModels)
                {
                    if (!this.AvailableDataModelMappings.ContainsKey(modelKey))
                    {
                        this.AvailableDataModelMappings[modelKey] = new List<string>();
                        this.CurrentDataModelMappings[modelKey] = dataSet.DisplayName;
                    }

                    this.AvailableDataModelMappings[modelKey].Add(dataSet.DisplayName);
                }
            }
        }

    }
}