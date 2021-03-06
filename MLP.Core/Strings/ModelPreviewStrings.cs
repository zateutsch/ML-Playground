using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Models;

namespace MLP.Core.Strings
{
    public static class ModelPreviewStrings
    {
        public static List<ModelPreview> ModelPreviews = new List<ModelPreview>(new[]
        {
            new ModelPreview("K-Nearest Neighbors", "../Assets/Images/KNN-preview.png", "Classify data by comparing it to nearby points", "knn"),
            new ModelPreview("K-Means Clusters", "../Assets/Images/Kmeans-preview.png", "Cluster unlabeled data into similar, segmented groups", "kmeans")
        });
    }
}
