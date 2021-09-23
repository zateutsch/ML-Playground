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
    public sealed partial class MainPage : Page
    {

        public ClassifyKNNViewModel ViewModel { get; set; }
        public MainPage()
        { 
            this.InitializeComponent();
            MathHelper mathHelper = new MathHelper();

            DataFileService dataFileService = new DataFileService();
            DataSet dataSet = Task.Run(() => dataFileService.ReadJsonToDataSet("Weather-Test-001.json")).Result;
            DataService dataService = new DataService(dataSet);

            ClassificationKNNService KNN = new ClassificationKNNService(dataSet, dataService, mathHelper);
            KNN.Train("cloudcover", "humidity", "rain");

            this.ViewModel = new ClassifyKNNViewModel(KNN);

        }
    }
}
