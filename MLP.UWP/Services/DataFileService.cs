using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLP.Core.Models;
using Windows.Storage;
using Newtonsoft.Json;

namespace MLP.UWP.Services
{
    public class DataFileService : IDataFileService
    {
        private readonly StorageFolder _appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        private readonly string _dataDirectory = "Resources/Data/";

        public async Task<DataSet> ReadDataSetFromJSON(string filename)
        {
            StorageFile targetFile = await _appInstalledFolder.GetFileAsync(this._dataDirectory + filename);
            string textDataSet = await FileIO.ReadTextAsync(targetFile);

            return this.Deserialize(textDataSet);
        }

        public async Task WriteDataSetToJSON(DataSet dataSet)
        {
            StorageFile targetFile = await _appInstalledFolder.GetFileAsync(this.GetDataSetFilename(dataSet));
            string textDataSet = this.Serialize(dataSet);

            await FileIO.WriteTextAsync(targetFile, textDataSet);
        }

        public string GetDataSetFilename(DataSet dataSet)
        {
            return this._dataDirectory + dataSet.Name + ".json";
        }


        // Wrapper functions for JsonConvert
        public DataSet Deserialize(string textObj)
        {
            return JsonConvert.DeserializeObject<DataSet>(textObj);
        }

        public string Serialize(DataSet dataSet)
        {
            return JsonConvert.SerializeObject(dataSet);
        }

        
    }
}
