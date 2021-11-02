using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MLP.Core.Models
{
    public class ModelPreview : ObservableObject
    {
        private readonly int _fontSizeName = 20;
        private readonly int _fontSizeInfo = 14;

        private string displayText;
        private int fontSize;

        public string ModelName { set; get; }
        public string ImagePath { set; get; }
        public string InfoText { get; set; }
        public string Key { get; set; }

        public ModelPreview(string modelName, string imagePath, string InfoText, string key)
        {
            this.ModelName = modelName;
            this.ImagePath = imagePath;
            this.InfoText = InfoText;
            this.Key = key;
            this.FontSize = this._fontSizeName;
            this.DisplayText = ModelName;
        }
        public string DisplayText
        {
            get => displayText;
            set => SetProperty(ref displayText, value);
        }

        public int FontSize
        {
            get => fontSize;
            set => SetProperty(ref fontSize, value);
        }

        public void PointerOver()
        {
            this.FontSize = this._fontSizeInfo;
            this.DisplayText = InfoText;
        }

        public void PointerExit()
        {
            this.FontSize = this._fontSizeName;
            this.DisplayText = ModelName;
        }
    }
}
