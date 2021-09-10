﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MLP.MachineLearning.Services;

namespace MLP.MachineLearning.Tests
{

    [TestClass]
    public class MathHelperTests
    {

        [TestMethod]
        public void Euclidean_Distance_1()
        {
            // Arrange
            MathHelper helper = new MathHelper();
            List<double> l1 = new List<double>(new[] { 10.0, 5.0});
            List<double> l2 = new List<double>(new[] { 10.0, 10.0 });

            // Act

            double result = helper.EuclideanDistance(l1, l2);

            // Assert

            Assert.AreEqual<double>(5.0, result);

        }
    }
}
