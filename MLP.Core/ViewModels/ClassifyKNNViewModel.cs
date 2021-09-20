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

        // Private Observable members to provide set property
        private int trainingDataIndex;
        private int testDataIndex;
        private int visualizationIndex;
        private string trainingDataSeriesType;
        private string testDataSeriesType;
        private string visualizationDataSeriesType;

        // Primary Observable Collection - Core of KNN Graph Representation

        // Observable collection of NestedSeries objects (see MLP.Core.Common.NestedSeries)
        // Contains dynamical generated series of three different types, separated by index

        // Series data collection object
        public ObservableCollection<NestedSeries<double>> GraphSeries { get; set; }

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
        

        // Other Observable Properties

        // Constructor
        public ClassifyKNNViewModel(IClassificationKNN knn)
        {
            this._knn_service = knn;
            this.TrainingDataSeriesType = SeriesType.ScatterPoint;
            this.TestDataSeriesType = SeriesType.ScatterPoint;
            this.VisualizationDataSeriesType = SeriesType.ScatterLine;
            this.GraphSeries = new ObservableCollection<NestedSeries<double>>();

            this.TrainingDataIndex = 0;
            this.TestDataIndex = 1;
            this.VisualizationIndex = 2;

            this.InitializeGraphData();
        }


        // Methods
        public void AddTrainingDataSeries(string label, List<DataPoint<double>> series)
        {
            this.GraphSeries.Insert(this.TestDataIndex - 1, new NestedSeries<double>(series));
            this.TestDataIndex += 1;
            this.VisualizationIndex += 1;
        }

        public void InitializeGraphData()
        {
            Dictionary<string, List<DataPoint<double>>> seriesDict = _knn_service.GetLabeledSeries();

            foreach (KeyValuePair<string, List<DataPoint<double>>> series in seriesDict)
            {
                this.AddTrainingDataSeries(series.Key, series.Value);
            }
        }
    }


}
