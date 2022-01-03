using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day15
    {
        static int[,] minScores;

        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input15-1.txt");
      
            int[,] grid = new int[lines.Length, lines[0].Length];
            minScores = new int[lines.Length, lines[0].Length];
            int rowNo = 0;
            foreach (var line in lines)
            {
                var row = line.ToCharArray();
                int colNo = 0;
                foreach (var r in row)
                {
                    int num = int.Parse(r.ToString());
                    grid[rowNo, colNo] = num;
                    minScores[rowNo, colNo] = Int32.MaxValue;
                    colNo++;
                }
                rowNo++;
            }

            // applying BFS on matrix cells starting from source
            Queue<QItem> queue = new Queue<QItem>();
            queue.Enqueue(new QItem(0, 0, 0));
            minScores[0, 0] = 0;

            bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            visited[0, 0] = true;

            while (queue.Count > 0)
            {
                QItem p = queue.Dequeue();

                int actScore = minScores[p.row, p.col];

                // moving up
                if (isValid(p.row - 1, p.col, grid, visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row - 1, p.col,
                                        p.dist + grid[p.row - 1, p.col]));
                    visited[p.row - 1, p.col] = true;
                }

                // moving down
                if (isValid(p.row + 1, p.col, grid, visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row + 1, p.col,
                                        p.dist + grid[p.row + 1, p.col]));
                    visited[p.row + 1, p.col] = true;
                }

                // moving left
                if (isValid(p.row, p.col - 1, grid, visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row, p.col - 1,
                                        p.dist + grid[p.row, p.col - 1]));
                    visited[p.row, p.col - 1] = true;
                }

                // moving right
                if (isValid(p.row, p.col + 1, grid,
                            visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row, p.col + 1,
                                        p.dist + grid[p.row, p.col + 1]));
                    visited[p.row, p.col + 1] = true;
                }
            }

            int result = minScores[grid.GetLength(0) - 1, grid.GetLength(1) - 1];
            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static bool isValid(int x, int y,
                                int[,] grid,
                                bool[,] visited,
                               int actScore)
        {
            if (x >= 0 && y >= 0 && x < grid.GetLength(0)
                && y < grid.GetLength(1))
            {
                if (actScore + grid[x, y] < minScores[x, y])
                {
                    minScores[x, y] = actScore + grid[x, y];
                    return true;
                }
            }
            return false;
        }


        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input15-1.txt");
            int result = 0;

            int[,] gridA = new int[lines.Length, lines[0].Length];
            minScores = new int[lines.Length * 5, lines[0].Length * 5];
            int colNo = 0;
            int rowNo = 0;
            foreach (var line in lines)
            {
                var row = line.ToCharArray();
                colNo = 0;
                foreach (var r in row)
                {
                    int num = int.Parse(r.ToString());
                    gridA[rowNo, colNo] = num;
                    minScores[rowNo, colNo] = Int32.MaxValue;
                    colNo++;
                }
                rowNo++;
            }

            int[,] grid = new int[lines.Length * 5, lines[0].Length * 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int x = 0; x < gridA.GetLength(0); x++)
                    {
                        for (int y = 0; y < gridA.GetLength(1); y++)
                        {
                            grid[x + gridA.GetLength(0) * i, y + gridA.GetLength(1) * j] = ((gridA[x, y] + i + j - 1) % 9) + 1;
                            minScores[x + gridA.GetLength(0) * i, y + gridA.GetLength(1) * j] = int.MaxValue;
                        }
                    }
                }
            }

            // applying BFS on matrix cells starting from source
            Queue<QItem> queue = new Queue<QItem>();
            queue.Enqueue(new QItem(0, 0, 0));
            minScores[0, 0] = 0;

            bool[,] visited
              = new bool[grid.GetLength(0), grid.GetLength(1)];
            visited[0, 0] = true;

            while (queue.Count > 0)
            {
                QItem p = queue.Dequeue();

                int actScore = minScores[p.row, p.col];

                // moving up
                if (isValid(p.row - 1, p.col, grid, visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row - 1, p.col,
                                        p.dist + grid[p.row - 1, p.col]));
                    visited[p.row - 1, p.col] = true;
                }

                // moving down
                if (isValid(p.row + 1, p.col, grid, visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row + 1, p.col,
                                        p.dist + grid[p.row + 1, p.col]));
                    visited[p.row + 1, p.col] = true;
                }

                // moving left
                if (isValid(p.row, p.col - 1, grid, visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row, p.col - 1,
                                        p.dist + grid[p.row, p.col - 1]));
                    visited[p.row, p.col - 1] = true;
                }

                // moving right
                if (isValid(p.row, p.col + 1, grid,
                            visited, actScore))
                {
                    queue.Enqueue(new QItem(p.row, p.col + 1,
                                        p.dist + grid[p.row, p.col + 1]));
                    visited[p.row, p.col + 1] = true;
                }
            }

            result = minScores[grid.GetLength(0) - 1, grid.GetLength(1) - 1];
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }


    class QItem
    {
        public int row;
        public int col;
        public int dist;
        public QItem(int row, int col, int dist)
        {
            this.row = row;
            this.col = col;
            this.dist = dist;
        }
    }
}
