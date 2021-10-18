using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MLP.Core.Interfaces;
using MLP.Core.Common;
using MLP.Core.Models;

namespace MLP.Core.ViewModels
{
    public class KMeansViewModel : ObservableObject
    {
        private readonly IKMeans _kmeans_service;
        private readonly IDataManagerService _data_manager_service;

        private int k;
        private string currentFeatureX;
        private string currentFeatureY;

        private int visualizationIndex = 0;
        // centroids index - tbd

        public ObservableCollection<string> RegressionFeatureNames { get; set; }
        public ObservableCollection<NestedSeries<double>> GraphSeries { get; set; }

        
        public KMeansViewModel(IKMeans kMeansService, IDataManagerService dataManagerService)
        {
            this._kmeans_service = kMeansService;
            this._data_manager_service = dataManagerService;

            this._kmeans_service.ConfigService(this._data_manager_service.FetchDataSet("Kmeans-Large-Test"));

            this.RegressionFeatureNames = new ObservableCollection<string>(this._kmeans_service.RegressionFeatureNames);
            this.CurrentFeatureX = this._kmeans_service.CurrentFeatureX;
            this.CurrentFeatureY = this._kmeans_service.CurrentFeatureY;
            this.K = this._kmeans_service.K;

            this.GraphSeries = new ObservableCollection<NestedSeries<double>>();
            this.InitializeGraph();
        }

        public void InitializeGraph()
        {
            this.AddSeries(this._kmeans_service.GetBaseSeries());
        }

        public void ClearGraph()
        {
            while (this.GraphSeries.Count > 0)
            {
                this.GraphSeries.RemoveAt(0);
            }

            this.VisualizationIndex = 0;
        }

        public void UpdateGraph()
        {
            this.ClearGraph();
            this._kmeans_service.Train(this.CurrentFeatureX, this.CurrentFeatureY);
            this.InitializeGraph();
        }

        public void AddClustersToGraph()
        {
            this._kmeans_service.Iterate();
            this.ClearGraph();
            foreach (List<DataPoint<double>> cluster in this._kmeans_service.GetClusterSeries())
            {
                this.AddSeries(cluster);
            }
        }

        public void AddSeries(List<DataPoint<double>> series)
        {
            this.GraphSeries.Insert(this.VisualizationIndex, new NestedSeries<double>(series));
            this.VisualizationIndex += 1;
        }
        public void KUpdated()
        {
            this._kmeans_service.K = this.K;
        }


        // Observable Props //
        public string CurrentFeatureX
        {
            get => currentFeatureX;
            set => SetProperty(ref currentFeatureX, value);
        }

        public string CurrentFeatureY
        {
            get => currentFeatureY;
            set => SetProperty(ref currentFeatureY, value);
        }

        public int K
        {
            get => k;
            set
            {
                SetProperty(ref k, value);
                this.KUpdated();
            }
        }

        public int VisualizationIndex
        {
            get => visualizationIndex;
            set => SetProperty(ref visualizationIndex, value);
        }
    }

}
