using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLP.Core.Models;

namespace MLP.UWP.Services
{
    public interface IDataFileService
    {
        Task<DataSet> ReadDataSetFromJSON(string filename);
        Task WriteDataSetToJSON(DataSet dataSet);
        DataSet Deserialize(string textObj);
    }
}
