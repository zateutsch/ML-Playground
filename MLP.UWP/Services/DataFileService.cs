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
    public class DataFileService
    {
        private readonly StorageFolder _appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        private readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;
        private readonly string _assetsFolderName = "Assets";
        private readonly string _dataFolderName = "Data";


        public DataFileService()
        {
            if (!this.IsAppDataInitialized().Result)
            {
                Task.Run(() => this.WriteFromInstallToAppData());
            }
        }

        public async Task<Dictionary<string, DataSet>> ReadAllDataSets()
        {
            Dictionary<string, DataSet> dataSetDictionary = new Dictionary<string, DataSet>();
            StorageFolder sourceFolder = await this.GetDataFolder();
            foreach (StorageFile file in await sourceFolder.GetFilesAsync())
            {
                DataSet dataSet = await this.ReadJsonToDataSet(file);
                dataSetDictionary.Add(dataSet.Name, dataSet);
            }

            return dataSetDictionary;
        }


        public async Task<DataSet> ReadJsonToDataSet(string filename)
        {
            StorageFolder sourceFolder = await this.GetDataFolder();
            StorageFile sourceFile = await sourceFolder.GetFileAsync(filename);
            string dataSetText = await FileIO.ReadTextAsync(sourceFile);

            return this.Deserialize(dataSetText);
          
        }

        public async Task<DataSet> ReadJsonToDataSet(StorageFile sourceFile)
        {
            string dataSetText = await FileIO.ReadTextAsync(sourceFile);

            return this.Deserialize(dataSetText);
        }

        public async Task WriteDataSetToJson(DataSet dataSet)
        {
            StorageFolder destinationFolder = await this.GetDataFolder();
            StorageFile writeFile = await destinationFolder.CreateFileAsync(dataSet.Name + ".json", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(writeFile, this.Serialize(dataSet));
        }

        private async Task<StorageFolder> GetDataFolder()
        {
            StorageFolder assets = await this._localFolder.GetFolderAsync(_assetsFolderName);
            return await assets.GetFolderAsync(_dataFolderName);
        }
        private async Task<bool> IsAppDataInitialized()
        {
            return await this._localFolder.TryGetItemAsync(this._assetsFolderName) != null;
        }

        private async Task WriteFromInstallToAppData()
        {
            await this.CreateAppDataDirectories();
            await this.CopyFromInstallToAppData();
        }

        private async Task CreateAppDataDirectories()
        {
            await this._localFolder.CreateFolderAsync(this._assetsFolderName);
            StorageFolder resourcesFolder = await this._localFolder.GetFolderAsync(this._assetsFolderName);
            await resourcesFolder.CreateFolderAsync(this._dataFolderName);
        }

        private async Task CopyFromInstallToAppData()
        {
            StorageFolder sourceAssets = await this._appInstalledFolder.GetFolderAsync(this._assetsFolderName);
            StorageFolder source = await sourceAssets.GetFolderAsync(this._dataFolderName);

            StorageFolder destination = await this.GetDataFolder();

            foreach (StorageFile file in await source.GetFilesAsync())
            {
                await file.CopyAsync(destination, file.Name, NameCollisionOption.ReplaceExisting);
            }
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
