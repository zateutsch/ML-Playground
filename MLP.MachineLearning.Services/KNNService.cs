using System;
using System.Collections.Generic;
using System.Text;
using MLP.MachineLearning.Models;

namespace MLP.MachineLearning.Services
{
    public class KNNService
    {
        public DataSet Data { get; set; }
        public int Kparam { get; set; }
        public bool IsClassification { get; set; }

        private readonly IDataService _dataService;


        // Primary constructor
        public KNNService(
            DataSet data, 
            IDataService dataService, 
            int k = 3)
        {
            this.Data = data;
            this.Kparam = k;
            this._dataService = dataService;

        }

        

    }
}
