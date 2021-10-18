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
using MLP.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MLP.Core.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MLP.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModelsPage : Page
    {

        private Dictionary<string, Type> modelPageDictionary;
        public ModelsPageViewModel ViewModel => (ModelsPageViewModel)this.DataContext;
        public ModelsPage()
        {
            this.DataContext = App.Services.GetRequiredService<ModelsPageViewModel>();
            this.InitModelDictionary();
            this.InitializeComponent();

        }

        private void ModelsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ModelPreview model = e.ClickedItem as ModelPreview;
            this.Frame.Navigate(this.modelPageDictionary[model.Key]);
        }

        private void InitModelDictionary()
        {
            this.modelPageDictionary = new Dictionary<string, Type>();

            this.modelPageDictionary.Add("knn", typeof(KNNPage));
            this.modelPageDictionary.Add("kmeans", typeof(KMeansPage));
        }
    }
}
