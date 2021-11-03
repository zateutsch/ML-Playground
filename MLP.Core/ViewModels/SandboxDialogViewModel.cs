using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using MLP.Core.Models;

namespace MLP.Core.ViewModels
{
    public class SandboxDialogViewModel : ObservableObject
    {
        private string xFeatureName = "X";
        private string yFeatureName = "Y";
        private double minX = 0;
        private double minY = 0;
        private double maxX = 100;
        private double maxY = 100;

        public ChartParameters GetChartParameters()
        {
            return new ChartParameters(this.XFeatureName, this.YFeatureName, this.MinX, this.MinY, this.MaxX, this.MaxY);
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
