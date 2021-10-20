using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MLP.Core.Interfaces;
using MLP.Core.Common;
using MLP.Core.Models;
using System.Threading.Tasks;

namespace MLP.Core.ViewModels
{
    public class KMeansViewModel : ObservableObject
    {
        private readonly IKMeans _kmeans_service;
        private readonly IDataManagerService _data_manager_service;

        private int k;
        private string currentFeatureX;
        private string currentFeatureY;
        private int iterations;

        // Result Pane Props
        private string clusteringState = "Unclustered";
        private string clusteringStatusText = "";
        private string doneStatusText = "";
        private bool isAnimating = false;

        // Data Set Management
        private string currentDataSetName;

        private int visualizationIndex = 0;
        // centroids index - tbd

        public ObservableCollection<string> RegressionFeatureNames { get; set; }
        public ObservableCollection<NestedSeries<double>> GraphSeries { get; set; }


        public KMeansViewModel(IKMeans kMeansService, IDataManagerService dataManagerService)
        {
            this._kmeans_service = kMeansService;
            this._data_manager_service = dataManagerService;
            this.CurrentDataSetName = this._data_manager_service.CurrentDataModelMappings["kmeans"];

            this._kmeans_service.ConfigService(this._data_manager_service.FetchDataSet(this.CurrentDataSetName));

            this.RegressionFeatureNames = new ObservableCollection<string>(this._kmeans_service.RegressionFeatureNames);
            this.CurrentFeatureX = this._kmeans_service.CurrentFeatureX;
            this.CurrentFeatureY = this._kmeans_service.CurrentFeatureY;

            this.GraphSeries = new ObservableCollection<NestedSeries<double>>();
            this.K = this._kmeans_service.K;
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
            this.UpdateGraph();
        }

        // Button Clicks //

        public void IterateButton()
        {
            if (this._kmeans_service.Iterate())
            {
                this.ClusteringState = "Done";
                this.DoneStatusText = this.GetDoneStatusText();
            }
            else
            {
                this.ClusteringState = "Clustering";
                this.ClusteringStatusText = this.GetClusteringStatusText();
                this.ClearGraph();
                this.AddClustersToGraph();
            }
        }


        public async Task ConvergeButton()
        {
            while (!this._kmeans_service.Iterate())
            {
                if (this.IsAnimating)
                {
                    await Task.Delay(TimeSpan.FromSeconds(.75));
                    this.ClusteringStatusText = this.GetClusteringStatusText();
                    this.ClearGraph();
                    this.AddClustersToGraph();
                }
            }

            this.DoneStatusText = this.GetDoneStatusText();
            this.ClusteringState = "Done";
            this.ClearGraph();
            this.AddClustersToGraph();
        }

        public string GetClusteringStatusText()
        {
            return "After " + this.Iterations + " iterations, the model is still updating the clusters. The model will stop updating once data points are no longer being assigned to new clusters.";
        }

        public string GetDoneStatusText()
        {
            return "The model has converged after " + this.Iterations + " iterations. All of the data points are now assigned to their final clusters.";
        }

        public void ResetButton()
        {
            this.ClusteringState = "Unclustered";
            this.UpdateGraph();
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

        public string ClusteringState
        {
            get => clusteringState;
            set => SetProperty(ref clusteringState, value);
        }

        public int Iterations
        {
            get => this._kmeans_service.Iteration;
            set => SetProperty(ref iterations, value);
        }

        public string ClusteringStatusText
        {
            get => clusteringStatusText;
            set => SetProperty(ref clusteringStatusText, value);
        }

        public string DoneStatusText
        {
            get => doneStatusText;
            set => SetProperty(ref doneStatusText, value);
        }

        public bool IsAnimating
        {
            get => isAnimating;
            set => SetProperty(ref isAnimating, value);
        }

        public string CurrentDataSetName
        {
            get => currentDataSetName;
            set => SetProperty(ref currentDataSetName, value);
        }
    }
}