using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using MLP.Core.Models;

namespace MLP.Core.ViewModels
{
    public class ModelsPageViewModel
    {
        public ObservableCollection<ModelPreview> ModelPreviews { get; set; }
        public ModelsPageViewModel()
        {
            this.ModelPreviews = new ObservableCollection<ModelPreview>();
            ModelPreview KNN = new ModelPreview();
            KNN.ImagePath = "../Assets/Images/KNNPreviewImage.png";
            KNN.ModelName = "K-Nearest Neighbors";

            this.ModelPreviews.Add(KNN);
        }
    }
}