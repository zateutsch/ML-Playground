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
using MLP.UWP.Services;
using MLP.Core.Common;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MLP.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SandboxPage : Page
    {
        public SandboxViewModel SBViewModel;
        public GraphPaletteService PaletteService;
        public SandboxPage()
        {
            this.SBViewModel = App.Services.GetRequiredService<SandboxViewModel>();
            this.PaletteService = App.Services.GetRequiredService<GraphPaletteService>();
            this.SBViewModel.GraphSeries.Add(new NestedSeries(new[] { 10.0, 15.0, 20.0 }, new[] { 10.0, 15.0, 20.0 }));
            this.InitializeComponent();
            this.UpdatePalette();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplaySandboxDialog();
        }

        private async void DisplaySandboxDialog()
        {
            ContentDialog sandboxDialog = new ContentDialog()
            {
                Title = "Customize Your Sandbox",
                Content = new SandboxDialog(),
                CloseButtonText = "Proceed"
            };

            sandboxDialog.Closed += new TypedEventHandler<ContentDialog, ContentDialogClosedEventArgs>(ContentDialog_Closed);

            await sandboxDialog.ShowAsync();
        }

        private void UpdatePalette()
        {
            this.SandboxChart.Palette = PaletteService.DefaultKNNPalette();
        }

        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            this.SBViewModel.SetChartParameters((sender.Content as SandboxDialog).SBDViewModel.GetChartParameters());
        }
    }
}
