using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;

namespace MLP.Core.Interfaces
{
    public interface IDataManagerService
    {
        Dictionary<string, DataSet> DataSets { get; set; }
        Dictionary<string, List<string>> AvailableDataModelMappings { get; set; }
        Dictionary<string, string> CurrentDataModelMappings { get; set; }
        DataSet FetchDataSet(string name);
        void InitDataModelMappings();
    }
}
