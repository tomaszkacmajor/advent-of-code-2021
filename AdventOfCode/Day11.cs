using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day11
    {
        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input11-1.txt");
            int result = 0;

            int[,] tab = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tab[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            List<(int, int)> dirs = new List<(int, int)>();
            dirs.Add((-1, -1));
            dirs.Add((-1, 0));
            dirs.Add((-1, 1));
            dirs.Add((1, -1));
            dirs.Add((1, 0));
            dirs.Add((1, 1));
            dirs.Add((0, -1));
            dirs.Add((0, 1));

            for (int step = 0; step < 100000; step++)
            {
                List<(int, int)> list = new List<(int, int)>();

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        tab[i, j]++;
                        if (tab[i, j] == 10)
                        {
                            tab[i, j] = 0;
                            list.Add((i, j));
                            result++;
                        }
                    }
                }

                while (list.Count > 0)
                {
                    int i = list[list.Count - 1].Item1;
                    int j = list[list.Count - 1].Item2;
                    list.RemoveAt(list.Count - 1);

                    foreach (var dir in dirs)
                    {
                        int x = i + dir.Item1;
                        int y = j + dir.Item2;
                        if (x >= 0 && x < 10 && y >= 0 && y < 10)
                        {
                            if (tab[x, y] != 0)
                            {
                                tab[x, y]++;
                                if (tab[x, y] == 10)
                                {
                                    tab[x, y] = 0;
                                    list.Add((x, y));
                                    result++;
                                }
                            }
                        }
                    }
                }

                bool all = true;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (tab[i, j] != 0)
                        {
                            all = false;
                            break;
                        }
                    }

                    if (!all)
                        break;
                }

                if (all)
                {
                    result = step;
                    break;

                }

            }

            Console.WriteLine(result + 1);
            Console.ReadKey();
        }
    }
}
