
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MLP.UWP.Services;
using System.Collections.Generic;
using MLP.Core.Models;

namespace MLP.UWP.Tests
{
    [TestClass]
    public class DataServiceTests
    {

        IDataFileService dataFileService;
        DataSet dataSet;

        [TestInitialize]
        public void TestInitialize()
        {
            dataFileService = new DataFileService();

            Dictionary<string, List<string>> classData = new Dictionary<string, List<string>>();
            Dictionary<string, List<double>> regData = new Dictionary<string, List<double>>();

            classData.Add("rain", new List<string>(new[] { "yes", "no", "no", "yes", "no", "yes", "yes", "no" }));
            regData.Add("cloudcover", new List<double>(new[] { 70.0, 80.0, 30.0, 20.0, 5.0, 3.0, 90.0, 45.0 }));
            regData.Add("temp", new List<double>(new[] { 50.0, 60.0, 62.0, 68.0, 72.0, 73.0, 61.0, 58.0 }));
            regData.Add("humidity", new List<double>(new[] { 20.0, 20.0, 12.0, 18.0, 12.0, 23.0, 21.0, 18.0 }));

            dataSet = new DataSet();

            dataSet.RegressionData = regData;
            dataSet.ClassificationData = classData;
            dataSet.Name = "Weather-001";


        }


        [TestMethod]
        public void Write_File_to_JSON()
        {
            dataFileService.WriteDataSetToJSON(this.dataSet);
        }
    }
}
