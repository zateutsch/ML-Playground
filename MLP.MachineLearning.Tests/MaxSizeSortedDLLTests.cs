using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MLP.MachineLearning.Services;

namespace MLP.MachineLearning.Tests
{
    [TestClass]
    public class MaxSizeSortedDLLTests
    {

        [TestMethod]
        public void Add_Node_to_Empty_List()
        {

            // Arrange
            MaxSizeSortedDLL DLL = new MaxSizeSortedDLL(5);

            // Act

            DLL.AddAndTrim(new Node(10, 1));

            // Assert

            Assert.AreEqual<double>(10, DLL.Head.Data);


        }

        [TestMethod]
        public void Return_as_Dictionary()
        {
            // Arrange
            MaxSizeSortedDLL DLL = new MaxSizeSortedDLL(5);
            DLL.AddAndTrim(new Node(10, 1));
            DLL.AddAndTrim(new Node(12, 3));
            DLL.AddAndTrim(new Node(15, 5));

            // Act

            Dictionary<int, double> dll_dictionary = DLL.ReturnAsDictionary();
    

            // Assert

            Assert.AreEqual<double>(10, dll_dictionary[1]);
            Assert.AreEqual<double>(12, dll_dictionary[3]);
            Assert.AreEqual<double>(15, dll_dictionary[5]);
        }


        [TestMethod]
        public void Add_Over_Capacity()
        {
            // Arrange
            MaxSizeSortedDLL DLL = new MaxSizeSortedDLL(2);
            DLL.AddAndTrim(new Node(10, 1));
            DLL.AddAndTrim(new Node(12, 3));


            // Act

            DLL.AddAndTrim(new Node(15, 5));
            Dictionary<int, double> dll_dictionary = DLL.ReturnAsDictionary();

            // Assert

            Assert.AreEqual<int>(2, dll_dictionary.Count);
            Assert.AreEqual<double>(12, dll_dictionary[3]);
            Assert.AreEqual<double>(15, dll_dictionary[5]);

        }
    }
}
