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

    // DataFileService to manage Reading and Writing data sets to Local Storage
    public class DataFileService
    {
        private readonly StorageFolder _appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        private readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;
        private readonly string _assetsFolderName = "Assets";
        private readonly string _dataFolderName = "Data";


        // Constructor
        // Checks if AppData has been initialized
        // if not, write Install data to App Data
        public DataFileService()
        {
            if (!this.IsAppDataInitialized().Result)
            {
                Task.Run(() => this.WriteFromInstallToAppData());
            }
        }

        // Reads all data sets from data directory and returns them as a dictionary
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


        // Read a .json file into a data set, string override
        public async Task<DataSet> ReadJsonToDataSet(string filename)
        {
            StorageFolder sourceFolder = await this.GetDataFolder();
            StorageFile sourceFile = await sourceFolder.GetFileAsync(filename);
            string dataSetText = await FileIO.ReadTextAsync(sourceFile);

            return this.Deserialize(dataSetText);
          
        }

        // Read a .json file into a data set, StorageFile override
        public async Task<DataSet> ReadJsonToDataSet(StorageFile sourceFile)
        {
            string dataSetText = await FileIO.ReadTextAsync(sourceFile);

            return this.Deserialize(dataSetText);
        }

        // Write a data set object to a .json file
        public async Task WriteDataSetToJson(DataSet dataSet)
        {
            StorageFolder destinationFolder = await this.GetDataFolder();
            StorageFile writeFile = await destinationFolder.CreateFileAsync(dataSet.Name + ".json", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(writeFile, this.Serialize(dataSet));
        }

        // Fetches the StorageFolder where data is being stored
        private async Task<StorageFolder> GetDataFolder()
        {
            StorageFolder assets = await this._localFolder.GetFolderAsync(_assetsFolderName);
            return await assets.GetFolderAsync(_dataFolderName);
        }

        // Checks if AppData directory has been initialized for usage
        private async Task<bool> IsAppDataInitialized()
        {
            return await this._localFolder.TryGetItemAsync(this._assetsFolderName) != null;
        }

        // Creates and coopies relevant data files from install location to AppData
        private async Task WriteFromInstallToAppData()
        {
            await this.CreateAppDataFolders();
            await this.CopyFromInstallToAppData();
        }

        // Creates relevant Data directories for App Data
        private async Task CreateAppDataFolders()
        {
            await this._localFolder.CreateFolderAsync(this._assetsFolderName);
            StorageFolder resourcesFolder = await this._localFolder.GetFolderAsync(this._assetsFolderName);
            await resourcesFolder.CreateFolderAsync(this._dataFolderName);
        }

        // Copies relevant data from install to AppData
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
