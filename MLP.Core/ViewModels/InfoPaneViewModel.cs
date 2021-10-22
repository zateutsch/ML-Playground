using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using MLP.Core.Models;

namespace MLP.Core.ViewModels
{
    // ViewModel for InfoPanes for each model
    public class InfoPaneViewModel : ObservableObject
    {
        private bool isPaneOpen = false;
        private int selectedIndex = -1;
        private int previousIndex = 0;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                previousIndex = selectedIndex == -1 ? 0 : selectedIndex;
                SetProperty(ref selectedIndex, value);
                this.UpdateInfoPaneDisplay();
            }
        }
        public bool IsPaneOpen
        {
           get => isPaneOpen;
           set => SetProperty(ref isPaneOpen, value);
        }

        public void TogglePaneOpen()
        {
            this.IsPaneOpen = !this.IsPaneOpen;
        }

        public ObservableCollection<InfoItem> InfoItems { get; set; }
        
        public void SetInfoItemsFromList(List<InfoItem> items)
        {
            this.InfoItems = new ObservableCollection<InfoItem>(items);
        }

        private void UpdateInfoPaneDisplay()
        {
            this.InfoItems[this.previousIndex].Displaying = "Header";
            this.InfoItems[this.SelectedIndex].Displaying = "Content";
        }
    }

   
}
