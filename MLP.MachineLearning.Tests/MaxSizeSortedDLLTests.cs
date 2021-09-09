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

            DLL.AddAndTrim(new Node(10));

            // Assert

            Assert.AreEqual<double>(10, DLL.Head.Data);


        }

        [TestMethod]
        public void Return_as_List()
        {
            // Arrange
            MaxSizeSortedDLL DLL = new MaxSizeSortedDLL(5);
            DLL.AddAndTrim(new Node(10));
            DLL.AddAndTrim(new Node(12));
            DLL.AddAndTrim(new Node(15));

            // Act

            List<double> dll_list = DLL.ReturnAsList();
    

            // Assert

            Assert.AreEqual<double>(10, dll_list[2]);
            Assert.AreEqual<double>(12, dll_list[1]);
            Assert.AreEqual<double>(15, dll_list[0]);
        }


        [TestMethod]
        public void Add_Over_Capacity()
        {
            // Arrange
            MaxSizeSortedDLL DLL = new MaxSizeSortedDLL(2);
            DLL.AddAndTrim(new Node(10));
            DLL.AddAndTrim(new Node(12));


            // Act

            DLL.AddAndTrim(new Node(15));
            List<double> dll_list = DLL.ReturnAsList();

            // Assert

            Assert.AreEqual<int>(2, dll_list.Count);
            Assert.AreEqual<double>(12, dll_list[1]);
            Assert.AreEqual<double>(15, dll_list[0]);

        }
    }
}
