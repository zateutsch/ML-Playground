using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MLP.Core.Interfaces;
using MLP.Core.Common;

namespace MLP.Core.ViewModels
{
    // ViewModel representation of a Classification K-Nearest Nieghbors Model
    public class ClassifyKNNViewModel : ObservableObject
    {
        // Private Members
        private readonly IClassificationKNN _knn_service;
        private readonly IDataManagerService _data_manager_service;
        private double _currentTestX;
        private double _currentTestY;

        // Private Observable members to provide set property
        private int trainingDataIndex = 0;
        private int testDataIndex = 0;
        private int visualizationIndex = 0;
        private string trainingDataSeriesType = SeriesType.ScatterPoint;
        private string testDataSeriesType = SeriesType.ScatterPoint;
        private string visualizationDataSeriesType = SeriesType.ScatterSpline;

        private string currentFeatureX;
        private string currentFeatureY;
        private string currentFeatureLabel;


        // Primary Observable Collection - Core of KNN Graph Representation

        // Observable collection of NestedSeries objects (see MLP.Core.Common.NestedSeries)
        // Contains dynamical generated series of three different types, separated by index

        // Series data collection object
        public ObservableCollection<NestedSeries<double>> GraphSeries { get; set; }


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

        public string CurrentFeatureLabel
        {
            get => currentFeatureLabel;
            set => SetProperty(ref currentFeatureLabel, value);
        }
        // Indexes designating where each type of data resides in GraphSeires

        // original points in model for comparison
        public int TrainingDataIndex 
        {
            get => trainingDataIndex;
            set => SetProperty(ref trainingDataIndex, value); 
        }

        // test data to classify
        public int TestDataIndex
        {
            get => testDataIndex;
            set => SetProperty(ref testDataIndex, value);
        }

        // visualization data
        // this data has no bearing on the actual model, but is used to visualize
        // the connections between the test point and its k-nearest neighbors
        public int VisualizationIndex
        {
            get => visualizationIndex;
            set => SetProperty(ref visualizationIndex, value);
        }
      

        // Series type properties to indicate how series is visualized
        // (eg. "ScatterPointSeries", "ScatterLineSeries", etc.)
        public string TrainingDataSeriesType
        {
            get => trainingDataSeriesType;
            set => SetProperty(ref trainingDataSeriesType, value);
        }

        public string TestDataSeriesType
        {
            get => testDataSeriesType;
            set => SetProperty(ref testDataSeriesType, value);
        }

        public string VisualizationDataSeriesType
        {
            get => visualizationDataSeriesType;
            set => SetProperty(ref visualizationDataSeriesType, value);
        }

        // Feature Collection
        public ObservableCollection<string> RegressionFeatureNames { get; set; }

        // Other Observable Properties

        // Constructor
        public ClassifyKNNViewModel(IClassificationKNN knn, IDataManagerService dataManager)
        {
            this._data_manager_service = dataManager;
            this._knn_service = knn;
            this._knn_service.ConfigService(this._data_manager_service.FetchDataSet("Weather-Test-001"));

            this.RegressionFeatureNames = new ObservableCollection<string>(this._knn_service.RegressionFeatureNames);
            this.CurrentFeatureX = this._knn_service.CurrentFeatureX;
            this.CurrentFeatureY = this._knn_service.CurrentFeatureY;
            this.CurrentFeatureLabel = this._knn_service.CurrentFeatureLabel;

            this.GraphSeries = new ObservableCollection<NestedSeries<double>>();
            this.InitializeGraph();
        }


        // Methods
        public void AddTrainingDataSeries(string label, List<DataPoint<double>> series)
        {
            this.GraphSeries.Insert(this.TrainingDataIndex, new NestedSeries<double>(series));
            this.TestDataIndex += 1;
            this.VisualizationIndex += 1;
        }

        public void AddTestDataSeries(double testX, double testY)
        {
            this.GraphSeries.Insert(this.TestDataIndex, new NestedSeries<double>(new[] { testX }, new[] { testY }));
            this._currentTestX = testX;
            this._currentTestY = testY;
            this.VisualizationIndex += 1;
        }

        public void AddVisualizationSeries(double endX, double endY)
        {
            this.GraphSeries.Insert(this.VisualizationIndex, new NestedSeries<double>(new[] { this._currentTestX, endX }, new[] { this._currentTestY, endY }));
        }

        public void InitializeGraph()
        {
            Dictionary<string, List<DataPoint<double>>> seriesDict = _knn_service.GetLabeledSeries();

            foreach (KeyValuePair<string, List<DataPoint<double>>> series in seriesDict)
            {
                this.AddTrainingDataSeries(series.Key, series.Value);
            }
        }

        public void ClearGraph()
        {
            while(this.GraphSeries.Count > 0)
            {
                this.GraphSeries.RemoveAt(0);
            }

            this.TrainingDataIndex = 0;
            this.TestDataIndex = 0;
            this.VisualizationIndex = 0;
        }

        public void UpdateGraph()
        {
            this.ClearGraph();
            this._knn_service.Train(this.CurrentFeatureX, this.CurrentFeatureY, this.CurrentFeatureLabel);
            this.InitializeGraph();
        }

    }


}
