/* Во всех функция работы с нечеткими бинарными отношениями принимается, что элементы x,y являются равными.*/
/* Нечеткое бинарное отношение представляется в виде двумерного массива состоящем из степеней нечеткости. */
using System;
namespace FuzzySets
{
    public partial class Fuz
    {
        /*====== Reflective - возращает true, если бинарное отношение рефлексивно ========*/
        public static bool Reflective(double[,] matrix1)
        {
            bool reflective = true;
            int i = 0;
            while (i < matrix1.GetLength(0) - 1)
            {
                if (matrix1[i, i] != 1)
                {
                    reflective = false;
                }
                i++;
            }
            return reflective;
        }
        /*==================================================================================*/
        /*====== AntiReflective - возращает true, если бинарное отношение антирефлексивно ========*/
        public static bool AntiReflective(double[,] matrix1)
        {
            bool Antireflective = true;
            int i = 0;
            while (i < matrix1.GetLength(0) - 1)
            {
                if (matrix1[i, i] != 0)
                {
                    Antireflective = false;
                }
                i++;
            }
            return Antireflective;
        }
        /*==========================================================================================*/
        /*====== Symmetric - возращает true, если бинарное отношение симметрично ========*/
        public static bool Symmetric(double[,] matrix1)
        {
            bool isSymmentric = true;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (matrix1[i, j] != matrix1[j, i])
                    {
                        isSymmentric = false;
                    }
                }
            }
            return isSymmentric;
        }
        /*==========================================================================================*/
        /*====== Asymmetric - возращает true, если бинарное отношение ассиметрично ========*/
        public static bool Asymmetric(double[,] matrix1)
        {
            bool isASymmentric = true;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (Math.Min(matrix1[i, j], matrix1[j, i]) != 0)
                    {
                        isASymmentric = false;
                    }
                }
            }
            return isASymmentric;
        }
        /*==========================================================================================*/
        /*====== Antisymmetric - возращает true, если бинарное отношение антисиметрично ========*/
        public static bool Antisymmetric(double[,] matrix1)
        {
            bool isAntiSymmentric = true;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        if (Math.Min(matrix1[i, j], matrix1[j, i]) != 0)
                        {
                            isAntiSymmentric = false;
                        }
                    }
                }
            }
            return isAntiSymmentric;
        }
        /*==========================================================================================*/
        /*= BinaryAddition - возращает матрицу операции объединения между двумя бинарными отношениями =*/
        public static double[,] BinaryUnite(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrixRes = matrix1;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrixRes[i, j] = Math.Max(matrix1[i, j], matrix2[i, j]);
                    // Console.WriteLine(matrixRes[i, j]);
                }
            }
            return matrixRes;
        }
        /*============================================================================================*/
        /*= BinaryInter - возращает матрицу операции пересечения между двумя бинарными отношениями =*/
        public static double[,] BinaryInter(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrixRes = matrix1;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrixRes[i, j] = Math.Min(matrix1[i, j], matrix2[i, j]);
                    //   Console.WriteLine(matrixRes[i, j]);
                }
            }
            return matrixRes;
        }
        /*============================================================================================*/
        /*=========================== НОВЫЕ ОПЕРАЦИИ =====================================================*/
        /*= BinarySub - возращает матрицу операции разности между двумя бинарными отношениями =*/
        public static double[,] BinarySub(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrixRes = matrix1;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrixRes[i, j] = Math.Min(matrix1[i, j], Math.Round(1 - matrix2[i, j], 2));
                }
            }
            return matrixRes;
        }
        /*============================================================================================*/
        /*=========== BinarySymmetricSub - возращает матрицу операции симметричной разности между двумя бинарными отношениями ======================================*/
        public static double[,] BinarySymmetricSub(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrixRes = matrix1;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrixRes[i, j] = Math.Max(Math.Min(matrix1[i, j], Math.Round(1 - matrix2[i, j], 2)), Math.Min(Math.Round(1 - matrix1[i, j], 2), matrix2[i, j]));
                }
            }
            return matrixRes;
        }
        /*==========================================================================================================================================================*/
        /*==== BinaryReverse - возращает противоположная матрицу, матрице поданной в первом аргументе ====*/
        public static double[,] BinaryReverse(double[,] matrix1)
        {
            double[,] matrixRes = matrix1;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrixRes[i, j] = Math.Round(1 - matrix1[i, j], 2);
                }
            }
            return matrixRes;
        }
        /*=============================================================================================*/
        //==== Binary_Max_Min_Compose - возращает обратную матрицу, которая является результатом max-min ======
        //==== композиции двух нечетких отношений или композицией нечеткого отношения и нечеткого множества, ==
        //==== На вход подаются (аргумент matrix1 - для первого нечеткого отношения и matrix2- для второго), ==
        //==== третий аргумент принимает значения "MaxMin", "MinMax", "MaxProd"  =======================================
        //==============================================================================================
        public static double[,] Binary_Max_Min_Compose(double[,] matrix1, double[,] matrix2, string TypeOfCompose)
        {
            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                throw new System.ArgumentException("Rows amount of matrix1 != columns amout of matrix2 ", "original");
            }
            double[,] matrixRes = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
            for (int i = 0; i < matrixRes.GetLength(0); i++)
            {
                for (int j = 0; j < matrixRes.GetLength(1); j++)
                {
                    double S = 0;
                    for (int k = 0; k < matrix1.GetLength(1); k++)
                        switch (TypeOfCompose)
                        {
                            case "MaxMin":
                                {
                                    S = Math.Max(S, Math.Min(matrix1[i, k], matrix2[k, j]));
                                    break;
                                }
                            case "MinMax":
                                {
                                    S = Math.Min(S, Math.Max(matrix1[i, k], matrix2[k, j]));
                                    break;
                                }
                            case "MaxProd":
                                {
                                    S = Math.Max(S, matrix1[i, k] * matrix2[k, j]);
                                    break;
                                }
                            case "MaxMax":
                                {
                                    S = Math.Max(S, Math.Max(matrix1[i, k], matrix2[k, j]));
                                    break;
                                }
                            case "MinMin":
                                {
                                    S = Math.Min(S, Math.Min(matrix1[i, k], matrix2[k, j]));
                                    break;
                                }
                            case "MaxAverage":
                                {
                                    S = 0.5 * Math.Max(S, matrix1[i, k] + matrix2[k, j]);
                                    break;
                                }
                        }
                    matrixRes[i, j] = S;
                }
            }
            return matrixRes;
        }
        /*==============================================================================================*/
        public static bool Transitivity(double[,] matrix1)
        {
            bool IsTransit = true;
            double[,] MatrixCopy = matrix1;
            double[,] LastMatrix;
            double[,] CurrentMatrix;
            LastMatrix = Binary_Max_Min_Compose(MatrixCopy, MatrixCopy, "MaxMin");
            for (int i = 2; i <= MatrixCopy.GetLength(0); i++) {
                CurrentMatrix = Binary_Max_Min_Compose(LastMatrix, MatrixCopy, "MaxMin");
                if (Compare(CurrentMatrix, LastMatrix))
                {
                    for (int k = 0; k < matrix1.GetLength(0); k++)
                    {
                        for (int j = 0; j < matrix1.GetLength(0); j++)
                        {
                            if (LastMatrix[k,j] == 0)
                            {
                                IsTransit = false;
                                return IsTransit;
                            }
                        }
                    }
                }
                else
                {
                    LastMatrix = CurrentMatrix;
                }
            }
            return IsTransit;
        }
        public static bool Compare(double[,] matrix1, double[,] matrix2) {
          bool  EqualFuz = true;
            for (int i = 0; i < matrix1.GetLength(0); i++) {
                for (int j = 0; j < matrix1.GetLength(0); j++) {
                    if (matrix1[i, j] != matrix2[i, j]) {
                        EqualFuz = false;
                        return EqualFuz;
                    }
                }
            }
            return EqualFuz;
        }
    }
}
