using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SearchEngine;

namespace SearchEngineTests
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void TestGetDotProduct1()
        {
            double[] vectorList1 = {1.0, 0.89, 0, 1.0, 2.14, 3.414, 1.414};
            double[] vectorList2 = { 1.12, 0.91, 1.414, 0, 0.89, 0, 1.0 };
            var vector1 = new Vector(vectorList1);
            var vector2 = new Vector(vectorList2);

            double expected_result = 5.2485;
            double actual_result = Vector.GetDotProduct(vector1, vector2);
            Assert.AreEqual(expected_result, actual_result, "Test GetDotProduct1 failed");
        }

        [TestMethod]
        public void TestGetDotProduct2()
        {
            double[] vectorList1 = { 0, 0, 0, 0, 0, 0, 0};
            double[] vectorList2 = { 1.12, 0.91, 1.414, 0, 0.89, 0, 1.0 };
            var vector1 = new Vector(vectorList1);
            var vector2 = new Vector(vectorList2);

            double expected_result = 0;
            double actual_result = Vector.GetDotProduct(vector1, vector2);
            Assert.AreEqual(expected_result, actual_result, "Test GetDotProduct2 failed");
        }

        [TestMethod]
        public void TestGetDotProduct3()
        {
            double[] vectorList1 = { 0, 0, 0, 0, 0, 0, 0 };
            double[] vectorList2 = { 0, 0, 0, 0, 0, 0, 0 };
            var vector1 = new Vector(vectorList1);
            var vector2 = new Vector(vectorList2);

            double expected_result = 0;
            double actual_result = Vector.GetDotProduct(vector1, vector2);
            Assert.AreEqual(expected_result, actual_result, "Test GetDotProduct3 failed");
        }

        [TestMethod]
        public void TestGetVectorNorm1()
        {
            double[] vectorList1 = { 0, 0, 0, 0, 0, 0, 0 };
            var vector1 = new Vector(vectorList1);

            double expected_result = 0;
            double actual_result = Vector.GetVectorNorm(vector1);
            Assert.AreEqual(expected_result, actual_result, "Test GetVectorNorm1 failed");
        }

        [TestMethod]
        public void TestGetVectorNorm2()
        {
            double[] vectorList1 = { 1.0, 0.89, 0, 1.0, 2.14, 3.414, 1.414 };
            var vector1 = new Vector(vectorList1);

            double expected_result = 21.026492;
            double actual_result = Vector.GetVectorNorm(vector1);
            Assert.AreEqual(expected_result, actual_result, "Test GetVectorNorm2 failed");
        }

        [TestMethod]
        public void TestGetSimilarityScore1()
        {
            double[] vectorList1 = { 1.0, 0.89, 0, 1.0, 2.14, 3.414, 1.414 };
            double[] vectorList2 = { 1.12, 0.91, 1.414, 0, 0.89, 0, 1.0 };
            var vector1 = new Vector(vectorList1);
            var vector2 = new Vector(vectorList2);

            double expected_result = Math.Cos(5.2485/(5.873996 * 21.026492));
            double actual_result = Vector.GetSimilarityScore(vector1, vector2);
            Assert.AreEqual(expected_result, actual_result, "Test GetSimilarityScore1 failed");
        }

        [TestMethod]
        public void TestGetSimilarityScore2()
        {
            double[] vectorList1 = { 0, 0, 0, 0, 0, 0, 0 };
            double[] vectorList2 = { 1.12, 0.91, 1.414, 0, 0.89, 0, 1.0 };
            var vector1 = new Vector(vectorList1);
            var vector2 = new Vector(vectorList2);

            double expected_result = 0;
            double actual_result = Vector.GetSimilarityScore(vector1, vector2);
            Assert.AreEqual(expected_result, actual_result, "Test GetSimilarityScore2 failed");
        }
    }
}
