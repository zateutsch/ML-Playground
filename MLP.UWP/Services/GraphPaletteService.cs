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
        
        public ChartPalette DefaultKNNPalette(int k = 10)
        {
            ChartPalette palette = new ChartPalette();
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(_defaultHexColors[0]));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(_defaultHexColors[1]));
            palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.TestColor));
            for (int i = 0; i < k; i++)
            {
                palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            }
            return palette;
        }

        public ChartPalette DefaultKMeansPalette(int k)
        {
            ChartPalette palette = new ChartPalette();
            for (int i = 0; i < k; i++)
            {
                palette.FillEntries.Brushes.Add(GetSolidColorBrush(_defaultHexColors[i]));
            }
            for (int i = 0; i < k; i++)
            {
                palette.FillEntries.Brushes.Add(GetSolidColorBrush(this.VisualizationColor));
            }

            return palette;
        }

        private static string _hexWhite = "#FFFFFF";
        private static string _hexBlack = "#000000";

        private static string[] _defaultHexColors =
        {
            "#6B69D6",
            "#E3008C",
            "#00B7C3",
            "#00B294",
            "#881798",
            "#78899E",
            "#ED9CDD"
        };  
        public string VisualizationColor
        {
            get
            {
                return App.Current.RequestedTheme == Windows.UI.Xaml.ApplicationTheme.Light ? _hexBlack : _hexWhite;
            }
        }

        public string TestColor
        {
            get
            {
                return App.Current.RequestedTheme == Windows.UI.Xaml.ApplicationTheme.Light ? _hexBlack : _hexWhite;
            }
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
