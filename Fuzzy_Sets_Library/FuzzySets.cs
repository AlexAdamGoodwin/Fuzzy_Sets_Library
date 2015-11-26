//  Данный класс Fuz содержит методы осуществляющие различные операции над нечеткими множествами.
//  Список методов:
//  FuzzyGenerator - возвращает сгенерированное нечеткое множество заданной длины.
//  Пример синтаксиса: Fuz.FuzzyGenerator(5);
//  ---------------------------------------------------------------------------------------------------------------------
//  PrintOut - выводит в консоль пары!(не может получить на вход множетсво не пар) нечеткого множества
//  Пример синтаксиса: Fuz.PrintOut(Lst); - !здесь и далее в переменной Lst и Lst1 хранится список пар нечеткого множества!
//  ---------------------------------------------------------------------------------------------------------------------
//  PrintOutSingle - выводит в консоль простое множество.
//  Пример синтаксиса: Fuz.PrintOutSingle(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  FuzzySupport - возвращает носитель поданого на вход нечеткого множества.
//  Пример синтаксиса: Fuz.FuzzySupport(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  FuzzyCore -  возвращает ядро введенного нечеткого множества.
//  Пример синтаксиса: Fuz.FuzzyCore(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  ChangePoint - возвращает точку перехода нечеткого множества.
//  Пример синтаксиса: Fuz.ChangePoint(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  FuzzySuprem - возвращает высоту нечеткого множества.
//  Пример синтаксиса: Fuz.FuzzySuprem(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  Normalize - возвращает нормализованное множество поданого на вход нечеткого множества.
//  Пример синтаксиса: Fuz.Normalize(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  Entropy - возвращает значение энтропии нечеткого множества.
//  Пример синтаксиса: Fuz.Entropy(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  DistLin - возвращает расстояние Хэмминга между нечеткими множествами.
//  Пример синтаксиса: Fuz.DistLin(Lst,Lst1);
//  ---------------------------------------------------------------------------------------------------------------------
//  DistEucl - возвращает Евклидовое расстояние между нечеткими множествами.
//  Пример синтаксиса: Fuz.DistEucl(Lst,Lst1);
//  ---------------------------------------------------------------------------------------------------------------------
//  StrictSet - возвращает ближайшее четкое множество.
//  Пример синтаксиса: Fuz.StrictSet(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  ConcaveFuz - возвращает true  если вогнутость присутствует
//  Пример синтаксиса: Fuz.ConcaveFuz(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  ConvexFuz - возвращает true  если выпуклость присутствует
//  Пример синтаксиса: Fuz.ConvexFuz(Lst);
//  ---------------------------------------------------------------------------------------------------------------------
//  EqualFuz - возвращает true если нечеткие множества равны, false - если наооборот
//  Пример синтаксиса: Fuz.EqualFuz(Lst,Lst1);
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Threading;
namespace FuzzySets
{
    public partial class Fuz
    {
       static Random randKey = new Random();
        /*========== FuzzyGenerator - возвращает нечеткое множество заданной длины ==============*/
        public static List<KeyValuePair<double, double>> FuzzyGenerator(int Count,string mode)
        {

            double randValue;
            int randIndexUno;
            int randIndexZero;
            List<KeyValuePair<double, double>> GenFuzzy = new List<KeyValuePair<double, double>>();
           if (mode == "real")
            {
                for (int i = 0; i < Count; i++)
                {
                    randValue = Math.Round((randKey.Next(101)) / 100.0, 2);
                    GenFuzzy.Add(new KeyValuePair<double, double>(randKey.Next(256), randValue));
                }
            }
          if (mode == "special") 
            {
                randIndexUno = randKey.Next(0, Count);
                randIndexZero = randKey.Next(0, Count);
                while (randIndexUno == randIndexZero) {
                    randIndexZero = randKey.Next(0, Count);   
                }
                for (int i = 0; i < Count; i++)
                {
                    randValue = Math.Round((randKey.Next(100)) / 99.0, 2);
                    if (i == randIndexUno)
                    {
                        GenFuzzy.Add(new KeyValuePair<double, double>(randKey.Next(255), 1.0));
                    }
                    else if (i == randIndexZero)
                    {
                        GenFuzzy.Add(new KeyValuePair<double, double>(randKey.Next(255), 0.0));
                    }
                    else
                    {
                        GenFuzzy.Add(new KeyValuePair<double, double>(randKey.Next(255), randValue));
                    }
                }
            }
            return GenFuzzy;    
        }
        /*======================================================================================*/
        /*======= PrintOut() - Функция вывода нечеткого множества =======*/
        public static void PrintOut(List<KeyValuePair<double, double>> InputList)
        {
            Console.Write("{");
            InputList.ForEach(delegate(KeyValuePair<double, double> Pair)
            {
                Console.Write(Pair);
                Console.Write("");
            });
            Console.Write("}");
        }
        /*================================================================*/
        /*======= PrintOutSingle() - Функция вывода обычного множества =======*/
        public static void PrintOutSingle(List<double> InputList)
        {
            Console.Write("{ ");
            InputList.ForEach(delegate(double Num)
            {
                Console.Write(Num);
                Console.Write("  ");
            });
            Console.Write("}");
        }
        /*================================================================*/
        /*=============== FuzzySupport() - возвращает носитель введенного нечеткого множества ==================*/
        public static List<KeyValuePair<double, double>> FuzzySupport(List<KeyValuePair<double, double>> InputList)
        {
            List<KeyValuePair<double, double>> Returned_Support = new List<KeyValuePair<double, double>>();
            InputList.ForEach(delegate(KeyValuePair<double, double> Pair)
            {
                if (Pair.Value > 0)
                {
                    Returned_Support.Add((Pair));
                }
            });
            return Returned_Support;
        }
        /*=============================================================================*/
        /*==================== FuzzyCore() - возвращает ядро введенного нечеткого множества ===========*/
        public static List<KeyValuePair<double, double>> FuzzyCore(List<KeyValuePair<double, double>> InputList)
        {
            List<KeyValuePair<double, double>> Returned_Core = new List<KeyValuePair<double, double>>();
            InputList.ForEach(delegate(KeyValuePair<double, double> Pair)
            {
                if (Pair.Value == 1)
                {
                    Returned_Core.Add(Pair);
                }
            });
            return Returned_Core;
        }
        /*==============================================================================================*/
        /*============ ChangePoint() - возвращает точку перехода нечеткого множества =============*/
        public static List<KeyValuePair<double, double>> ChangePoint(List<KeyValuePair<double, double>> InputList)
        {
            List<KeyValuePair<double, double>> Returned_Point = new List<KeyValuePair<double, double>>();
            int count = 0;
            InputList.ForEach(delegate(KeyValuePair<double, double> Pair)
            {
                if (Pair.Value == 0.5)
                {
                    count++;
                    Returned_Point.Add(Pair);
                }
            });
            if (count == 0)
            {
                Console.Write("Точки перехода нет");
                //  return null;
                return Returned_Point;
            }
            else
            {
                //   Console.Write("Точки перехода: ");
                return Returned_Point;
            }
        }
        /*==============================================================================================*/
        /*============= FuzzySuprem() - возвращает высоту нечеткого множества ============================================================*/
        public static double FuzzySuprem(List<KeyValuePair<double, double>> InputList)
        {
            List<KeyValuePair<double, double>> newlist = new List<KeyValuePair<double, double>>(InputList);
            //newlist = InputList;
            newlist.Sort(delegate(KeyValuePair<double, double> x, KeyValuePair<double, double> y) { return y.Value.CompareTo(x.Value); });
            double Sup = newlist[0].Value;
            return Sup;
        }
        /*================================================================================================================================*/
        /*============================== Normalize - возвращает нормализованное множество ==============================*/
        public static List<KeyValuePair<double, double>> Normalize(List<KeyValuePair<double, double>> InputList)
        {
            List<KeyValuePair<double, double>> Returned_Normal = new List<KeyValuePair<double, double>>();
            double Max = FuzzySuprem(InputList);
            if (Max == 0 || Max == 1)
            {
                Console.WriteLine("Высота равна 1 или 0 - нормализовать не имеет смысла. Возвращаю исходное множество...");
                return InputList;
            }
            InputList.ForEach(delegate(KeyValuePair<double, double> Pair)
            {
                Returned_Normal.Add(new KeyValuePair<double, double>(Pair.Key, Math.Round(Pair.Value / Max, 2)));
            });

            return Returned_Normal;
        }
        /*===============================================================================================================*/
        /*================== Entropy - возвращает значение энтропии нечеткого множества ================*/
        public static double Entropy(List<KeyValuePair<double, double>> InputList)
        {
            List<KeyValuePair<double, double>> Returned_Normal = new List<KeyValuePair<double, double>>();
            double EntropyNum = 0;
            double Sum = InputList.Sum(Pair => Pair.Value);
            InputList.ForEach(delegate(KeyValuePair<double, double> Pair)
            {
                Returned_Normal.Add(new KeyValuePair<double, double>(Pair.Key, Pair.Value / Sum));
            });
            for (int i = 0; i < Returned_Normal.Count; i++)
            {
                if (Returned_Normal[i].Value != 0)
                {
                    EntropyNum += (Returned_Normal[i].Value * Math.Log(Returned_Normal[i].Value, 2));
                }
            }
            EntropyNum =Math.Round(Math.Abs(EntropyNum),3);
            return EntropyNum;
        }
        /*================================================================================================*/
        /*=============== DistLin - возвращает расстояние Хэмминга между нечеткими множествами ================*/
        public static double DistLin(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            double dist = new double();
            for (int i = 0; i < list1.Count; i++)
            {
                dist += Math.Abs(list1[i].Value - list2[i].Value);
            }
            return dist;
        }
        /*======================================================================================================*/
        /*=============== DistEucl - возвращает Евклидовое расстояние между нечеткими множествами ==============*/
        public static double DistEucl(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            double dist = new double();
            for (int i = 0; i < list1.Count; i++)
            {
                dist += Math.Pow(Math.Abs(list1[i].Value - list2[i].Value), 2);
            }
            dist = Math.Round(Math.Pow(dist, 0.5), 2);
            return dist;
        }
        /*======================================================================================================*/
        /*======= StrictSet - возвращает ближайшее четкое множество ==========*/
        public static List<KeyValuePair<double, double>> StrictSet(List<KeyValuePair<double, double>> list1)
        {
            List<KeyValuePair<double, double>> Set = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Value >= 0.5f)
                {
                    Set.Add(new KeyValuePair<double, double>(list1[i].Key, 1.0));
                }
            }
            return Set;
        }
        /*=====================================================================*/
        /*========= ConcaveFuz - возвращает true  если вогнутость присутствует ====================================================================================================*/
        public static bool ConcaveFuz(List<KeyValuePair<double, double>> listt)
        {
            List<KeyValuePair<double, double>> list1 = new List<KeyValuePair<double, double>>(listt);
            list1.Sort(delegate(KeyValuePair<double, double> x, KeyValuePair<double, double> y) { return x.Key.CompareTo(y.Key); });
            bool Conc = false;
            for (int i = 0; i < list1.Count - 2; i++)
            {
                double Max;
                if (list1[i].Value > list1[i + 2].Value)
                {
                    Max = list1[i].Value;
                }
                else
                {
                    Max = list1[i + 2].Value;
                }
                double lyambda = Math.Abs(list1[i + 1].Key - list1[i + 2].Key) / Math.Abs(list1[i].Key - list1[i + 2].Key);
                if ((list1[i + 1].Value <= lyambda * list1[i].Value + (1 - lyambda) * list1[i + 2].Value) && lyambda * list1[i].Value + (1 - lyambda) * list1[i + 2].Value <= Max)
                {
                    Conc = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Не вогнутое");
                    Conc = false;
                    break;
                }
            }
            return Conc;
        }
        /*=======================================================================================================================================================================*/
        /*======= ConvexFuz - возвращает true  если выпуклость присутствует =====================================================================================================*/
        public static bool СonvexFuz(List<KeyValuePair<double, double>> listt)
        {
            List<KeyValuePair<double, double>> list1 = new List<KeyValuePair<double, double>>(listt);
            list1.Sort(delegate(KeyValuePair<double, double> x, KeyValuePair<double, double> y) { return x.Key.CompareTo(y.Key); });
            bool Conv = false;
            //List<KeyValuePair<double, double>> Set = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1.Count - 2; i++)
            {
                double Min;
                if (list1[i].Value < list1[i + 2].Value)
                {
                    Min = list1[i].Value;
                }
                else
                {
                    Min = list1[i + 2].Value;
                }
                double lyambda = Math.Abs(list1[i + 1].Key - list1[i + 2].Key) / Math.Abs(list1[i].Key - list1[i + 2].Key);
                if ((list1[i + 1].Value >= lyambda * list1[i].Value + (1 - lyambda) * list1[i + 2].Value) && lyambda * list1[i].Value + (1 - lyambda) * list1[i + 2].Value >= Min)
                {
                    Conv = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Не выпуклое");
                    Conv = false;
                    break;
                }
            }
            return Conv;
        }
        /*=======================================================================================================================================================================*/
        /*============= EqualFuz - возвращает true если нечеткие множества равны, false - если наооборот ==============*/
        public static bool EqualFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2) 
        {
            return list1.SequenceEqual(list2);
        }
        /*=== EqualFuz - возвращает true если нечеткое множество в первом аргументе является подмножеством нечеткого множества во втором аргументе ====*/
        public static bool IsSubFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            for (int i = 0; i < list2.Count; i++) {
                if (list1[i].Value <= list2[i].Value)
                {
                    continue;
                }
                else return false;
            }
            return true;
        }
        /*================================================= ДАЛЕЕ ИДУТ МАКСИМИННЫЕ ОПРЕДЕЛЕНИЯ ОПЕРАЦИЙ ===============================================*/
        /*================== AddonFuz возвращает дополнение нечеткого множества ========================*/
        public static List<KeyValuePair<double,double>> AddonFuz(List<KeyValuePair<double, double>> list1)
        {
            List<KeyValuePair<double, double>> adSet = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1.Count; i++) 
            {
                adSet.Add(new KeyValuePair<double,double> (list1[i].Key,1-list1[i].Value));
            }
            return adSet;
        }
        /*======================================== UniteFuz возвращает объединение двух нечетких множеств =============================================*/
        public static List<KeyValuePair<double, double>> UniteFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            if (diference!=0) 
            {
                if (diference < 0) {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));  
                    }
                }
                else if(diference>0){
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));  
                    }
                }
            }
            List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1Copy.Count; i++)
            { 
                    UniSet.Add(new KeyValuePair<double, double>(list1Copy[i].Key,Math.Max(list1Copy[i].Value,list2Copy[i].Value)));
            }
            return UniSet;
        }
        /*======================================== InterFuz возвращает пересечение двух нечетких множеств =============================================*/
        public static List<KeyValuePair<double, double>> InterFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            List<KeyValuePair<double, double>> InterSet = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1Copy.Count; i++)
            {
               InterSet.Add(new KeyValuePair<double, double>(list2Copy[i].Key, Math.Min(list1Copy[i].Value,list2Copy[i].Value)));
            }
            return InterSet;
        }
        /*========= SubsFuz возвращает нечеткое множество, являющееся разностью двух нечетких множеств(реализация через формулу минимума)=========*/
        public static List<KeyValuePair<double, double>> SubsFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            // ---------------------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> SubsSet = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1Copy.Count; i++)
            { 
                 SubsSet.Add(new KeyValuePair<double, double>(list2Copy[i].Key,Math.Min(list1Copy[i].Value,1-list2Copy[i].Value)));
            }
            return SubsSet;
        }
        /*=========SubsFuz2 возвращает нечеткое множество, являющееся разностью двух нечетких множеств (реализация через пересечение)==============*/
        public static List<KeyValuePair<double, double>> SubsFuz2(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            // ---------------------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> SubsSet = new List<KeyValuePair<double, double>>();
            SubsSet=InterFuz(list1Copy,AddonFuz(list2Copy));      
            return SubsSet;
        }
        //======================= SubsSyncFuz -возвращает нечеткое множество, являющееся симметричной разностью =======================================
        //======================= двух нечетких множеств (реализация через объединение разностей) ======================================================
        public static List<KeyValuePair<double, double>> SubsSyncFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            // ---------------------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> ResSet = new List<KeyValuePair<double, double>>();
            ResSet=(UniteFuz(SubsFuz(list1Copy,list2Copy),SubsFuz(list2Copy,list1Copy)));
            return ResSet;
        }
        //===================== SubsSyncFuz2 -возвращает нечеткое множество, являющееся симметричной разностью двух ====================================
        //===================== нечетких множеств (реализация через разность объединения и пересечения) ================================================
        public static List<KeyValuePair<double, double>> SubsSyncFuz2(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            // ---------------------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> ResSet = new List<KeyValuePair<double, double>>();
            ResSet = (SubsFuz(UniteFuz(list1Copy, list2Copy), InterFuz(list1Copy, list2Copy)));
            return ResSet;
        }
        /*==============================================================================================================================================*/
        /*============================================= ДАЛЕЕ ИДУТ АЛГЕБРАИЧЕСКИЕ ОПЕРАЦИИ ===============================================*/
        /* ====================================== AlUniteFuz Алгебраическое объединение =============================================================*/
        public static List<KeyValuePair<double, double>> ALUniteFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1Copy.Count; i++)
            {
                UniSet.Add(new KeyValuePair<double,double>( list1Copy[i].Key,(list1Copy[i].Value+list2Copy[i].Value)-(list1Copy[i].Value*list2Copy[i].Value)));
            }
            return FuzzySupport(UniSet);
        }
        /* ============================================================================================================================================*/
        /* =========================================== AlInterFuz Алгебраическое пересечение ==========================================================*/
        public static List<KeyValuePair<double, double>> ALInterFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
        {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            List<KeyValuePair<double, double>> InterSet = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < list1Copy.Count; i++)
            {
                InterSet.Add(new KeyValuePair<double, double>(list1Copy[i].Key, list1Copy[i].Value * list2Copy[i].Value));
            }
            return FuzzySupport(InterSet);
        }
        /* ============================================================================================================================================*/
          /*========= ASubsFuz возвращает нечеткое множество, являющееся алгебраической разностью двух нечетких множеств(реализация через пересечение)=========*/
         public static List<KeyValuePair<double, double>> ALSubsFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
          {
            List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
            List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
            int diference = list1Copy.Count - list2Copy.Count;
            // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
            if (diference != 0)
            {
                if (diference < 0)
                {
                    for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                    {
                        list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                    }
                }
                else if (diference > 0)
                {
                    for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                    {
                        list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                    }
                }
            }
            // ---------------------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> SubsSet = new List<KeyValuePair<double, double>>();
          //  for (int i = 0; i < list1Copy.Count; i++)
          //  {
                SubsSet = ALInterFuz(list1Copy,AddonFuz(list2Copy));     
           // }
            return FuzzySupport(SubsSet);
        }
         /*========= ASubsFuz возвращает нечеткое множество, являющееся алгебраической разностью двух нечетких множеств(реализация через умножение)=========*/
         public static List<KeyValuePair<double, double>> ALSubsFuz2(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             // ---------------------------------------------------------------------------------------------
             List<KeyValuePair<double, double>> SubsSet = new List<KeyValuePair<double, double>>();
             for (int i = 0; i < list1Copy.Count; i++)
             {
                 SubsSet.Add(new KeyValuePair<double,double> (list1Copy[i].Key,list1Copy[i].Value*(1-list2Copy[i].Value)));
             }
             return FuzzySupport(SubsSet);
         }
         //======================= AlSubsSyncFuz -возвращает нечеткое множество, являющееся алгебраической симметричной  =================================
         //======================= разностью двух нечетких множеств (реализация через объединение разностей) =============================================
         public static List<KeyValuePair<double, double>> AlSubsSyncFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             // ---------------------------------------------------------------------------------------------
             List<KeyValuePair<double, double>> ResSet = new List<KeyValuePair<double, double>>();
             ResSet = ALUniteFuz((ALSubsFuz(list1Copy, list2Copy)), ALSubsFuz(list2Copy, list1Copy));
             return FuzzySupport(ResSet);
         }
         //======================= AlSubsSyncFuz2 -возвращает нечеткое множество, являющееся алгебраической симметричной  =================================
         //======================= разностью двух нечетких множеств (реализация через разность объединения и пересечения) =================================
         public static List<KeyValuePair<double, double>> AlSubsSyncFuz2(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             // Проверка на равенство количества элементов и доплнение элементами с нулевой принадлежностью
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             // ---------------------------------------------------------------------------------------------
             List<KeyValuePair<double, double>> ResSet = new List<KeyValuePair<double, double>>();
             ResSet = ALSubsFuz((ALUniteFuz(list1Copy, list2Copy)), ALInterFuz(list1Copy, list2Copy));
             return FuzzySupport(ResSet);
         }
         /*==============================================================================================================================================*/
         /*============================================= ДАЛЕЕ ИДУТ ОГРАНИЧЕННЫЕ ОПЕРАЦИИ ===============================================*/
         /*======================================== UniteFuz возвращает ограниченное объединение двух нечетких множеств =============================================*/
         public static List<KeyValuePair<double, double>>  OUniteFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
             for (int i = 0; i < list1Copy.Count; i++)
             {
                 UniSet.Add(new KeyValuePair<double,double>(list1Copy[i].Key,Math.Min(1.0,list1Copy[i].Value+list2Copy[i].Value)));     
             }
             return FuzzySupport(UniSet);
         }
        /*===================================== OInterFuz возвращает ограниченное пересечение двух нечетких множеств ================================*/
         public static List<KeyValuePair<double, double>> OInterFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
             for (int i = 0; i < list1Copy.Count; i++)
             {
                 UniSet.Add(new KeyValuePair<double, double>(list1Copy[i].Key, Math.Max(0.0, (list1Copy[i].Value + list2Copy[i].Value-1))));
             }
             return UniSet;
         }

         /*===================================== OSubsFuz возвращает ограниченную разность двух нечетких множеств ================================*/
         public static List<KeyValuePair<double, double>> OSubsFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0));
                     }
                 }
             }
             
             List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
             UniSet = OInterFuz(list1, AddonFuz(list2));
             return FuzzySupport(UniSet);
         }
         /*===================================== OSubsFuz2 возвращает ограниченную разность двух нечетких множеств ================================*/
         public static List<KeyValuePair<double, double>> OSubsFuz2(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
             for (int i = 0; i < list1Copy.Count; i++)
             {
                 UniSet.Add(new KeyValuePair<double, double>(list1Copy[i].Key, Math.Max(0, list1Copy[i].Value - list2Copy[i].Value)));
             }
             return FuzzySupport(UniSet);
         }
         /*===================================== OSyncSubsFuz возвращает ограниченное  ограниченную симметричную разность двух нечетких множеств ================================*/
         public static List<KeyValuePair<double, double>> OSyncSubsFuz(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
             return FuzzySupport(OInterFuz(OSubsFuz(list1Copy, list2Copy), OSubsFuz(list2Copy, list1Copy)));
         }
         /*===================================== OSyncSubsFuz2 возвращает ограниченное  ограниченную симметричную разность двух нечетких множеств ================================*/
         public static List<KeyValuePair<double, double>> OSyncSubsFuz2(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> list2Copy = new List<KeyValuePair<double, double>>(list2);
             int diference = list1Copy.Count - list2Copy.Count;
             if (diference != 0)
             {
                 if (diference < 0)
                 {
                     for (int i = list1Copy.Count; i < list2Copy.Count; i++)
                     {
                         list1Copy.Add(new KeyValuePair<double, double>(list2Copy[i].Key, 0.0));
                     }
                 }
                 else if (diference > 0)
                 {
                     for (int i = list2Copy.Count; i < list1Copy.Count; i++)
                     {
                         list2Copy.Add(new KeyValuePair<double, double>(list1Copy[i].Key, 0.0));
                     }
                 }
             }
             List<KeyValuePair<double, double>> UniSet = new List<KeyValuePair<double, double>>();
             return FuzzySupport(OSubsFuz(OUniteFuz(list1Copy,list2Copy),OInterFuz(list1Copy, list2Copy)));
         }
         /*================================ DifuzCoG метод центра тяжести ==============================*/
         public static double DifuzCoG(List<KeyValuePair<double, double>> list1) 
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             double SumLeft=0;
             double SumRight=0;
             for (int i = 0; i < list1Copy.Count; i++) {
                 SumLeft += list1Copy[i].Value * list1Copy[i].Key;
                 SumRight += list1Copy[i].Value;
             }
             return Math.Round(SumLeft / SumRight,4);
         }
         /*============================== DifuzCoG метод центра площади ================================*/
         public static double DifuzCoA(List<KeyValuePair<double, double>> list1)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             double SumLeft = list1Copy[0].Value;
             double SumRight = list1Copy[list1Copy.Count - 1].Value;
             int leftLastIndex = 0, rightLastIndex = list1Copy.Count - 1;
             while (leftLastIndex != rightLastIndex - 1)
             {
                 if (SumLeft < SumRight)
                 {
                     leftLastIndex++;
                     SumLeft += list1Copy[leftLastIndex].Value;
                 }
                 else
                 {
                     rightLastIndex--;
                     SumRight += list1Copy[rightLastIndex].Value;
                 }
             }
             if (SumLeft >= SumRight)
             {
                 return list1Copy[leftLastIndex].Key;
             }
             else {
                 return list1Copy[rightLastIndex].Key;
             }
         }
         //======= DifuzMM метод модального значения (аргумент Mode может принимать значения "left" или =================================// 
         //======= "right" или "mid" для вычисления левой, правой и центральной моды соответственно) =========================================//
         public static double DifuzModa(List<KeyValuePair<double, double>> list1, string Mode)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             List<KeyValuePair<double, double>> newlist = new List<KeyValuePair<double, double>>(list1Copy);
             //newlist = InputList;
             newlist.Sort(delegate(KeyValuePair<double, double> x, KeyValuePair<double, double> y) { return y.Value.CompareTo(x.Value); });
             double Max = newlist[0].Value;
             double sum=0;
             newlist.RemoveAll(x => x.Value < newlist[0].Value);
             newlist.Sort((x,y)=>x.Key.CompareTo(y.Key));  
           //  newlist.ForEach(x => Console.WriteLine(x));
             switch (Mode) 
             {
                 case "left": {
                     return newlist[0].Key;
                 }
                 case "right":{
                     return newlist[newlist.Count - 1].Key;
                 }
                 case "mid":
                 {
                     newlist.ForEach(x=>sum+=x.Key);
                     return sum / newlist.Count;
                 }
                 default: {
                     return newlist[0].Key;
                 }
             }
         }
         /* ======================================== Concentrate - концентрация ==========================================*/
         public static List<KeyValuePair<double, double>> Concentrate(List<KeyValuePair<double, double>> list1)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>();
             list1.ForEach(x=>list1Copy.Add(new KeyValuePair<double,double>(x.Key,Math.Round(Math.Pow(x.Value, 0.5f), 2))));
             return list1Copy;
         }
         /*===============================================================================================================*/
         /* ======================================= Dilation - растяжение ==================================================*/
         public static List<KeyValuePair<double, double>> Dilation(List<KeyValuePair<double, double>> list1)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>();
             list1.ForEach(x => list1Copy.Add(new KeyValuePair<double, double>(x.Key, Math.Round(Math.Pow(x.Value, 2f), 2))));
             return list1Copy;
         }
         /*======================================== AlfaCut -альфа разрез ============================================*/
         public static List<KeyValuePair<double, double>> AlfaCut(List<KeyValuePair<double, double>> list1,double alfa)
         {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>();
             for(int i=0;i<list1.Count;i++){
             if(list1[i].Value>=alfa) list1Copy.Add(list1[i]);
             }
             return list1Copy;
         }
         /*============================================================================================================*/
        /* =========================== DecompFuz - декомпозиция ========================================*/
         public static void Decomp(List<KeyValuePair<double,double>> list1) {
             List<KeyValuePair<double, double>> list1Copy = new List<KeyValuePair<double, double>>(list1);
             list1Copy.Sort((x,y)=>x.Value.CompareTo(y.Value));
             List<KeyValuePair<double,double>>[] DecompList=new List<KeyValuePair<double,double>>[list1.Count];
             for(int i=0;i<list1.Count;i++){
             DecompList[i]=new List<KeyValuePair<double,double>>();
             }
             for (int i = 0; i < list1Copy.Count; i++)
             {
                 double minValue = list1Copy[i].Value;
                 for (int j = 0; j < list1Copy.Count; j++)
                 {
                     if (list1Copy[j].Value >= minValue)
                         DecompList[i].Add(new KeyValuePair<double, double>(list1Copy[j].Key,minValue));
                 }
                 DecompList[i].Sort((x, y) => x.Key.CompareTo(y.Key));
             }
             for (int i = 0; i < DecompList.Length; i++) {
                 DecompList[i].ForEach(x => Console.Write(x));
                 Console.WriteLine();
             }
         }
         /*======================= CompareLists - Более точный метод сравнения листов пар ==================================*/
         public static bool CompareLists(List<KeyValuePair<double, double>> list1, List<KeyValuePair<double, double>> list2) 
          {
              for (int i=0 ; i < list1.Count; i++) {
                  if (list1[i].Key == list2[i].Key)
                  {
                      continue;
                  }
                  else {
                      return false;
                  }
              }
              return true;  
          }
         /*=========================================================================================================================================================================*/
        public static void Testing()
        {
            bool Supp = false;
            bool Core = false;
            bool PointC = false;
            bool Suprem = false;
            bool Normal = false;
            bool EntropiaBool = false;
            bool DistLinear = false;
            bool DistEucle = false;
            bool Strict = false;
            bool IsConvex = false;
            bool IsConcave = false;
            bool IsEqual = false;
            bool IsSub = false;
            bool Addon = false;
            bool Unite = false;
            bool Intersection = false;
            bool Subset = false;
            bool SyncSubset = false;
            bool AUnite = false;
            bool AInter = false;
            bool ALSubs = false;
            bool ALSyncSubs = false;
            bool OUnite = false;
            bool OInter = false;
            bool OSubs = false;
            bool OSyncSubs = false;
            bool DifuzzCOG = false;
            bool DifuzzCOA = false;
            bool DifuzzSOM = false;
            bool DifuzzLOM = false;
            bool DifuzzMOM = false;
            bool ConcentrateBool = false;
            bool DilationBool = false;
            bool AlfaCutBool = false;
            bool BinaryBoolAd = false;
            bool BinaryBoolSub = false;
            bool BinaryBoolMult = false;
            bool BinaryBoolDiv = false;
            /*========================= Тестовые входные данные ===============================*/
            List<KeyValuePair<double, double>> Test1 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> Test2 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> Test3 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1)
            };
             List<KeyValuePair<double, double>> Test4 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6),
              new KeyValuePair<double,double>(5,0.3),
              new KeyValuePair<double,double>(6,0.1)
            };
             List<KeyValuePair<double, double>> Test5 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.7),
              new KeyValuePair<double,double>(2,0.9),
              new KeyValuePair<double,double>(3,0),
              new KeyValuePair<double,double>(4,0.6),
              new KeyValuePair<double,double>(5,0.5),
              new KeyValuePair<double,double>(6,1)
            };
             List<KeyValuePair<double, double>> Testnew1 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(112,0.51),
              new KeyValuePair<double,double>(230,1.0),
              new KeyValuePair<double,double>(211,0.0),
              new KeyValuePair<double,double>(202,0.59),
              new KeyValuePair<double,double>(186,0.73),
              new KeyValuePair<double,double>(21,0.91)
            };
             List<KeyValuePair<double, double>> Testnew2 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(63,0.95),
              new KeyValuePair<double,double>(240,0.35),
              new KeyValuePair<double,double>(47,0.13),
              new KeyValuePair<double,double>(22,0.22),
              new KeyValuePair<double,double>(88,0.0),
              new KeyValuePair<double,double>(61,1)
            };

            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> TestConvex = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.25),
              new KeyValuePair<double,double>(2,0.375),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.4),
              new KeyValuePair<double,double>(5,0.3)
            };
            List<KeyValuePair<double, double>> TestConcave = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.6),
              new KeyValuePair<double,double>(2,0.5),
              new KeyValuePair<double,double>(3,0.55),
              new KeyValuePair<double,double>(4,0.7)
            };
            List<KeyValuePair<double, double>> TestDefuz = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.1),
              new KeyValuePair<double,double>(2,0.25),
              new KeyValuePair<double,double>(3,0.35),
              new KeyValuePair<double,double>(4,0.48),
              new KeyValuePair<double,double>(5,0.8),
              new KeyValuePair<double,double>(6,0.61),
              new KeyValuePair<double,double>(7,0.24),
              new KeyValuePair<double,double>(8,0.21),
              new KeyValuePair<double,double>(9,0.01)
            };
            List<KeyValuePair<double, double>> TestDefuz1 = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.1),
              new KeyValuePair<double,double>(2,0.5),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.5),
              new KeyValuePair<double,double>(5,0.3)
            };
            List<KeyValuePair<double, double>> TestDeComp = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.8),
              new KeyValuePair<double,double>(2,0.6),
              new KeyValuePair<double,double>(3,0.3),
              new KeyValuePair<double,double>(4,0.9),
              new KeyValuePair<double,double>(5,1.0)
            };
            List<double> TestLR_Set1 = new List<double>()
            {
             1,3,4
            };
            List<double> TestLR_Set2 = new List<double>()
            {
             2,5,8
            };
            List<double> TestLR_Set3 = new List<double>()
            {
             1,5,6,10
            };
            List<double> TestLR_Set4 = new List<double>()
            {
             9,10,11,12
            };
            /*==================================================================================*/
            /*============================= Истинные результаты ================================*/
            List<KeyValuePair<double, double>> UnswerSup = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
            //-----------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerCore = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(2,1.0),
              new KeyValuePair<double,double>(4,1.0)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerPoint = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,0.5),
            };
            //---------------------------------------------------------------------------------
            double UnswerSupr = 0.6;
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerNormal = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.67),
              new KeyValuePair<double,double>(2,0.0),
              new KeyValuePair<double,double>(3,0.83),
              new KeyValuePair<double,double>(4,1.0)
            };
            //---------------------------------------------------------------------------------
            double Entropia = 0.881;
            //---------------------------------------------------------------------------------
            double UnswerDistLinear = 1.40;
            //---------------------------------------------------------------------------------
            double UnswerDistEucl = 1.08;
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerStrict = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,1.0),
              new KeyValuePair<double,double>(4,1.0)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerAddon = new List<KeyValuePair<double, double>>()
            {
                new KeyValuePair<double,double>(1,0.6),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerUnite = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,1),
              new KeyValuePair<double,double>(5,0.3),
              new KeyValuePair<double,double>(6,0.1)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerInterSec= new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6),
              new KeyValuePair<double,double>(5,0.0),
               new KeyValuePair<double,double>(6,0.0)
            };
            //---------------------------------------------------------------------------------
            List<KeyValuePair<double, double>> UnswerSubstract = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.4)
            };
            List<KeyValuePair<double, double>> UnswerAUnite = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.64),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.75),
              new KeyValuePair<double,double>(4,1)
            };
            List<KeyValuePair<double, double>> UnswerALInter = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.16),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.25),
              new KeyValuePair<double,double>(4,0.6)
            };
            List<KeyValuePair<double, double>> UnswerOUnite = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.8),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,1),
              new KeyValuePair<double,double>(4,1)
            };
            List<KeyValuePair<double, double>> UnswerOIntersec = new List<KeyValuePair<double, double>>(){
              new KeyValuePair<double,double>(1,0),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0),
              new KeyValuePair<double,double>(4,0.6)
            };
            double UnswerDefuzCoG = 4.8131;
            double UnswerDefuzCoA = 3;
            double UnswerDefuzLoM = 4;
            double UnswerDefuzMoM = 3;
            double UnswerDefuzSoM = 2;
            List<KeyValuePair<double, double>> UnswerConcentrate = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.63),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.71),
              new KeyValuePair<double,double>(4,0.77)
            };
             List<KeyValuePair<double, double>> UnswerDilation = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.16),
              new KeyValuePair<double,double>(2,0),
              new KeyValuePair<double,double>(3,0.25),
              new KeyValuePair<double,double>(4,0.36)
            };
             List<KeyValuePair<double, double>> UnswerAlfaCut = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6)
            };
             List<KeyValuePair<double, double>> UnswerBinaryAdd = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(2,0.4),
              new KeyValuePair<double,double>(3,0.4),
              new KeyValuePair<double,double>(4,0.5),
              new KeyValuePair<double,double>(5,1),
              new KeyValuePair<double,double>(6,1),
              new KeyValuePair<double,double>(7,0.6),
              new KeyValuePair<double,double>(8,1)
            };
             List<KeyValuePair<double, double>> UnswerBinarySubs = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(-3,1),
              new KeyValuePair<double,double>(-2,1),
              new KeyValuePair<double,double>(-1,1),
              new KeyValuePair<double,double>(0,0.5),
              new KeyValuePair<double,double>(1,0.6),
              new KeyValuePair<double,double>(2,0.5),
              new KeyValuePair<double,double>(3,0.6)
            };
             List<KeyValuePair<double, double>> UnswerBinaryMult = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(2,0.4),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6),
              new KeyValuePair<double,double>(6,1),
              new KeyValuePair<double,double>(8,1),
              new KeyValuePair<double,double>(9,0.5),
              new KeyValuePair<double,double>(12,0.6),
              new KeyValuePair<double,double>(16,1),
            };
             List<KeyValuePair<double, double>> UnswerBinaryDiv = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(0.25,1),
              new KeyValuePair<double,double>(0.33,0.5),
              new KeyValuePair<double,double>(0.5,1),
              new KeyValuePair<double,double>(0.67,0.5),
              new KeyValuePair<double,double>(0.75,1),
              new KeyValuePair<double,double>(1,0.5),
              new KeyValuePair<double,double>(1.33,0.6),
              new KeyValuePair<double,double>(1.5,1),
              new KeyValuePair<double,double>(2,1),
              new KeyValuePair<double,double>(3,0.5),
              new KeyValuePair<double,double>(4,0.6),
            };
             List<KeyValuePair<double, double>> UnswerOpposite = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(-1,0.4),
              new KeyValuePair<double,double>(-2,0),
              new KeyValuePair<double,double>(-3,0.5),
              new KeyValuePair<double,double>(-4,0.6)
            };
             List<KeyValuePair<double, double>> UnswerReverse = new List<KeyValuePair<double, double>>()
            {
              new KeyValuePair<double,double>(1,0.4),
              new KeyValuePair<double,double>(0.5,0),
              new KeyValuePair<double,double>(0.33,0.5),
              new KeyValuePair<double,double>(0.25,0.6)
            };
             List<double> UnswerBinLRAd = new List<double>()
            {
            3,8,12
            };
             List<double> UnswerBinLRAdT = new List<double>()
            {
            10,15,17,22
            };
             List<double> UnswerBinLRSub = new List<double>()
            {
            -1,11,9
            };
             List<double> UnswerBinLRSubT = new List<double>()
            {
            -8,-5,18,21
            };
            //---------------------------------------------------------------------------------
            /*==================================================================================*/
            Console.WriteLine();
            Console.WriteLine("Проход по заготовленным множествам");
            Console.WriteLine();
            Console.Write("1.  Носитель ----------------------------------------");
            Supp = FuzzySupport(Test1).SequenceEqual(UnswerSup);
            Console.WriteLine(Supp);
            //------------------------------------------------------   
            Console.Write("2.  Ядро --------------------------------------------");
            Core = FuzzyCore(Test2).SequenceEqual(UnswerCore);
            Console.WriteLine(Core);
            //------------------------------------------------------
            Console.Write("3.  Точка перехода ----------------------------------");
            PointC = ChangePoint(Test1).SequenceEqual(UnswerPoint);
            Console.WriteLine(PointC);
            //------------------------------------------------------
            Console.Write("4.  Высота ------------------------------------------");
            if (FuzzySuprem(Test1) == UnswerSupr)
                Suprem = true;
            else
                Suprem = false;
            Console.WriteLine(Suprem);
            //------------------------------------------------------
            Console.Write("5.  Энтропия ----------------------------------------");
            if (Entropia == Entropy(Test5)) 
                EntropiaBool = true;
            else EntropiaBool = false;
            Console.WriteLine(EntropiaBool);
            //------------------------------------------------------
            Console.Write("6.  Нормализация ------------------------------------");
            Normal = Normalize(Test1).SequenceEqual(UnswerNormal);
            Console.WriteLine(Normal);
            //------------------------------------------------------
            Console.Write("7.  Линейное расстояние -----------------------------");
            if (DistLin(Test1, Test2) == UnswerDistLinear)
                DistLinear = true;
            else DistLinear = false;
            Console.WriteLine(DistLinear);
            //------------------------------------------------------
            Console.Write("8.  Евклидо расстояние ------------------------------");
            if (DistEucl(Test1, Test2) == UnswerDistEucl)
                DistEucle = true;
            else DistEucle = false;
            Console.WriteLine(DistEucle);    
            //------------------------------------------------------
            Console.Write("9.  Ближайшее четкое --------------------------------");
            Strict = StrictSet(Test1).SequenceEqual(UnswerStrict);
            Console.WriteLine(Strict);
            //------------------------------------------------------
            Console.Write("10. Проверка на выпуклость---------------------------");
            IsConvex = СonvexFuz(TestConvex);
            Console.WriteLine(IsConvex);
            //------------------------------------------------------
            Console.Write("11. Проверка на вогнутость---------------------------");
            IsConcave = ConcaveFuz(TestConcave);
            Console.WriteLine(IsConcave);
            //------------------------------------------------------
            Console.Write("12. Проверка на равенство----------------------------");
            IsEqual = EqualFuz(Test3, Test2);
            Console.WriteLine(IsEqual);
            //------------------------------------------------------
            Console.Write("13. Проверка на подмножество ------------------------");
            IsSub = IsSubFuz(Test3, Test3);
            Console.WriteLine(IsSub);
            //------------------------------------------------------
            Console.Write("14. Операция дополнения -----------------------------");
            Addon = AddonFuz(Test2).SequenceEqual(UnswerAddon);
            Console.WriteLine(Addon);
            //------------------------------------------------------
            Console.Write("15. Максиминная операция объединения ----------------");
            Unite = UniteFuz(Test4, Test2).SequenceEqual(UnswerUnite);
            Console.WriteLine(Unite);
            //------------------------------------------------------
            Console.Write("16. Максиминная операция пересечения ----------------");
            Intersection = InterFuz(Test4, Test2).SequenceEqual(UnswerInterSec);
            Console.WriteLine(Intersection);
            //------------------------------------------------------
            Console.Write("17. Максиминная операция вычитания ------------------");
            Subset = SubsFuz(Test2, Test1).SequenceEqual(SubsFuz2(Test2, Test1));
            Console.WriteLine(Subset);
            //------------------------------------------------------
            Console.Write("18. Максиминная операция симметричного вычитания ----");
            SyncSubset = SubsSyncFuz(Test2, Test1).SequenceEqual(SubsSyncFuz2(Test2,Test1));
            Console.WriteLine(SyncSubset);
            //------------------------------------------------------
            Console.Write("19. Алгебраическая операция объединения -------------");
            AUnite = ALUniteFuz(Test1, Test2).SequenceEqual(UnswerAUnite);
            Console.WriteLine(AUnite);
            //------------------------------------------------------
            Console.Write("20. Алгебраическая операция пересечения -------------");
            AInter = ALInterFuz(Test1, Test2).SequenceEqual(UnswerALInter);
            Console.WriteLine(AUnite);
            //------------------------------------------------------
            Console.Write("21. Алгебраическая операция вычитания ---------------");
            ALSubs = ALSubsFuz(Test1, Test2).SequenceEqual(ALSubsFuz2(Test1,Test2));
            Console.WriteLine(ALSubs);
            //------------------------------------------------------
            Console.Write("22. Алгебраическая операция симметричного вычитания -");
            ALSyncSubs = CompareLists(AlSubsSyncFuz(Test1, Test2),AlSubsSyncFuz2(Test1,Test2));
            Console.WriteLine(ALSyncSubs);
            //------------------------------------------------------
            Console.Write("23. Ограниченная операция объединения ---------------");
            OUnite = OUniteFuz(Test1, Test2).SequenceEqual(UnswerOUnite);
            Console.WriteLine(OUnite);
            //------------------------------------------------------
            Console.Write("24. Ограниченная операция пересечения ---------------");
            OInter = CompareLists(OInterFuz(Test1, Test2),UnswerOIntersec);
            Console.WriteLine(OInter);
            //------------------------------------------------------
            Console.Write("25. Ограниченная операция вычитания -----------------");
            OSubs = CompareLists(OSubsFuz(Test2, Test1), OSubsFuz2(Test2, Test1));
            Console.WriteLine(OSubs);
            //------------------------------------------------------
            Console.Write("26. Ограниченная операция симметричного вычитания ---");
            OSyncSubs = CompareLists(OSyncSubsFuz(Testnew1, Testnew2), OSyncSubsFuz2(Testnew1, Testnew2));
            Console.WriteLine( OSyncSubs);
            //------------------------------------------------------
            Console.Write("27. Дефаззификация - метод центра тяжести ---------- ");
            if (DifuzCoG(TestDefuz) == UnswerDefuzCoG)
                DifuzzCOG = true;
            else
                DifuzzCOG = false;
            Console.WriteLine(DifuzzCOG);
            //------------------------------------------------------
            Console.Write("28. Деффазификация - метод центра площади ---------- ");
            if (DifuzCoA(TestDefuz1) == UnswerDefuzCoA)
                DifuzzCOA = true;
            else
                DifuzzCOA = false;
            Console.WriteLine(DifuzzCOA);
            //------------------------------------------------------
            Console.Write("29. Дефаззификация - метод левой моды  ------------- ");
            if (DifuzModa(TestDefuz1,"left") ==UnswerDefuzSoM)
                DifuzzSOM = true;
            else
                DifuzzSOM = false;
            Console.WriteLine(DifuzzSOM);
            //------------------------------------------------------
            Console.Write("30. Дефаззификация - метод правой моды  ------------ ");
            if (DifuzModa(TestDefuz1, "right") == UnswerDefuzLoM)
                DifuzzLOM= true;
            else
                DifuzzLOM = false;
            Console.WriteLine(DifuzzLOM);
            //------------------------------------------------------
            Console.Write("31. Дефаззификация - метод средней моды  ------------");
            if (DifuzModa(TestDefuz1, "mid") == UnswerDefuzMoM)
                DifuzzMOM = true;
            else
                DifuzzMOM = false;
            Console.WriteLine(DifuzzMOM);
            //------------------------------------------------------
            //------------------------------------------------------
            Console.Write("32. Декомпозиция  для ансабля [(1,0.8),(2,0.6),(3,0.3),(4,0.9),(5,1.0)] ");
            Console.WriteLine();
            Decomp(TestDeComp);
            Console.WriteLine();
            //------------------------------------------------------
            Console.Write("33. Концентрация ------------------------------------");
            ConcentrateBool = CompareLists(Concentrate(Test1), UnswerConcentrate);
            Console.WriteLine(ConcentrateBool);
            //------------------------------------------------------
            Console.Write("34. Растяжение --------------------------------------");
            DilationBool = CompareLists(Dilation(Test1), UnswerDilation);
            Console.WriteLine(DilationBool);
            //------------------------------------------------------
            Console.Write("34. Альфа разрез ------------------------------------");
            AlfaCutBool = CompareLists(AlfaCut(Test1,0.5), UnswerAlfaCut);
            Console.WriteLine(AlfaCutBool);
            //------------------------------------------------------
            Console.WriteLine("----------- БИНАРНЫЕ ОПЕРАЦИИ (Л.Задe) ------------- ");
            Console.Write("35. Сложение ----------------------------------------");
            BinaryBoolAd = CompareLists(BinaryOperation(Test1, Test2, "addition"), UnswerBinaryAdd);
            Console.WriteLine(BinaryBoolAd);
            //------------------------------------------------------
            Console.Write("36. Вычитание ---------------------------------------");
            BinaryBoolSub = CompareLists(BinaryOperation(Test1, Test2, "substract"), UnswerBinarySubs);
            Console.WriteLine(BinaryBoolSub);
            //------------------------------------------------------
            Console.Write("37. Умножение ---------------------------------------");
            BinaryBoolMult = CompareLists(BinaryOperation(Test1, Test2, "mult"), UnswerBinaryMult);
            Console.WriteLine(BinaryBoolMult);
            //------------------------------------------------------
            Console.Write("38. Деление -----------------------------------------");
            BinaryBoolDiv = CompareLists(BinaryOperation(Test1, Test2, "div"), UnswerBinaryDiv);
            Console.WriteLine(BinaryBoolDiv);
            Console.WriteLine("---------------- УНАРНЫЕ ОПЕРАЦИИ ------------------- ");
            //------------------------------------------------------
            Console.Write("39. Противоположное -------------------------------- ");
           // BinaryBoolDiv = CompareLists(BinaryOperation(Test1, Test2, "opposite"), UnswerOpposite);
            Console.WriteLine(CompareLists(UnoOperation(Test1,"opposite"), UnswerOpposite));
            //------------------------------------------------------
            Console.Write("40. Обратное --------------------------------------- ");
            // BinaryBoolDiv = CompareLists(BinaryOperation(Test1, Test2, "opposite"), UnswerOpposite);
            Console.WriteLine(CompareLists(UnoOperation(Test1, "reverse"), UnswerReverse));
            //------------------------------------------------------
            Console.WriteLine("----------- БИНАРНЫЕ ОПЕРАЦИИ (LR-числа) ------------ ");
            Console.Write("41. Сложение(треуг) ---------------------------------");
            Console.WriteLine(BinaryOperationLR(TestLR_Set1, TestLR_Set2, "addition").SequenceEqual(UnswerBinLRAd));
            //------------------------------------------------------
            Console.Write("42. Вычитание(треуг) --------------------------------");
            Console.WriteLine(BinaryOperationLR(TestLR_Set1, TestLR_Set2, "substract").SequenceEqual(UnswerBinLRSub));
            //------------------------------------------------------
            Console.Write("43. Сложение(трап) ----------------------------------");
            Console.WriteLine(BinaryOperationLR(TestLR_Set3, TestLR_Set4, "addition").SequenceEqual(UnswerBinLRAdT));
            //------------------------------------------------------
            Console.Write("44. Вычитание(трап) ---------------------------------");
            Console.WriteLine(BinaryOperationLR(TestLR_Set3, TestLR_Set4, "substract").SequenceEqual(UnswerBinLRSubT));
            Console.ReadLine();
        }
        public static bool InvariantTesting(List<KeyValuePair<double, double>> listIn, List<KeyValuePair<double, double>> listIn2)
        {
            Console.WriteLine("Генеративно созданные множества над которыми проводились тесты:");
            PrintOut(listIn);
            Console.WriteLine();
            PrintOut(listIn2);
            Console.WriteLine();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>(listIn);
            /*---------- Тест на наличие степеней принадлежности <0 -----------*/
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Value < 0.0)
                {
                    Console.WriteLine("! Найдена степень принадлежности <0 !");
                    return false;
                }
            }
            /*---------- Тест на наличие степеней принадлежности >1 ---------*/
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Value > 1.0)
                {
                    Console.WriteLine("! Найдена степень принадлежности >1 !");
                    return false;
                }
            }
            /*-------------------- Тест носителя -----------------*/
            list = FuzzySupport(listIn);
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Value == 0.0) {
                    Console.WriteLine("! Найден нуль в носителе !");
                    return false;
                }
            }
            /*--------------------- Тест ядра -----------------------*/
            list = FuzzyCore(listIn);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Value != 1.0)
                {
                    Console.WriteLine("! Найдена не единица в ядре !");
                    return false;
                }
            }
            /*=========================== Тесты Максиминных операций ===================================*/
            /*--------- Тест операции вычитания - сравнение через разные реализации -----------*/
            if (!SubsFuz(listIn, listIn2).SequenceEqual(SubsFuz2(listIn, listIn2))) { Console.WriteLine("Максиминная разность - не пройден"); return false; } 
            /*------ Тест операции симметричного вычитания - сравнение через разные реализации --------*/
            if (!SubsSyncFuz(listIn, listIn2).SequenceEqual(SubsSyncFuz2(listIn, listIn2))) { Console.WriteLine("Максиминная симметричная разность - не пройден"); return false; } 
            /*=========================== Тесты Алгебраических операций ===================================*/
            /*---- Тест операции вычитания - сравнение через разные реализации --------------------*/
            if (!ALSubsFuz(listIn, listIn2).SequenceEqual(ALSubsFuz2(listIn, listIn2))) { Console.WriteLine("Алгебраическая разность - не пройден"); return false; } 
            /*-------- Тест операции симметричного вычитания - сравнение через разные реализации ----------*/
           // if (!CompareLists(AlSubsSyncFuz(listIn, listIn2), AlSubsSyncFuz2(listIn, listIn2))) { Console.WriteLine("Алгебраическая симметричная разность - не пройден"); return false; } 
            /*=========================== Тесты Ограниченных операций ===================================*/
            /*---- Тест операции вычитания - сравнение через разные реализации --------------------*/
            if (!CompareLists(OSubsFuz(listIn, listIn2), OSubsFuz2(listIn, listIn2))) { Console.WriteLine("Ограниченная разность - не пройден"); return false; } 
            /*-------- Тест операции симметричного вычитания - сравнение через разные реализации ----------*/
            if (!CompareLists(OSyncSubsFuz(listIn, listIn2), OSyncSubsFuz2(listIn, listIn2))) { Console.WriteLine("Ограниченная симметричная разность - не пройден"); return false; } 
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Все инвариантные тесты выполнены. Затраченное время: {0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine();
            
            Console.WriteLine();
            return true;
        }
    }
}
