using System;

namespace AdventOfCode
{
    public class Day9
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input9-1.txt");
            int result = 0;

            int[,] tab = new int[lines.Length, lines[0].Length];
            int colNo = 0;
            int rowNo = 0;
            foreach (var line in lines)
            {
                var row = line.ToCharArray();
                colNo = 0;
                foreach (var r in row)
                {
                    int num = int.Parse(r.ToString());
                    tab[rowNo, colNo] = num;
                    colNo++;
                }
                rowNo++;
            }

            for (int i = 0; i < rowNo; i++)
            {
                for (int j = 0; j < colNo; j++)
                {
                    bool min = true;
                    int num = tab[i, j];
                    if (i - 1 >= 0)
                    {
                        min = num < tab[i - 1, j] ? true : false;
                        if (!min)
                            continue;
                    }

                    if (i + 1 < rowNo)
                    {
                        min = num < tab[i + 1, j] ? true : false;
                        if (!min)
                            continue;
                    }

                    if (j - 1 >= 0)
                    {
                        min = num < tab[i, j - 1] ? true : false;
                        if (!min)
                            continue;
                    }

                    if (j + 1 < colNo)
                    {
                        min = num < tab[i, j + 1] ? true : false;
                        if (!min)
                            continue;
                    }

                    if (min)
                    {
                        result += 1 + num;
                    }
                }


            }

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public bool[,] visited;

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input9-1.txt");

            int[,] tab = new int[lines.Length, lines[0].Length];
            int colNo = 0;
            int rowNo = 0;
            foreach (var line in lines)
            {
                var row = line.ToCharArray();
                colNo = 0;
                foreach (var r in row)
                {
                    int num = int.Parse(r.ToString());
                    tab[rowNo, colNo] = num;
                    colNo++;
                }
                rowNo++;
            }

            int max1 = 0;
            int max2 = 0;
            int max3 = 0;

            for (int i = 0; i < rowNo; i++)
            {
                for (int j = 0; j < colNo; j++)
                {
                    bool isLower = true;
                    int num = tab[i, j];
                    if (i - 1 >= 0)
                    {
                        isLower = num < tab[i - 1, j] ? true : false;
                        if (!isLower)
                            continue;
                    }

                    if (i + 1 < rowNo)
                    {
                        isLower = num < tab[i + 1, j] ? true : false;
                        if (!isLower)
                            continue;
                    }

                    if (j - 1 >= 0)
                    {
                        isLower = num < tab[i, j - 1] ? true : false;
                        if (!isLower)
                            continue;
                    }

                    if (j + 1 < colNo)
                    {
                        isLower = num < tab[i, j + 1] ? true : false;
                        if (!isLower)
                            continue;
                    }

                    if (isLower)
                    {
                        visited = new bool[rowNo, colNo];
                        for (int x = 0; x < rowNo; x++)
                        {
                            for (int y = 0; y < colNo; y++)
                            {
                                visited[x, y] = false;
                            }
                        }

                        int size = Search(tab, i, j, rowNo, colNo);
                        if (size > max1)
                        {
                            max3 = max2;
                            max2 = max1;
                            max1 = size;
                        }
                        else if (size > max2)
                        {
                            max3 = max2;
                            max2 = size;
                        }
                        else if (size > max3)
                        {
                            max3 = size;
                        }
                    }
                }


            }

            Console.WriteLine(max1 * max2 * max3);
            Console.ReadKey();
        }

        private int Search(int[,] tab, int i, int j, int rowNo, int colNo)
        {
            int num = tab[i, j];
            int result = 1;
            visited[i, j] = true;

            bool isLower;
            if (i - 1 >= 0 && tab[i - 1, j] < 9 && !visited[i - 1, j])
            {
                isLower = num < tab[i - 1, j] ? true : false;
                if (isLower)
                {
                    result += Search(tab, i - 1, j, rowNo, colNo);
                }
            }

            if (i + 1 < rowNo && tab[i + 1, j] < 9 && !visited[i + 1, j])
            {
                isLower = num < tab[i + 1, j] ? true : false;
                if (isLower)
                {
                    result += Search(tab, i + 1, j, rowNo, colNo);
                }
            }

            if (j - 1 >= 0 && tab[i, j - 1] < 9 && !visited[i, j - 1])
            {
                isLower = num < tab[i, j - 1] ? true : false;
                if (isLower)
                {
                    result += Search(tab, i, j - 1, rowNo, colNo);
                }
            }

            if (j + 1 < colNo && tab[i, j + 1] < 9 && !visited[i, j + 1])
            {
                isLower = num < tab[i, j + 1] ? true : false;
                if (isLower)
                {
                    result += Search(tab, i, j + 1, rowNo, colNo);
                }
            }

            return result;
        }
    }
}
