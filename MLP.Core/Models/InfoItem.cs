using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Models
{
    public class InfoItem : ObservableObject
    {
        private string displaying = "Header";
        public string Header { get; set; }
        public string Content { get; set; }
        public string Displaying
        {
            get => displaying;
            set => SetProperty(ref displaying, value);
        }
        public InfoItem(string header, string content)
        {
            this.Header = header;
            this.Content = content;
        }
    }
}

