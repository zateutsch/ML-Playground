using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using MLP.Core.Interfaces;
using MLP.Core.Common;
using MLP.Core.Messages;
using MLP.Core.Models;
using System.Threading.Tasks;

namespace MLP.Core.ViewModels
{
    public class KMeansViewModel : ObservableObject
    {
        private readonly IKMeans _kMeansService;
        private readonly IDataManagerService _dataManagerService;
        private readonly IConvexHull _convexHullService;
        private readonly string _model_key = "kmeans";

        private int k;
        private string currentFeatureX;
        private string currentFeatureY;
        private int iterations;
        private double minX;
        private double minY;
        private double maxX;
        private double maxY;

        // Result Pane Props
        private string clusteringState = "Unclustered";
        private string clusteringStatusText = "";
        private string doneStatusText = "";
        private bool isAnimating = false;
        private bool isDrawingClusters = true;


        // Data Set Management
        private string currentDataSetName;
        public ObservableCollection<string> AvailableDataSets { get; set; }

        private int visualizationIndex = 0;
        // centroids index - tbd

        public ObservableCollection<string> RegressionFeatureNames { get; set; }
        public ObservableCollection<NestedSeries> GraphSeries { get; set; }


        public KMeansViewModel(IKMeans kMeansService, IDataManagerService dataManagerService, IConvexHull convexHullService)
        {
            this._kMeansService = kMeansService;
            this._dataManagerService = dataManagerService;
            this._convexHullService = convexHullService;
            this.CurrentDataSetName = this._dataManagerService.CurrentDataModelMappings[this._model_key];
            this.AvailableDataSets = new ObservableCollection<string>(this._dataManagerService.AvailableDataModelMappings[this._model_key]);

            this._kMeansService.ConfigService(this._dataManagerService.FetchDataSet(this.CurrentDataSetName));

            this.RegressionFeatureNames = new ObservableCollection<string>(this._kMeansService.RegressionFeatureNames);
            this.CurrentFeatureX = this._kMeansService.CurrentFeatureX;
            this.CurrentFeatureY = this._kMeansService.CurrentFeatureY;

            this.GraphSeries = new ObservableCollection<NestedSeries>();
            this.K = this._kMeansService.K;
            this.UpdateMinMaxes();
            this.InitializeGraph();
        }

        public void UpdateCurrentDataModelMapping()
        {
            this._dataManagerService.CurrentDataModelMappings[this._model_key] = this.CurrentDataSetName;
        }

        public void InitializeGraph()
        {
            this.AddSeries(this._kMeansService.GetBaseSeries());
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
            this._kMeansService.Train(this.CurrentFeatureX, this.CurrentFeatureY);
            this.UpdateMinMaxes();
            this.InitializeGraph();
        }

        public void UpdateMinMaxes()
        {
            double yMargin = Math.Abs(this._kMeansService.MaxY - this._kMeansService.MinY) * .05;
            double xMargin = Math.Abs(this._kMeansService.MaxX - this._kMeansService.MinX) * .05;
            this.MaxX = Math.Ceiling(this._kMeansService.MaxX + xMargin);
            this.MaxY = Math.Ceiling(this._kMeansService.MaxY + yMargin);
            this.MinX = Math.Floor(this._kMeansService.MinX - xMargin);
            this.MinY = Math.Floor(this._kMeansService.MinY - yMargin);
        }

        public void AddClustersToGraph()
        {
            foreach (List<Point> cluster in this._kMeansService.GetClusterSeries())
            {
                this.AddSeries(cluster);
                if (this.IsDrawingClusters)
                {
                    Point[] test = this._convexHullService.GetConvexHull(cluster.ToArray());
                    this.AddVisualizationSeries(test);
                }
            }
        }

        public void AddSeries(List<Point> series)
        {
            this.GraphSeries.Insert(this.VisualizationIndex, new NestedSeries(series));
            this.VisualizationIndex += 1;
        }

        public void AddVisualizationSeries(Point[] series)
        {
            this.GraphSeries.Insert(this.VisualizationIndex, new NestedSeries(series));
        }
        public void KUpdated()
        {
            this._kMeansService.K = this.K;
            this.ResetButton();
        }

        // Button Clicks //

        public void IterateButton()
        {
            if (this._kMeansService.Iterate())
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

        public void StartButton()
        {
            if(this.ClusteringState == "Unclustered")
            {
                this.IterateButton();
            }
        }


        public async Task ConvergeButton()
        {
            while (!this._kMeansService.Iterate())
            {
                if(this.clusteringState != "Clustering")
                {
                    break;
                }
                if (this.IsAnimating)
                {
                    this.ClusteringStatusText = this.GetClusteringStatusText();
                    this.ClearGraph();
                    this.AddClustersToGraph();
                    await Task.Delay(TimeSpan.FromSeconds(.75));
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
            set
            {
                this.ClusteringState = "Unclustered";
                SetProperty(ref currentFeatureX, value);
            }
        }

        public string CurrentFeatureY
        {
            get => currentFeatureY;
            set
            {
                this.ClusteringState = "Unclustered";
                SetProperty(ref currentFeatureY, value);
            }
        }

        public int K
        {
            get => k;
            set
            {
                SetProperty(ref k, value);
                WeakReferenceMessenger.Default.Send(new ParameterChangedMessage(this.K));
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
            get => this._kMeansService.Iteration;
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

        public bool IsDrawingClusters
        {
            get => isDrawingClusters;
            set => SetProperty(ref isDrawingClusters, value);
        }

        public string CurrentDataSetName
        {
            get => currentDataSetName;
            set => SetProperty(ref currentDataSetName, value);
        }

        public double MinX
        {
            get => minX;
            set => SetProperty(ref minX, value);
        }

        public double MaxX
        {
            get => maxX;
            set => SetProperty(ref maxX, value);
        }

        public double MinY
        {
            get => minY;
            set => SetProperty(ref minY, value);
        }

        public double MaxY
        {
            get => maxY;
            set => SetProperty(ref maxY, value);
        }
    }
}