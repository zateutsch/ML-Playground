using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MLP.Core.Interfaces;
using MLP.Core.Models;
using MLP.Core.Services;


namespace MLP.Core.Tests
{

    [TestClass]
    public class ClassificationKNNServiceTests
    {

        IDataService dataService;
        IMathHelper mathHelper;

        DataSet dataSet;
        ClassificationKNNService KNN;

        [TestInitialize]
        public void TestInitialize()
        {
            dataService = new DataService();
            mathHelper = new MathHelper();

            Dictionary<string, List<string>> classData = new Dictionary<string, List<string>>();
            Dictionary<string, List<double>> regData = new Dictionary<string, List<double>>();

            classData.Add("rain", new List<string>(new[] { "yes", "no", "no", "yes", "no", "yes", "yes", "no" }));
            regData.Add("cloudcover", new List<double>(new[] { 70.0, 80.0, 30.0, 20.0, 5.0, 3.0, 90.0, 45.0 }));
            regData.Add("temp", new List<double>(new[] { 50.0, 60.0, 62.0, 68.0, 72.0, 73.0, 61.0, 58.0 }));
            regData.Add("humidity", new List<double>(new[] { 20.0, 20.0, 12.0, 18.0, 12.0, 23.0, 21.0, 18.0 }));

            dataSet = new DataSet();

            dataSet.RegressionData = regData;
            dataSet.ClassificationData = classData;

            KNN = new ClassificationKNNService(dataSet, dataService, mathHelper);

        }

        [TestMethod]
        public void Train_1()
        {
            // Arrange 

            // Act

            KNN.Train("temp", "cloudcover", "rain");

            // Assert

            CollectionAssert.AreEqual(dataSet.RegressionData["temp"], KNN.CurrentDataX);
            CollectionAssert.AreEqual(dataSet.RegressionData["cloudcover"], KNN.CurrentDataY);
            CollectionAssert.AreEqual(dataSet.ClassificationData["rain"], KNN.TargetData);

        }

        [TestMethod]
        public void Classify_1()
        {
            // Arrange

            this.KNN.Train("temp", "cloudcover", "rain");

            // Act

            string result = KNN.Classify(71.0, 4.0);

            // Assert

            Assert.AreEqual<string>("yes", result);
        }

        [TestMethod]
        public void Classify_Change_K()
        {
            // Arrange

            KNN.Train("temp", "cloudcover", "rain");
            KNN.Kparam = 1;

            // Act

            string result = KNN.Classify(71.0, 4.0);

            // Assert

            Assert.AreEqual<string>("no", result);
        }

        [TestMethod]
        public void Get_Label_Specific_Series_From_Data()
        {
            // Arrange 

            // Act

            KNN.Train("temp", "cloudcover", "rain");

            // Assert
        }



    }
}
