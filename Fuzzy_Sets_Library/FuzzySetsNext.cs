using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuzzySets
{
    public partial class Fuz
    {
        public static List<KeyValuePair<double, double>> BinaryOperation(List<KeyValuePair<double, double>> Set1, List<KeyValuePair<double, double>> Set2, string OperationName)
        {
            List<KeyValuePair<double, double>> WorkingSet1 = new List<KeyValuePair<double, double>>(Set1);
            List<KeyValuePair<double, double>> WorkingSet2 = new List<KeyValuePair<double, double>>(Set2);
            List<KeyValuePair<double, double>> Heap = new List<KeyValuePair<double, double>>();
            for (int i = 0; i < WorkingSet1.Count; i++)
            {
                for (int j = 0; j < WorkingSet2.Count; j++)
                {
                    switch (OperationName)
                    {
                        case "addition":
                            {
                                Heap.Add(new KeyValuePair<double, double>(WorkingSet1[i].Key + WorkingSet2[j].Key, (WorkingSet1[i].Value >= WorkingSet2[j].Value) ? WorkingSet1[i].Value : WorkingSet2[j].Value));
                                break;
                            }
                        case "substract":
                            {
                                Heap.Add(new KeyValuePair<double, double>(WorkingSet1[i].Key - WorkingSet2[j].Key, (WorkingSet1[i].Value >= WorkingSet2[j].Value) ? WorkingSet1[i].Value : WorkingSet2[j].Value));
                                break;
                            }
                        case "mult":
                            {
                                Heap.Add(new KeyValuePair<double, double>(WorkingSet1[i].Key * WorkingSet2[j].Key, (WorkingSet1[i].Value >= WorkingSet2[j].Value) ? WorkingSet1[i].Value : WorkingSet2[j].Value));
                                break;
                            }
                        case "div":
                            {
                                Heap.Add(new KeyValuePair<double, double>(Math.Round(WorkingSet1[i].Key / WorkingSet2[j].Key, 2), (WorkingSet1[i].Value >= WorkingSet2[j].Value) ? WorkingSet1[i].Value : WorkingSet2[j].Value));
                                break;
                            }
                    }
                }
            }
            Heap.Sort((x, y) => x.Key.CompareTo(y.Key));
            Heap = Heap.GroupBy(x => x.Key).Select(g => g.First()).ToList();
            return Heap;
            //Heap.ForEach(x => Console.Write(x + " "));
            // Console.ReadLine();
        }
        public static List<KeyValuePair<double, double>> UnoOperation(List<KeyValuePair<double, double>> Set1, string operation)
        {
            List<KeyValuePair<double, double>> WorkingSet1 = new List<KeyValuePair<double, double>>();
            switch (operation)
            {
                case "opposite":
                    {
                        Set1.ForEach(x => WorkingSet1.Add(new KeyValuePair<double, double>(x.Key * (-1), x.Value)));
                        break;
                    }
                case "reverse":
                    {
                        Set1.ForEach(x => WorkingSet1.Add(new KeyValuePair<double, double>(Math.Round(1 / x.Key, 2), x.Value)));
                        break;
                    }
            }
            // WorkingSet1.ForEach(x => Console.Write(x + " "));
            return WorkingSet1;
        }
        public static List<double> BinaryOperationLR(List<double> Set1, List<double> Set2, string OperationName, int Mode)
        {
            List<double> Result = new List<double>();
            List<double> AbsRes = new List<double>();
            if (Mode == 0)
            {
                List<double> LRSet1 = new List<double>();
                List<double> LRSet2 = new List<double>();

                if (Set1.Count == 3)
                {
                    LRSet1.Add(Set1[1]);
                    LRSet1.Add(LRSet1[0] - Set1[0]);
                    LRSet1.Add(Set1[2] - LRSet1[0]);
                    LRSet2.Add(Set2[1]);
                    LRSet2.Add(LRSet2[0] - Set2[0]);
                    LRSet2.Add(Set2[2] - LRSet2[0]);
                }
                else if (Set1.Count == 4)
                {
                    LRSet1.Add(Set1[1]);
                    LRSet1.Add(Set1[2]);
                    LRSet1.Add(Set1[1] - Set1[0]);
                    LRSet1.Add(Set1[3] - Set1[2]);
                    LRSet2.Add(Set2[1]);
                    LRSet2.Add(Set2[2]);
                    LRSet2.Add(Set2[1] - Set2[0]);
                    LRSet2.Add(Set2[3] - Set2[2]);
                }
                LRSet1.ForEach(x => Console.Write(x + " "));
                Console.WriteLine();
                LRSet2.ForEach(x => Console.Write(x + " "));
                Console.WriteLine("Result");

                switch (OperationName)
                {
                    case "addition":
                        {
                            Result.Add(LRSet1[0] + LRSet2[0]);
                            Result.Add(LRSet1[1] + LRSet2[1]);
                            Result.Add(LRSet1[2] + LRSet2[2]);
                            if (LRSet1.Count == 4)
                            {
                                Result.Add(LRSet1[3] + LRSet2[3]);
                            }
                            break;
                        }
                    case "sub":
                        {
                            if (LRSet1.Count == 4)
                            {
                                Result.Add(LRSet1[0] - LRSet2[0]);
                                Result.Add(LRSet1[1] - LRSet2[1]);
                                Result.Add(LRSet1[2] + LRSet2[3]);
                                Result.Add(LRSet1[3] + LRSet2[2]);
                            }
                            else
                            {
                                Result.Add(LRSet1[0] - LRSet2[0]);
                                Result.Add(LRSet1[1] + LRSet2[2]);
                                Result.Add(LRSet2[1] + LRSet1[2]);
                            }
                            break;
                        }
                    case "mult":
                        {
                            if (LRSet1.Count == 3)
                            {
                                if ((LRSet1[0] >= 0 && LRSet2[0] >= 0) || (LRSet1[0] < 0 && LRSet2[0] < 0))
                                {
                                    Result.Add(LRSet1[0] * LRSet2[0]);
                                    Result.Add(LRSet1[0] * LRSet2[1] + LRSet2[0] * LRSet1[1]);
                                    Result.Add(LRSet1[0] * LRSet2[2] + LRSet2[0] * LRSet1[2]);
                                }
                                if (LRSet1[0] < 0 && LRSet2[0] >= 0)
                                {
                                    Result.Add(LRSet1[0] * LRSet2[0]);
                                    Result.Add(LRSet2[0] * LRSet1[1] - LRSet1[0] * LRSet2[2]);
                                    Result.Add(LRSet2[0] * LRSet1[2] - LRSet1[0] * LRSet2[1]);
                                }
                                if (LRSet1[0] >= 0 && LRSet2[0] < 0)
                                {
                                    Result.Add(LRSet1[0] * LRSet2[0]);
                                    Result.Add(-LRSet2[0] * LRSet1[2] - LRSet1[0] * LRSet2[2]);
                                    Result.Add(-LRSet2[0] * LRSet1[1] - LRSet1[0] * LRSet2[1]);
                                }
                            }
                            break;
                        }
                    case "div":
                        {
                            break;
                        }
                }
                if (Result[0] > Result[1])
                {
                    double temp = Result[0];
                    Result[0] = Result[1];
                    Result[1] = temp;
                }
                if (LRSet1.Count == 4)
                {
                    AbsRes.Add(Result[0] - Result[2]);
                    AbsRes.Add(Result[0]);
                    AbsRes.Add(Result[1]);
                    AbsRes.Add(Result[1] + Result[3]);
                }
                if (LRSet1.Count == 3)
                {
                    AbsRes.Add(Result[0] - Result[1]);
                    AbsRes.Add(Result[0]);
                    AbsRes.Add(Result[1] + Result[2]);
                }
            }
            else
            {
                switch (OperationName)
                {
                    case "addition":
                        {
                            Result.Add(Set1[0] + Set2[0]);
                            Result.Add(Set1[1] + Set2[1]);
                            Result.Add(Set1[2] + Set2[2]);
                            if (Set1.Count == 4)
                            {
                                Result.Add(Set1[3] + Set2[3]);
                            }
                            break;
                        }
                    case "sub":
                        {
                            if (Set1.Count == 4)
                            {
                                Result.Add(Set1[0] - Set2[3]);
                                Result.Add(Set1[1] - Set2[2]);
                                Result.Add(Set1[2] - Set2[1]);
                                Result.Add(Set1[3] - Set2[0]);
                            }
                            else
                            {
                                Result.Add(Set1[0] - Set2[2]);
                                Result.Add(Set1[1] - Set2[1]);
                                Result.Add(Set1[2] - Set2[0]);
                            }
                            break;
                        }
                    case "prod":
                        {
                            if (Set1.Count == 4)
                            {
                                double a1a2 = Set1[0] * Set2[0];
                                double a1d2 = Set1[0] * Set2[3];
                                double d1a2 = Set1[3] * Set2[0];
                                double d1d2 = Set1[3] * Set2[3];
                                /*============================*/
                                double b1b2 = Set1[1] * Set2[1];
                                double b1c2 = Set1[1] * Set2[2];
                                double c1b2 = Set1[2] * Set2[1];
                                double c1c2 = Set1[2] * Set2[2];
                                List<double> L0 = new List<double>() { a1a2, a1d2, d1a2, d1d2 };
                                List<double> L1 = new List<double>() { b1b2, b1c2, c1b2, c1c2 };
                                double a = L0.Min();
                                double d = L0.Max();
                                double b = L1.Min();
                                double c = L1.Max();
                                Result.Add(a);
                                Result.Add(b);
                                Result.Add(c);
                                Result.Add(d);
                            }
                            else
                            {
                                Result.Add(Set1[0] * Set2[0]);
                                Result.Add(Set1[1] * Set2[1]);
                                Result.Add(Set1[2] * Set2[2]);
                            }
                            break;
                        }
                    case "div":
                        {
                            if (Set1.Count == 4)
                            {
                                List<double> Set3 =new List<double>(Reverse(Set2));
                                double a1a2 = Set1[0] * Set3[0];
                                double a1d2 = Set1[0] * Set3[3];
                                double d1a2 = Set1[3] * Set3[0];
                                double d1d2 = Set1[3] * Set3[3];
                                /*============================*/
                                double b1b2 = Set1[1] * Set3[1];
                                double b1c2 = Set1[1] * Set3[2];
                                double c1b2 = Set1[2] * Set3[1];
                                double c1c2 = Set1[2] * Set3[2];
                                List<double> L0 = new List<double>() { a1a2, a1d2, d1a2, d1d2 };
                                List<double> L1 = new List<double>() { b1b2, b1c2, c1b2, c1c2 };
                                double a = L0.Min();
                                double d = L0.Max();
                                double b = L1.Min();
                                double c = L1.Max();
                                Result.Add(a);
                                Result.Add(b);
                                Result.Add(c);
                                Result.Add(d);
                            }
                            else
                            {
                                Result.Add(Set1[0] / Set2[0]);
                                Result.Add(Set1[1] / Set2[1]);
                                Result.Add(Set1[2] / Set2[2]);
                            }
                            break;
                        }
                }
                AbsRes = Result;

                // Result.ForEach(x => Console.Write(x + " "));

                // Heap.Sort((x, y) => x.Key.CompareTo(y.Key));
                //  Heap = Heap.GroupBy(x => x.Key).Select(g => g.First()).ToList();
                //  return Heap;
                //Heap.ForEach(x => Console.Write(x + " "));
                // Console.ReadLine();
            }
            return AbsRes;
        }
        public static List<double> Reverse(List<double> InputList) {
            return new List<double>() {1/InputList[3],1/InputList[2], 1 / InputList[1], 1 / InputList[0] };
        }
    }
}
