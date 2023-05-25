using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs_Acycl
{
    class Point
    {
        public int Number;
        public List<int> OrientedTo;

        public override string ToString()
        {
            string listtostr = "";
            foreach (int i in OrientedTo) { listtostr += i.ToString(); ; listtostr += ","; }

            return ($"Number: {Number} OrientedTo {listtostr}");
        }
        public Point(int Number, List<int> OrientedTo)
        {
            this.Number = Number;
            this.OrientedTo = OrientedTo;
        }
    }
    class Program
    {
        static List<Point> Init2(List<Point> toFill)
        {
            toFill = new List<Point>()
            {
                new Point(1, new List<int>() { 2 })
                ,new Point(2, new List<int>() { 3})
                ,new Point(3, new List<int>() { 1,4,5 })
                ,new Point(4, new List<int>() { 1 })
                ,new Point(5, new List<int>() { 4 })
            };
            return toFill;
            //1-2, 2-3, 3-4, 3-5, 5-4
        }
        static List<Point> Init1(List<Point> toFill)
        {
            toFill = new List<Point>()
            {
                 new Point(1, new List<int>(){2 })
                ,new Point(2, new List<int>(){3,4})
                ,new Point(3, new List<int>(){1})
                ,new Point(4, new List<int>(){5})
                ,new Point(5, new List<int>(){6 })
                ,new Point(6, new List<int>(){4,7})
                ,new Point(7, new List<int>(){ })
                //Номера записывать обязательно с 1, не пропуская цифр
                //Ответ: 1,2,3,4,5,6,7
            };
            return toFill;
        }
        static List<string> Ans = new List<string>();

        static void CheckAns(ref List<string> AnsStr)
        {
            var tempans = AnsStr.Distinct().ToList();
            if(AnsStr.Count > Ans.Count)
            {
                Ans = new List<string>(AnsStr);
            }
        }
        static void Shuffle_Next(ref List<Point> points, List<int> UsedPrevPoints, List<int> ToGo,ref List<string> PrevAnsStr)
        {
            //var tempans = new List<string>();
            for(int i = 0; i < ToGo.Count; i++)
            {
                //var _p = points[ToGo[i] - 1];
                if(!UsedPrevPoints.Contains(ToGo[i]))
                {
                    var ansstr = new List<string>(PrevAnsStr);
                    var usedpoints = new List<int>(UsedPrevPoints);
                    usedpoints.Add(ToGo[i]);
                    string tempstr = $"Number: {UsedPrevPoints[UsedPrevPoints.Count - 1]} to: {ToGo[i]}";
                    var check = false;
                    foreach(var s in PrevAnsStr)
                    {
                        if(String.Compare(tempstr, s) == 0)
                            check = true;
                    }
                    if(!check)
                        PrevAnsStr.Add(tempstr);


                    var _p = points[ToGo[i] - 1].OrientedTo;
                    Shuffle_Next(ref points, usedpoints, _p,ref PrevAnsStr);

                    CheckAns(ref PrevAnsStr);
                }    
            }
            //return tempans;//CHANGE//CHANGE//CHANGE//CHANGE//CHANGE//CHANGE//CHANGE//CHANGE//CHANGE
        }
        //AnsStr.Add($"Number: {UsedPrevPoints[UsedPrevPoints.Count-1]} to: {ToGo[i]}");

        static void Shuffle(List<Point> points)
        {
            var BestAnsStr = new List<string>();
            for(int i = 0; i < points.Count; i++)
            {
                var UsedPoints = new List<int>();
                UsedPoints.Add(points[i].Number);
                var AnsStr = new List<string>();
                Shuffle_Next(ref points, UsedPoints, points[i].OrientedTo,ref AnsStr);
            }
        }

        static void Main()
        {
            var Points = new List<Point>();
            Points = Init1(Points);
            //var Ans = new List<string>();
            Shuffle(Points);
            foreach(var t in Ans)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
