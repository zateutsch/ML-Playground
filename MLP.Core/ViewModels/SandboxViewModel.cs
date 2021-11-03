using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using MLP.Core.Models;
using MLP.Core.Common;

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

        public void SetChartParameters(ChartParameters chartParams)
        {
            this.XFeatureName = chartParams.XFeatureName;
            this.YFeatureName = chartParams.YFeatureName;
            this.MinX = chartParams.MinX;
            this.MinY = chartParams.MinY;
            this.MaxX = chartParams.MaxX;
            this.MaxY = chartParams.MaxY;
        }

        public List<Point> SandboxData { get; set; }
        public ObservableCollection<NestedSeries> GraphSeries { get; set; } = new ObservableCollection<NestedSeries>();
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
