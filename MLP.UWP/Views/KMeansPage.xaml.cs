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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MLP.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KMeansPage : Page
    {
        public KMeansViewModel KMViewModel;
        public InfoPaneViewModel InfoViewModel;

        public KMeansPage()
        {
            this.KMViewModel = App.Services.GetRequiredService<KMeansViewModel>();
            this.InfoViewModel = App.Services.GetRequiredService<InfoPaneViewModel>();
            this.InitializeComponent();
        }
    }
}
