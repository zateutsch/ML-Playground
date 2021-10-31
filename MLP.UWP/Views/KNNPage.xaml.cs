using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using MLP.Core.ViewModels;
using MLP.Core.Services;
using MLP.Core.Models;
using MLP.UWP.Services;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MLP.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KNNPage : Page
    {

        public ClassifyKNNViewModel KNNViewModel;
        public InfoPaneViewModel InfoViewModel;
        public GraphPaletteService PaletteService;

        public KNNPage()
        { 
            this.KNNViewModel = App.Services.GetRequiredService<ClassifyKNNViewModel>();
            this.InfoViewModel = App.Services.GetRequiredService<InfoPaneViewModel>();
            this.PaletteService = App.Services.GetRequiredService<GraphPaletteService>();
            
            this.InfoViewModel.SetInfoItemsFromList(MLP.Core.Strings.InfoPaneStrings.KNNInfo);

            this.InitializeComponent();

            UpdatePalette();
        }
        private void ReloadPageWithNewData(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListBox).IsLoaded)
            {
                this.KNNViewModel.UpdateCurrentDataModelMapping();
                Frame.Navigate(typeof(KNNPage));
            }
             
        }

        private void UpdatePalette()
        {
            this.ModelChart.Palette = PaletteService.DefaultKNNPalette();
            this.KNNViewModel.FirstSeriesColor = (this.ModelChart.Palette.FillEntries.Brushes[0] as SolidColorBrush).Color.ToString();
            this.KNNViewModel.SecondSeriesColor = (this.ModelChart.Palette.FillEntries.Brushes[1] as SolidColorBrush).Color.ToString();
            this.KNNViewModel.TestSeriesColor = (this.ModelChart.Palette.FillEntries.Brushes[2] as SolidColorBrush).Color.ToString();
        }
    }
}
