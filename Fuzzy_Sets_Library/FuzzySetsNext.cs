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
                                        Heap.Add(new KeyValuePair<double, double>(Math.Round(WorkingSet1[i].Key / WorkingSet2[j].Key,2), (WorkingSet1[i].Value >= WorkingSet2[j].Value) ? WorkingSet1[i].Value : WorkingSet2[j].Value));
                                        break;
                                    }
                            }
                        }
            }
            Heap.Sort((x,y)=>x.Key.CompareTo(y.Key));
            Heap = Heap.GroupBy(x => x.Key).Select(g => g.First()).ToList();
            return Heap;
            //Heap.ForEach(x => Console.Write(x + " "));
           // Console.ReadLine();
        }
        public static List<KeyValuePair<double, double>> UnoOperation(List<KeyValuePair<double, double>> Set1, string operation)
        {
            List<KeyValuePair<double, double>> WorkingSet1 = new List<KeyValuePair<double, double>>();
          switch(operation)
          {
              case "opposite":
                  {
                      Set1.ForEach(x => WorkingSet1.Add(new KeyValuePair<double, double>(x.Key * (-1), x.Value)));
                      break;
                  }
              case "reverse":
                  {
                      Set1.ForEach(x => WorkingSet1.Add(new KeyValuePair<double, double>(Math.Round(1/x.Key,2), x.Value)));
                      break;
                  }
          }
         // WorkingSet1.ForEach(x => Console.Write(x + " "));
          return WorkingSet1;
        }
        public static List<double> BinaryOperationLR(List<double> Set1, List<double> Set2, string OperationName)
        {
            List<double> LRSet1 = new List<double>(Set1);
            List<double> LRSet2 = new List<double>(Set2);
            
         /*   if (Set1.Count == 3) {
                LRSet1.Add(Set1[1]);
                LRSet1.Add(LRSet1[0]-Set1[0]);
                LRSet1.Add(Set1[2]-LRSet1[0]);
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
            }*/
            List<double> Result = new List<double>();
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
                        case "substract":
                            {
                                if (LRSet1.Count == 4)
                                {
                                    Result.Add(LRSet1[0] - LRSet2[0]);
                                    Result.Add(LRSet1[1] - LRSet2[1]);
                                    Result.Add(LRSet1[2] + LRSet2[3]);
                                    Result.Add(LRSet2[2] + LRSet1[3]);
                                }
                                else {
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
                                    if ((LRSet1[0] >= 0 && LRSet2[0] >= 0)||(LRSet1[0] <0 && LRSet2[0] <0))
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
                   // Result.ForEach(x => Console.Write(x + " "));
                    return Result;
           // Heap.Sort((x, y) => x.Key.CompareTo(y.Key));
          //  Heap = Heap.GroupBy(x => x.Key).Select(g => g.First()).ToList();
          //  return Heap;
            //Heap.ForEach(x => Console.Write(x + " "));
            // Console.ReadLine();
        }

    }
}
