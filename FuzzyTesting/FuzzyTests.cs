using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzySets;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.Linq;
namespace FuzzyTesting
{
    [TestClass]
    public class FuzzyTests
    {
        [TestMethod]
        /*=============================== Тест носителя ====================================*/
        public void Test_Support()
        {
            List<KeyValuePair<double, double>> TestSet = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Unswer= new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
           CollectionAssert.AreEqual(Fuz.FuzzySupport(TestSet),Unswer);
        }
        [TestMethod]
        /*======================================= Тест ядра ===================================*/
        public void Test_Core()
        {
            List<KeyValuePair<double, double>> TestSet = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            List<KeyValuePair<double, double>> Unswer = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(2,1.0),
              new KeyValuePair<double,double>(4,1.0)
            };
            CollectionAssert.AreEqual(Fuz.FuzzyCore(TestSet), Unswer);
        }
        [TestMethod]
        /*================================ Тест точки перехода ================================*/
        public void Test_ChangePoint()
        {
            List<KeyValuePair<double, double>> TestSet = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Unswer = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,0.5),
            };
            CollectionAssert.AreEqual(Fuz.ChangePoint(TestSet), Unswer);
        }
        [TestMethod]
        /*================================ Тест точки перехода ================================*/
        public void Test_Suprem()
        {
            List<KeyValuePair<double, double>> TestSet = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            double Unswer = 0.6;
            Assert.AreEqual(Fuz.FuzzySuprem(TestSet), Unswer);
        }
        [TestMethod]
        /*================================ Тест нормализация ================================*/
        public void Test_Normal()
        {
            List<KeyValuePair<double, double>> TestSet = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Unswer = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.67),
              new KeyValuePair<double,double>(2,0.0),
              new KeyValuePair<double,double>(3,0.83),
              new KeyValuePair<double,double>(4,1.0)
            };
            CollectionAssert.AreEqual(Fuz.Normalize(TestSet), Unswer);
        }
        [TestMethod]
        /*================================ Тест нормализация ================================*/
        public void Test_Entropy()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.7),
              new KeyValuePair<double,double>(2,0.9),
              new KeyValuePair<double,double>(3,0),
              new KeyValuePair<double,double>(4,0.6),
              new KeyValuePair<double,double>(5,0.5),
              new KeyValuePair<double,double>(6,1)
            };
            double Unswer = 2.276;
            Assert.AreEqual(Fuz.Entropy(Test), Unswer);
        }
    }
}
