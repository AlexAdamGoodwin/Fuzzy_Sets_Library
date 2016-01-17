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

        /*=============================== Тест носителя ====================================*/
        [TestMethod]
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

        /*======================================= Тест ядра ===================================*/
        [TestMethod]
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

        /*================================ Тест точки перехода ================================*/
        [TestMethod]
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
        /*================================ Тест точки перехода ================================*/
        [TestMethod]
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
        /*================================ Тест нормализация ================================*/
        [TestMethod]
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
        /*================================ Тест нормализация ================================*/
        [TestMethod]
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
        /*================================ Тест расстояние ================================*/
        [TestMethod]
        public void Test_DistLinear()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Test2 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            double Unswer = 1.40;
            Assert.AreEqual(Fuz.DistLin(Test,Test2), Unswer);
        }
        /*========================== Тест расстояние Евклидово =============================*/
        [TestMethod]
        public void Test_DistEucl()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Test2 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            double Unswer = 1.08;
            Assert.AreEqual(Fuz.DistEucl(Test, Test2), Unswer);
        }
        /*======================== Тест ближайшее четкое множество =========================*/
        [TestMethod]
        public void Test_ClosestStrict()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Unswer = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,1.0),
              new KeyValuePair<double,double>(4,1.0)
            };
            CollectionAssert.AreEqual(Fuz.StrictSet(Test), Unswer);
        }

        /*======================== Тест на вогнутость =========================*/
        [TestMethod]
        public void Test_Convex()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.25),
              new KeyValuePair<double,double>(2,0.375),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.4),
              new KeyValuePair<double,double>(5,0.3)
            };
            Assert.AreEqual(Fuz.СonvexFuz(Test), true);
        }
        /*======================== Тест на выпуклость =========================*/
        [TestMethod]
        public void Test_Concave()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.6),
              new KeyValuePair<double,double>(2,0.5),
              new KeyValuePair<double,double>(3,0.55),
              new KeyValuePair<double,double>(4,0.7)
            };
            Assert.AreEqual(Fuz.ConcaveFuz(Test), true);
        }
        /*======================== Тест на равенство =========================*/
        [TestMethod]
        public void Test_Equal()
        {
            List<KeyValuePair<double, double>> Test1 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> Test2 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            Assert.AreEqual(Fuz.EqualFuz(Test1,Test2),true);
        }
        /*======================== Тест на подмножество =========================*/
        [TestMethod]
        public void Test_SubSet()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            Assert.AreEqual(Fuz.IsSubFuz(Test, Test), true);
        }
        /*======================== Тест Операция дополнения =========================*/
        [TestMethod]
        public void Test_Addon()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Unswer = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,1.0),
              new KeyValuePair<double,double>(4,1.0)
            };
            CollectionAssert.AreEqual(Fuz.StrictSet(Test), Unswer);
        }
        /*======================== Тест  Максиминная операция объединения =========================*/
        [TestMethod]
        public void Test_Unite()
        {
            List<KeyValuePair<double, double>> Test = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> Unswer = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,1.0),
              new KeyValuePair<double,double>(4,1.0)
            };
            CollectionAssert.AreEqual(Fuz.StrictSet(Test), Unswer);
        }
        /*========================= Последние методы ==================================*/
        /*======================== Тест  Рефлекcивность =========================*/
        [TestMethod]
        public void Test_IsReflective()
        {
            double[,] Test = {{1,0.5},{0.8,1}};
            bool Unswer = true;
            Assert.AreEqual(Fuz.Reflective(Test), Unswer);
        }
        /*======================== Тест  АнтиРефлекcивность =========================*/
        [TestMethod]
        public void Test_IsAntiReflective()
        {
            double[,] Test = { { 0, 0.5 }, { 0.8, 0 } };
            bool Unswer = true;
            Assert.AreEqual(Fuz.AntiReflective(Test), Unswer);
        }
        /*======================== Тест  Симметричность =========================*/
        [TestMethod]
        public void Test_IsSymmetric()
        {
            double[,] Test = { { 0, 0.5 }, { 0.5, 0 } };
            bool Unswer = true;
            Assert.AreEqual(Fuz.Symmetric(Test), Unswer);
        }
        /*======================== Тест  Acимметричность =========================*/
        [TestMethod]
        public void Test_IsAsymmetric()
        {
            double[,] Test = { { 0, 1 }, { 0, 0 } };
            bool Unswer = true;
            Assert.AreEqual(Fuz.Asymmetric(Test), Unswer);
        }
        /*======================== Тест  Aнтиcимметричность =========================*/
        [TestMethod]
        public void Test_IsAntisymmetric()
        {
            double[,] Test = { { 0, 1 }, { 0, 0 } };
            bool Unswer = true;
            Assert.AreEqual(Fuz.Antisymmetric(Test), Unswer);
        }
        /*======================== Тест  Объединение Бинарных отношений =========================*/
        [TestMethod]
        public void Test_BinaryUnite()
        {
            double[,] Test = { { 0.1, 1, 1 }, { 0.5, 0.8,0.9 }, { 0.5, 1, 0.9 } };
            double[,] Test1 = { { 0.2, 0, 0.8 }, { 0.5, 0.4, 1 }, { 0.6, 1, 1 } };
            double[,] Test0 = { { 0, 1, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
            double[,] Test11 = { { 1, 0, 0 }, { 0, 0, 1 }, { 1, 1, 1 } };
            double[,] Unswer = { { 0.2, 1, 1 }, { 0.5, 0.8, 1 }, { 0.6, 1, 1 } };
            double[,] Unswer1= { { 1,1,1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            CollectionAssert.AreEqual(Fuz.BinaryUnite(Test, Test1), Unswer);
            CollectionAssert.AreEqual(Fuz.BinaryUnite(Test0, Test11), Unswer1);
        }
        /*======================== Тест  Пересечение Бинарных отношений =========================*/
        [TestMethod]
        public void Test_BinaryInter()
        {
            double[,] Test = { { 0.1, 1, 1 }, { 0.5, 0.8, 0.9 }, { 0.5, 1, 0.9 } };
            double[,] Test1 = { { 0.2, 0, 0.8 }, { 0.5, 0.4, 1 }, { 0.6, 1, 1 } };
            double[,] Test0 = { { 0, 1, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
            double[,] Test11 = { { 1, 0, 0 }, { 0, 0, 1 }, { 1, 1, 1 } };
            double[,] Unswer = { { 0.1, 0, 0.8 }, { 0.5, 0.4, 0.9 }, { 0.5, 1, 0.9 } };
            double[,] Unswer1 = { {0, 0, 0 }, { 0, 0, 1 }, { 0, 0, 0 } };
            CollectionAssert.AreEqual(Fuz.BinaryInter(Test, Test1), Unswer);
            CollectionAssert.AreEqual(Fuz.BinaryInter(Test0, Test11), Unswer1);
        }
        /*======================== Тест  Разности Бинарных отношений =========================*/
        [TestMethod]
        public void Test_BinarySub()
        {
            double[,] Test = { { 0.1, 1, 1 }, { 0.5, 0.8, 0.9 }, { 0.5, 1, 0.9 } };
            double[,] Test1 = { { 0.2, 0, 0.8 }, { 0.5, 0.4, 1 }, { 0.6, 1, 1 } };
            double[,] Unswer = { { 0.1,1,0.2 }, { 0.5,0.6,0}, {0.4,0,0 }};
            CollectionAssert.AreEquivalent(Fuz.BinarySub(Test,Test1),Unswer);
        }
        /*======================== Тест симметричной разности бинарных отношений =========================*/
        [TestMethod]
        public void Test_BinarySymmetricSub()
        {
            double[,] Test = { { 0.1, 1, 1 }, { 0.5, 0.8, 0.9 }, { 0.5, 1, 0.9 } };
            double[,] Test1 = { { 0.2, 0, 0.8 }, { 0.5, 0.4, 1 }, { 0.6, 1, 1 } };
            double[,] Unswer = { { 0.2, 1, 0.2 }, { 0.5, 0.6, 0.1 }, { 0.5, 0, 0.1 } };
            CollectionAssert.AreEquivalent(Fuz.BinarySymmetricSub(Test, Test1), Unswer);
        }
        /*======================== Тест противоположной матрицы  =========================*/
        [TestMethod]
        public void Test_BinaryReverse()
        {
            double[,] Test = { { 0.1, 1, 1 }, { 0.5, 0.8, 0.9 }, { 0.5, 1, 0.9 } };
            double[,] Unswer = { { 0.9, 0, 0 }, { 0.5, 0.2, 0.1 }, { 0.5, 0, 0.1 } };
            CollectionAssert.AreEquivalent(Fuz.BinaryReverse(Test), Unswer);
        }
        /*======================== Тест max-min композиции =========================*/
        [TestMethod]
        public void Test_Binary_Max_Min()
        {
            double[,] Test = { { 0.2, 0.5 }, { 0.6, 1 }};
            double[,] Test1 = { { 0.3, 0.6, 0.8 }, { 0.7, 0.9, 0.4 } };
            double[,] Unswer = { { 0.5, 0.5, 0.4 }, { 0.7, 0.9, 0.6 } };
            CollectionAssert.AreEqual(Fuz.Binary_Max_Min_Compose(Test,Test1,"MaxMin"),Unswer);
        }
        /*======================== Тест "Транзитивность" =========================*/
        [TestMethod]
        public void Test_Transit()
        {
            double[,] Test = { { 1.0, 0.8, 0.4, 0.2, 0.0 }, 
                               { 0.8, 1.0, 0.1, 0.7, 0.2 },
                               { 0.4, 0.1, 1.0, 0.6, 0.5 },
                               { 0.2, 0.7, 0.6, 1.0, 0.0 },
                               { 0.0, 0.2, 0.5, 0.0, 1.0}};
            bool Unswer = true;
            Assert.AreEqual(Fuz.Transitivity(Test), Unswer);
        }
        /*=============== Тест на экзамен вопрос №17  "Ограниченное пересечение" ================*/

            }      
        }
   
