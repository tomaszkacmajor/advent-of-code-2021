using System;

namespace AdventOfCode
{
    public class Day5
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input5-1.txt");

            int[,] tab = new int[10000, 10000];
            int cnt = 0;

            foreach (var line in lines)
            {
                string[] row = line.Split(new[] { "", " ", ",", "->" }, StringSplitOptions.RemoveEmptyEntries);
                int x0 = int.Parse(row[0]);
                int y0 = int.Parse(row[1]);
                int x1 = int.Parse(row[2]);
                int y1 = int.Parse(row[3]);

                if (x0 == x1 && y0 < y1)
                {
                    for (int i = y0; i <= y1; i++)
                    {
                        tab[x0, i]++;
                        if (tab[x0, i] == 2)
                            cnt++;
                    }
                }

                if (x0 == x1 && y0 >= y1)
                {
                    for (int i = y0; i >= y1; i--)
                    {
                        tab[x0, i]++;
                        if (tab[x0, i] == 2)
                            cnt++;
                    }
                }

                if (y0 == y1 && x0 < x1)
                {
                    for (int i = x0; i <= x1; i++)
                    {
                        tab[i, y0]++;
                        if (tab[i, y0] == 2)
                            cnt++;
                    }
                }

                if (y0 == y1 && x0 >= x1)
                {
                    for (int i = x0; i >= x1; i--)
                    {
                        tab[i, y0]++;
                        if (tab[i, y0] == 2)
                            cnt++;
                    }
                }

            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input5-2.txt");

            int[,] tab = new int[10000, 10000];
            int cnt = 0;

            foreach (var line in lines)
            {
                string[] row = line.Split(new[] { "", " ", ",", "->" }, StringSplitOptions.RemoveEmptyEntries);
                int x0 = int.Parse(row[0]);
                int y0 = int.Parse(row[1]);
                int x1 = int.Parse(row[2]);
                int y1 = int.Parse(row[3]);

                if (x0 == x1 && y0 < y1)
                {
                    for (int i = y0; i <= y1; i++)
                    {
                        tab[x0, i]++;
                        if (tab[x0, i] == 2)
                            cnt++;
                    }
                }

                else if (x0 == x1 && y0 >= y1)
                {
                    for (int i = y0; i >= y1; i--)
                    {
                        tab[x0, i]++;
                        if (tab[x0, i] == 2)
                            cnt++;
                    }
                }
                else if (y0 == y1 && x0 < x1)
                {
                    for (int i = x0; i <= x1; i++)
                    {
                        tab[i, y0]++;
                        if (tab[i, y0] == 2)
                            cnt++;
                    }
                }
                else if (y0 == y1 && x0 >= x1)
                {
                    for (int i = x0; i >= x1; i--)
                    {
                        tab[i, y0]++;
                        if (tab[i, y0] == 2)
                            cnt++;
                    }
                }
                else
                {
                    double a = (y1 - y0) / (x1 - x0);
                    int b = y1 - (int)(a * x1);

                    if (x0 < x1)
                    {
                        for (int i = x0; i <= x1; i++)
                        {
                            int y = (int)(a * i) + b;
                            tab[i, y]++;
                            if (tab[i, y] == 2)
                                cnt++;
                        }
                    }

                    if (x0 >= x1)
                    {
                        for (int i = x0; i >= x1; i--)
                        {
                            int y = (int)(a * i) + b;
                            tab[i, y]++;
                            if (tab[i, y] == 2)
                                cnt++;
                        }
                    }
                }
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }
    }
}
