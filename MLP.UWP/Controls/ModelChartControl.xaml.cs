using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Telerik.UI.Xaml.Controls.Chart;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MLP.UWP.Controls
{
    public sealed partial class ModelChartControl : UserControl
    {
        public ModelChartControl()
        {
            this.InitializeComponent();
        }

        public System.Object Source
        {
            get { return (System.Object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(System.Object), typeof(ModelChartControl), new PropertyMetadata(0));

        public ChartPalette Palette
        {
            get { return (ChartPalette)GetValue(PaletteProperty); }
            set { SetValue(PaletteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Palette.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaletteProperty =
            DependencyProperty.Register("Palette", typeof(ChartPalette), typeof(ModelChartControl), new PropertyMetadata(0));


        public int VisualizationIndex
        {
            get { return (int)GetValue(VisualizationIndexProperty); }
            set { SetValue(VisualizationIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisualizationIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisualizationIndexProperty =
            DependencyProperty.Register("VisualizationIndex", typeof(int), typeof(ModelChartControl), new PropertyMetadata(100));

        public string AxisLabelX
        {
            get { return (string)GetValue(AxisLabelXProperty); }
            set { SetValue(AxisLabelXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisLabelX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisLabelXProperty =
            DependencyProperty.Register("AxisLabelX", typeof(string), typeof(ModelChartControl), new PropertyMetadata(0));

        public string AxisLabelY
        {
            get { return (string)GetValue(AxisLabelYProperty); }
            set { SetValue(AxisLabelYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisLabelY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisLabelYProperty =
            DependencyProperty.Register("AxisLabelY", typeof(string), typeof(ModelChartControl), new PropertyMetadata(0));
        public double XMin
        {
            get { return (double)GetValue(XMinProperty); }
            set { SetValue(XMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XMinProperty =
            DependencyProperty.Register("XMin", typeof(double), typeof(ModelChartControl), new PropertyMetadata(0.0));
        public double XMax
        {
            get { return (double)GetValue(XMaxProperty); }
            set { SetValue(XMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XMaxProperty =
            DependencyProperty.Register("XMax", typeof(double), typeof(ModelChartControl), new PropertyMetadata(0.0));
        public double YMin
        {
            get { return (double)GetValue(YMinProperty); }
            set { SetValue(YMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YMinProperty =
            DependencyProperty.Register("YMin", typeof(double), typeof(ModelChartControl), new PropertyMetadata(0.0));
        public double YMax
        {
            get { return (double)GetValue(YMaxProperty); }
            set { SetValue(YMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YMaxProperty =
            DependencyProperty.Register("YMax", typeof(double), typeof(ModelChartControl), new PropertyMetadata(0.0));


    }
}
