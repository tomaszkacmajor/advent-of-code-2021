using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day13
    {
        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input13-1.txt");

            List<(int, int)> list = new List<(int, int)>();
            List<(string, int)> folds = new List<(string, int)>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                if (!line.StartsWith("fold"))
                {
                    var str = line.Split(',');
                    int x = int.Parse(str[0]);
                    int y = int.Parse(str[1]);
                    list.Add((x, y));
                }
                else
                {
                    var str1 = line.Split(new[] { "fold along ", "=" }, StringSplitOptions.RemoveEmptyEntries);
                    string foldAxis = str1[0];
                    int num = int.Parse(str1[1]);
                    folds.Add((foldAxis, num));
                }
            }

            foreach (var fold in folds)
            {
                var axis = fold.Item1;
                var num = fold.Item2;

                List<(int, int)> toAdd = new List<(int, int)>();
                List<(int, int)> toRemove = new List<(int, int)>();

                foreach (var dot in list)
                {
                    var x = dot.Item1;
                    var y = dot.Item2;

                    if (axis == "x" && x > num)
                    {
                        int newX = num - (x - num);
                        toRemove.Add((x, y));
                        if (!list.Contains((newX, y)))
                        {
                            toAdd.Add((newX, y));
                        }
                    }

                    if (axis == "y" && y > num)
                    {
                        int newY = num - (y - num);
                        toRemove.Add((x, y));
                        if (!list.Contains((x, newY)))
                        {
                            toAdd.Add((x, newY));
                        }
                    }
                }

                list = list.Except(toRemove).ToList();
                list = list.Union(toAdd).ToList();

            }


            for (int i = 0; i < 100; i++)
            {
                string res = "";
                for (int j = 0; j < 100; j++)
                {
                    if (list.Contains((j, i)))
                    {
                        res += "#";
                    }
                    else
                    {
                        res += ".";
                    }
                }

                Console.WriteLine(res);
            }

            Console.ReadKey();
        }
    }
}
