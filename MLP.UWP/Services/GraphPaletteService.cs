using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.UI.Xaml.Controls.Chart;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MLP.UWP.Services
{
    public class GraphPaletteService
    {
        public GraphPaletteService()
        {

        }

        public string VisualizationColor
        {
            get
            {
                return App.Current.RequestedTheme == Windows.UI.Xaml.ApplicationTheme.Light ? "#000000" : "#FFFFFF";
            }
        }
        public ChartPalette DefaultKNNPalette()
        {
            ChartPalette palette = new ChartPalette();
            palette.FillEntries.Brushes.Add(GetSolidColorBrush("#F7630C"));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush("#ECDC29"));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));

            return palette;
        }


        // code credit: Joel Joseph @ http://www.joeljoseph.net/converting-hex-to-color-in-universal-windows-platform-uwp/
        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, r, g, b));
            return myBrush;
        }

    }
}
