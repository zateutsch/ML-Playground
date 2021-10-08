using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MLP.Core.ViewModels
{
    // ViewModel for InfoPanes for each model
    public class InfoPaneViewModel : ObservableObject
    {
        private bool _isPaneOpen = true;
        public bool IsPaneOpen
        {
           get => _isPaneOpen;
           set => SetProperty(ref _isPaneOpen, value);
        }

        public void TogglePaneOpen()
        {
            this.IsPaneOpen = !this.IsPaneOpen;
        }
    }
}
