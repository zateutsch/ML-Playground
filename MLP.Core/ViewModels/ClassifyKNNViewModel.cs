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

        // Primary Observable Collections - Core of KNN Graph Representation

        // Three observable collections of NestedSeries objects (see MLP.Core.Common.NestedSeries)
        // Contains dynamical generated series of three different types

        // 1. Training data separated by label
        public ObservableCollection<NestedSeries<double>> TrainingData { get; set; }
        // 2. Test data (potentially single point, tbd)
        public ObservableCollection<NestedSeries<double>> TestData { get; set; }

        // 3. KNN process visualization data
        // This will take the form of series of two data points connected by line
        // Test point <=> closest point #1 - #k
        public ObservableCollection<NestedSeries<double>> VisualizationData { get; set; }


        // Other Observable Properties

        // Constructor
        public ClassifyKNNViewModel(IClassificationKNN knn)
        {
            this._knn_service = knn;
        }
    }


}
