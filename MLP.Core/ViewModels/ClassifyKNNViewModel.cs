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
    // ViewModel representation of a Classification K-Nearest Nieghbors Model
    public class ClassifyKNNViewModel : ObservableObject
    {
        // Private Members
        private readonly IClassificationKNN _knn_service;
        private readonly IDataManagerService _data_manager_service;
        private readonly string _model_key = "knn";
        private double _currentTestX;
        private double _currentTestY;
        private bool _isTesting = false;
        private string _testResult;

        // Private Observable Members

        private int trainingDataIndex = 0;
        private int testDataIndex = 0;
        private int visualizationIndex = 0;
        private string trainingDataSeriesType = SeriesType.ScatterPoint;
        private string testDataSeriesType = SeriesType.ScatterPoint;
        private string visualizationDataSeriesType = SeriesType.ScatterSpline;

        private string currentFeatureX;
        private string currentFeatureY;
        private string currentFeatureLabel;
        private double minX;
        private double minY;
        private double maxX;
        private double maxY;
        private bool standardized;

        private double userTestX;
        private double userTestY;

        private string predictedLabelText = "";
        private string resultExplanationText = "";

        private string firstSeriesColor = "#00CC6A";
        private string secondSeriesColor = "#FF7960";
        private string testSeriesColor = "White";
        private string visualizationColor = "White";
        private string winningColor = "White";

        private string firstSeriesLabel = "";
        private string secondSeriesLabel= "";

        private string isTesting = "false";
        private int k;

        // Data Set Management
        private string currentDataSetName;
        public ObservableCollection<string> AvailableDataSets { get; set; }

        // Primary Observable Collection - Core of KNN Graph Representation

        // Observable collection of NestedSeries objects (see MLP.Core.Common.NestedSeries)
        // Contains dynamical generated series of three different types, separated by index

        // Series data collection object
        public ObservableCollection<NestedSeries> GraphSeries { get; set; }
        // Test History collection
        public ObservableCollection<KNNTest> TestHistory { get; set; }
        // Feature Collection
        public ObservableCollection<string> RegressionFeatureNames { get; set; }

        // Constructor
        public ClassifyKNNViewModel(IClassificationKNN knn, IDataManagerService dataManager)
        {
            this._data_manager_service = dataManager;
            this._knn_service = knn;
            this.CurrentDataSetName = this._data_manager_service.CurrentDataModelMappings[this._model_key];
            this.AvailableDataSets = new ObservableCollection<string>(this._data_manager_service.AvailableDataModelMappings[this._model_key]);
            this._knn_service.ConfigService(this._data_manager_service.FetchDataSet(this.CurrentDataSetName));

            this.RegressionFeatureNames = new ObservableCollection<string>(this._knn_service.RegressionFeatureNames);
            this.CurrentFeatureX = this._knn_service.CurrentFeatureX;
            this.CurrentFeatureY = this._knn_service.CurrentFeatureY;
            this.CurrentFeatureLabel = this._knn_service.CurrentFeatureLabel;
            this.K = this._knn_service.K;

            this.GraphSeries = new ObservableCollection<NestedSeries>();
            this.TestHistory = new ObservableCollection<KNNTest>();
            this.PredictedLabelText = this.GetPredictedLabelText();
            this.UpdateMinMaxes();
            this.InitializeGraph();
        }

        public void UpdateCurrentDataModelMapping()
        {
            this._data_manager_service.CurrentDataModelMappings[this._model_key] = this.CurrentDataSetName;
        }
        // Methods
        public void AddTrainingDataSeries(string label, List<Point> series)
        {
            this.GraphSeries.Insert(this.TrainingDataIndex, new NestedSeries(series));
            this.TestDataIndex += 1;
            this.VisualizationIndex += 1;
        }

        public void AddTestDataSeries(double testX, double testY)
        {
            this.GraphSeries.Insert(this.TestDataIndex, new NestedSeries(new[] { testX }, new[] { testY }));
            this._currentTestX = testX;
            this._currentTestY = testY;
            this.VisualizationIndex += 1;
        }

        public void AddVisualizationSeries(double endX, double endY)
        {
            this.GraphSeries.Insert(this.VisualizationIndex, new NestedSeries(new[] { this._currentTestX, endX, endX }, new[] { this._currentTestY, endY, endY }));
        }

        public void InitializeGraph()
        {
            Dictionary<string, List<Point>> seriesDict = _knn_service.GetLabeledSeries();

            foreach (KeyValuePair<string, List<Point>> series in seriesDict)
            {
                this.AddTrainingDataSeries(series.Key, series.Value);
                if(this.secondSeriesLabel == "")
                {
                    this.secondSeriesLabel = series.Key;
                }
                else
                {
                    this.firstSeriesLabel = series.Key;
                }
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
            this.firstSeriesLabel = "";
            this.secondSeriesLabel = "";
        }

        public void UpdateGraph()
        {
            this.ClearGraph();
            this._isTesting = false;
            this.IsTesting = "False";
            this._knn_service.Train(this.CurrentFeatureX, this.CurrentFeatureY, this.CurrentFeatureLabel, this.Standardized);
            this.UpdateMinMaxes();
            this.InitializeGraph();
        }

        public void UpdateMinMaxes()
        {
            double yMargin = Math.Abs(this._knn_service.MaxY - this._knn_service.MinY) * .05;
            double xMargin = Math.Abs(this._knn_service.MaxX - this._knn_service.MinX) * .05;
            this.MaxX = Math.Ceiling(this._knn_service.MaxX + xMargin);
            this.MaxY = Math.Ceiling(this._knn_service.MaxY + yMargin);
            this.MinX = Math.Floor(this._knn_service.MinX - xMargin);
            this.MinY = Math.Floor(this._knn_service.MinY - yMargin);
        }

        public void TestPoint()
        {
            if (this._isTesting)
            {
                this.RemoveCurrentTest();
            }

            this._isTesting = true;
            this.IsTesting = "True";
            this.AddTestDataSeries(this.UserTestX, this.UserTestY);
            Tuple<string, Dictionary<int, double>> result = this._knn_service.RobustClassify(this.UserTestX, UserTestY);
            foreach (int idx in result.Item2.Keys)
            {
                this.AddVisualizationSeries(this._knn_service.CurrentDataX[idx], this._knn_service.CurrentDataY[idx]);
            }

            this._testResult = result.Item1;
            this.TestHistory.Add(new KNNTest(this.CurrentFeatureX, this.CurrentFeatureY, this.CurrentFeatureLabel, this.K, this.UserTestX, this.UserTestY));
            this.PredictedLabelText = _testResult;
            this.ResultExplanationText = this.GetResultExplanationText();
        }

        public void RemoveCurrentTest()
        {
            while(this.GraphSeries.Count > this.TestDataIndex)
            {
                this.GraphSeries.RemoveAt(this.GraphSeries.Count - 1);
            }

            this._isTesting = false;
            this.PredictedLabelText = this.GetPredictedLabelText();
        }

        public void KUpdated()
        {
            this._knn_service.K = this.K;
            if (this._isTesting)
            {
                this.TestPoint();
            }

        }

        public string GetPredictedLabelText()
        {
            string result = "The predicted label for " + this.CurrentFeatureLabel + ": ";
            if (_isTesting)
            {
                result += this._testResult;
            }

            return result;
        }

        public string GetResultExplanationText()
        {
            string winningLabel = this._testResult;
            string losingLabel = winningLabel != this.FirstSeriesLabel ? this.FirstSeriesLabel : this.SecondSeriesLabel;
            this.WinningColor = winningLabel == this.FirstSeriesLabel ? this.FirstSeriesColor : this.SecondSeriesColor;
            int winningCount = this._knn_service.Counts[winningLabel];
            int losingCount = this._knn_service.Counts.ContainsKey(losingLabel) ? this._knn_service.Counts[losingLabel] : 0;

            if (winningCount == losingCount)
            {
                return string.Format("The model evaluated the {0} nearest points, and found that both \"{1}\" and \"{2}\" had counts of {3} for feature {4}. Because the closest point to our test datahad label \"{1}\", this label is selected as the tiebreaker.", this.K, winningLabel, losingLabel, winningCount, this.CurrentFeatureLabel);
            }

            return string.Format("The model evaluated the {0} closest points, and found that {1} points were labeled \"{2}\" for feature {3}, while only {4} points were labeled \"{5}\" for feature {3}.", this.K, winningCount, winningLabel, this.CurrentFeatureLabel, losingCount, losingLabel);
        }

        /// Observable Properties ///

        // Pallette Properties //

        public string FirstSeriesColor
        {
            get => firstSeriesColor;
            set => SetProperty(ref firstSeriesColor, value);
        }

        public string SecondSeriesColor
        {
            get => secondSeriesColor;
            set => SetProperty(ref secondSeriesColor, value);
        }

        public string TestSeriesColor
        {
            get => testSeriesColor;
            set => SetProperty(ref testSeriesColor, value);
        }

        public string VisualizationColor
        {
            get => visualizationColor;
            set => SetProperty(ref visualizationColor, value);
        }

        // Series Order Properties for Color Management //

        public string FirstSeriesLabel
        {
            get => firstSeriesLabel;
            set => SetProperty(ref firstSeriesLabel, value);
        }

        public string SecondSeriesLabel
        {
            get => secondSeriesLabel;
            set => SetProperty(ref secondSeriesLabel, value);
        }

        public string TestSeriesLabel
        {
            get => "test";
        }

        // Display Text Properties //

        public string PredictedLabelText
        {
            get => predictedLabelText;
            set => SetProperty(ref predictedLabelText, value);
        }

        public string KeyHeaderText
        {
            get => "Labels for feature \"" + this.CurrentFeatureLabel + "\":";
        }

        public string ResultExplanationText
        {
            get => resultExplanationText;
            set => SetProperty(ref resultExplanationText, value);
        }

        // Model Interaction Properties //

        public int K
        {
            get => k;
            set
            {
                SetProperty(ref k, value);
                this.KUpdated();
            }
        }
        public double UserTestX
        {
            get => userTestX;
            set => SetProperty(ref userTestX, value);
        }

        public double UserTestY
        {
            get => userTestY;
            set => SetProperty(ref userTestY, value);
        }


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

        public string IsTesting
        {
            get => isTesting;
            set => SetProperty(ref isTesting, value);
        }

        // Indexing Properties //

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
            get => this.testDataIndex + 1;
            set => SetProperty(ref visualizationIndex, value);
        }


        // Series Type Properties //

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

        public bool Standardized
        {
            get => standardized;
            set
            {
                SetProperty(ref standardized, value);
                this.UpdateGraph();
                this.TestPoint();
            }
        }

        public string WinningColor
        {
            get => winningColor;
            set => SetProperty(ref winningColor, value);
        }
    }
}
