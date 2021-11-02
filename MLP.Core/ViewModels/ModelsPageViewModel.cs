using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using MLP.Core.Models;
using MLP.Core.Strings;

namespace MLP.Core.ViewModels
{
    public class ModelsPageViewModel
    {
        public ObservableCollection<ModelPreview> ModelPreviews { get; set; }

        public ModelsPageViewModel()
        {
            this.ModelPreviews = new ObservableCollection<ModelPreview>(ModelPreviewStrings.ModelPreviews);
        }
    }

}