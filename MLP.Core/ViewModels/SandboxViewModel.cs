using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using MLP.Core.Models;
using MLP.Core.Common;
using MLP.Core.Services;

namespace MLP.Core.ViewModels
{
    public class SandboxViewModel : ObservableObject
    {
        private string xFeatureName = "X";
        private string yFeatureName = "Y";
        private double minX = 50;
        private double minY = 0;
        private double maxX = 75;
        private double maxY = 100;
      
        public SandboxViewModel()
        {
            GraphSeries = new ObservableCollection<NestedSeries>();
            GraphSeries.Add(new NestedSeries(new double[] { }, new double[] { }));
            SandboxData = new List<Point>();
            SandboxDataSet = new DataSet();
            this.SandboxDataSet.RegressionData = new Dictionary<string, List<double>>();
        }
        public void SetChartParameters(ChartParameters chartParams)
        {
            this.XFeatureName = chartParams.XFeatureName;
            this.YFeatureName = chartParams.YFeatureName;
            this.MinX = chartParams.MinX;
            this.MinY = chartParams.MinY;
            this.MaxX = chartParams.MaxX;
            this.MaxY = chartParams.MaxY;
            this.SandboxDataSet.RegressionData.Add(XFeatureName, new List<double>());
            this.SandboxDataSet.RegressionData.Add(YFeatureName, new List<double>());
        }

        public List<Point> SandboxData { get; set; }
        public DataSet SandboxDataSet { get; set; } 
        public ObservableCollection<NestedSeries> GraphSeries { get; set; }

        public DataSet CreateDataSet()
        {
            SandboxDataSet.DisplayName = "Sandbox Data Set";
            SandboxDataSet.DefaultFeatureX = this.XFeatureName;
            SandboxDataSet.DefaultFeatureY = this.YFeatureName;
            SandboxDataSet.EnabledModels = new List<string>(new[] { "kmeans" });

            return SandboxDataSet;
        }

        public void AddPointToGraph(double x, double y)
        {
            SandboxData.Add(new Point(x, y));
            SandboxDataSet.RegressionData[XFeatureName].Add(x);
            SandboxDataSet.RegressionData[YFeatureName].Add(y);
            GraphSeries.Clear();
            GraphSeries.Add(new NestedSeries(SandboxData));
        }
        public string XFeatureName
        {
            get => xFeatureName;
            set => SetProperty(ref xFeatureName, value);
        }
        public string YFeatureName
        {
            get => yFeatureName;
            set => SetProperty(ref yFeatureName, value);
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
