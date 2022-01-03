using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day4
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input4-1.txt");
            
            List<int> nums = new List<int>();
            foreach (var s in lines[0].Split(','))
            {
                nums.Add(int.Parse(s));
            }

            List<int[,]> boards = new List<int[,]>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                int[,] arr = new int[5, 5];
                for (int j = 0; j < 5; j++)
                {
                    string[] row = lines[i + j].Split(new[] { "", " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < 5; k++)
                    {
                        arr[j, k] = Int32.Parse(row[k]);
                    }
                }

                boards.Add(arr);

                i += 5;
            }

            List<int[,]> indicators = new List<int[,]>();
            for (int i = 0; i < boards.Count; i++)
            {
                int[,] arr = new int[5, 5];
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        arr[j, k] = 0;
                    }
                }
                indicators.Add(arr);
            }

            Dictionary<int, int[]> sums_rows = new Dictionary<int, int[]>();
            Dictionary<int, int[]> sums_cols = new Dictionary<int, int[]>();
            for (int i = 0; i < boards.Count; i++)
            {
                sums_rows[i] = new int[5];
                sums_cols[i] = new int[5];
            }


            bool found = false;
            int boardId = 0;
            int winNum = 0;

            foreach (var num in nums)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    var board = boards[i];

                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (board[j, k] == num)
                            {
                                indicators[i][j, k] = 1;
                                sums_rows[i][j]++;
                                sums_cols[i][k]++;
                                if (sums_rows[i][j] == 5 || sums_cols[i][j] == 5)
                                {
                                    found = true;
                                    boardId = i;
                                    winNum = num;
                                    break;
                                }
                            }
                        }
                        if (found)
                            break;
                    }
                    if (found)
                        break;

                }
                if (found)
                    break;
            }

            int sum = 0;
            var winBoard = boards[boardId];
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (indicators[boardId][j, k] == 0)
                    {
                        sum += winBoard[j, k];
                    }
                }
            }


            Console.WriteLine(sum * winNum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input4-2.txt");

            List<int> nums = new List<int>();
            foreach (var s in lines[0].Split(','))
            {
                nums.Add(int.Parse(s));
            }

            List<int[,]> boards = new List<int[,]>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                int[,] arr = new int[5, 5];
                for (int j = 0; j < 5; j++)
                {
                    string[] row = lines[i + j].Split(new[] { "", " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < 5; k++)
                    {
                        arr[j, k] = Int32.Parse(row[k]);
                    }
                }

                boards.Add(arr);

                i += 5;
            }

            List<int[,]> indicators = new List<int[,]>();
            for (int i = 0; i < boards.Count; i++)
            {
                int[,] arr = new int[5, 5];
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        arr[j, k] = 0;
                    }
                }
                indicators.Add(arr);
            }


            List<int> wonBoards = new List<int>();
            Dictionary<int, int[]> sums_rows = new Dictionary<int, int[]>();
            Dictionary<int, int[]> sums_cols = new Dictionary<int, int[]>();
            for (int i = 0; i < boards.Count; i++)
            {
                sums_rows[i] = new int[5];
                sums_cols[i] = new int[5];
                wonBoards.Add(0);
            }


            bool found = false;
            int boardId = 0;
            int winNum = 0;

            foreach (var num in nums)
            {
                for (int i = 0; i < boards.Count; i++)
                {
                    var board = boards[i];

                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (board[j, k] == num)
                            {
                                indicators[i][j, k] = 1;
                                sums_rows[i][j]++;
                                sums_cols[i][k]++;
                                if (sums_rows[i][j] == 5 || sums_cols[i][k] == 5)
                                {
                                    wonBoards[i] = 1;
                                    if (!wonBoards.Contains(0))
                                    {
                                        found = true;
                                        boardId = i;
                                        winNum = num;

                                        break;
                                    }

                                }
                            }
                        }
                        if (found)
                            break;
                    }
                    if (found)
                        break;

                }
                if (found)
                    break;
            }

            int sum = 0;
            var winBoard = boards[boardId];
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (indicators[boardId][j, k] == 0)
                    {
                        sum += winBoard[j, k];
                    }
                }
            }


            Console.WriteLine(sum * winNum);
            Console.ReadKey();
        }
    }
}
