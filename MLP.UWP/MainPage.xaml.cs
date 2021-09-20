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
            DataService dataService = new DataService();
            MathHelper mathHelper = new MathHelper();

            Dictionary<string, List<string>> classData = new Dictionary<string, List<string>>();
            Dictionary<string, List<double>> regData = new Dictionary<string, List<double>>();

            classData.Add("rain", new List<string>(new[] { "yes", "no", "no", "yes", "no", "yes", "yes", "no" }));
            regData.Add("cloudcover", new List<double>(new[] { 70.0, 80.0, 30.0, 20.0, 5.0, 3.0, 90.0, 45.0 }));
            regData.Add("temp", new List<double>(new[] { 50.0, 60.0, 62.0, 68.0, 72.0, 73.0, 61.0, 58.0 }));
            regData.Add("humidity", new List<double>(new[] { 20.0, 20.0, 12.0, 18.0, 12.0, 23.0, 21.0, 18.0 }));

            DataSet dataSet = new DataSet();

            dataSet.RegressionData = regData;
            dataSet.ClassificationData = classData;

            ClassificationKNNService KNN = new ClassificationKNNService(dataSet, dataService, mathHelper);
            KNN.Train("cloudcover", "humidity", "rain");

            this.ViewModel = new ClassifyKNNViewModel(KNN);

        }
    }
}
